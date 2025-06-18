using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ic_tienda_business.IServices.Images
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string prefix);
        Task DeleteImageAsync(string fileName);
        string GetPublicIdFromUrl(string url);
    }
}