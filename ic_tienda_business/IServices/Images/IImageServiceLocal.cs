using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ic_tienda_business.IServices.Images
{
    public interface IImageServiceLocal
    {
        Task<string> SaveImageAsync(IFormFile file);
    }
}