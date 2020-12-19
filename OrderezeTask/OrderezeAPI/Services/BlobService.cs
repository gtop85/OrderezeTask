using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OrderezeAPI
{
    public class BlobService : IBlobService
    {
        readonly string BlobConnection;
        readonly BlobContainerClient Container;

        public BlobService(IConfiguration configuration)
        {
            BlobConnection = configuration.GetConnectionString("AccessKey");
            Container = new BlobContainerClient(BlobConnection, configuration.GetValue<string>("BlobContainer"));
            if (Container.CreateIfNotExists() != null)
            {
                Container.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            }
        }

        public async Task<string> UploadImageAsync(string name, IFormFile file)
        {
            try
            {
                string fileName = GenerateFileName(name);
                BlobClient blob = Container.GetBlobClient(fileName);

                await using (var stream = File.Create(name))
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    await blob.UploadAsync(stream);
                }
                return Uri.UnescapeDataString(blob.Uri.ToString());
            }
            catch
            {
                //throw new Exception("Something went wrong. Please try again later.");
            }
            finally
            {
                File.Delete(name);
            }
            return string.Empty;
        }


        private string GenerateFileName(string fileName)
        {
            string[] strName = fileName.Split('.');
            string strFileName =
                DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") +
                "/" + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") +
                "." + strName[strName.Length - 1];
            return strFileName;
        }

        public async Task<bool> RemoveImageAsync(string imagePath)
        {
            try
            {
                var fileName = imagePath.Substring(Container.Uri.ToString().Length + 1);
                BlobClient blob = Container.GetBlobClient(Uri.UnescapeDataString(fileName));
                var res = await blob.DeleteIfExistsAsync();

                return res;
            }
            catch (Exception ex)
            { 
            }
            return false;
        }
    }
}

