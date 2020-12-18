using System.Collections.Generic;
using System.Linq;
using EFDataAccessLibrary;

namespace OrderezeAPI
{
    public class ImageService : IImageService
    {
        public IDataContext DataContext { get; }

        public ImageService(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public List<ImageModel> GetImages()
        {
            var imageList = DataContext.Images.ToList();
            var imageModelList = imageList.Select(a => new ImageModel()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                ImagePath = a.ImagePath
            }).ToList();

            return imageModelList;
        }

        public int AddNewImage(ImageModel imageModel)
        {
            //save to blob storage
            var pathFromBlob = "";


            DataContext.BeginTransaction();

            var image = new Image
            {
                Name = imageModel.Name,
                Description = imageModel.Description,
                ImagePath = pathFromBlob
            };

            DataContext.Images.Add(image);

            DataContext.Commit();

            return image.Id;
        }

        public bool DeleteImage(int id)
        {
            DataContext.BeginTransaction();
            var image = DataContext.Images.ToList().FirstOrDefault(x => x.Id == id);
            if (image != null)
            {
                DataContext.Images.Remove(image);

                //remove from blob storage
                //image.ImagePath;

                DataContext.Commit();
                return true;
            }
            return false;
        }
    }
}
