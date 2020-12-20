using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataAccessLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace OrderezeAPI
{
    public class ImageService : IImageService
    {
        public IDataContext DataContext { get; }
        public IBlobService BlobService { get; }

        public ImageService(IDataContext dataContext, IBlobService blobService)
        {
            DataContext = dataContext;
            BlobService = blobService;
        }

        public async Task<List<ImageModel>> GetImagesAsync()
        {
            var imageList = await DataContext.Images.ToListAsync();
            var imageModelList = imageList.Select(a => new ImageModel()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                ImagePath = a.ImagePath
            }).ToList();

            return imageModelList;
        }

        public async Task<int> AddNewImageAsync(ImageModel imageModel)
        {
            var pathFromBlob = await BlobService.UploadImageAsync(imageModel.Name, imageModel.ImageFile);
            if (!string.IsNullOrWhiteSpace(pathFromBlob))
            {
                await DataContext.BeginTransactionAsync();

                var image = new Image
                {
                    Name = imageModel.Name,
                    Description = imageModel.Description,
                    ImagePath = pathFromBlob
                };

                await DataContext.Images.AddAsync(image);

                await DataContext.CommitAsync();

                return image.Id;
            }
            return 0;
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var image = DataContext.Images.ToList().FirstOrDefault(x => x.Id == id);
            if (image != null)
            {
                var result = await BlobService.RemoveImageAsync(image.ImagePath);

                if (result)
                {
                    await DataContext.BeginTransactionAsync();
                    DataContext.Images.Remove(image);
                    await DataContext.CommitAsync();

                    return true;
                }
            }
            return false;
        }
    }
}
