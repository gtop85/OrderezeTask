using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OrderezeAPI
{
    public interface IBlobService
    {
        Task<string> UploadImageAsync(string name, IFormFile file);

        Task<bool> RemoveImageAsync(string fileName);
    }
}

