using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.IServices.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ic_tienda_data.Services.Image
{
    public class ImageServiceLocal : IImageServiceLocal
    {
        private readonly string _comprobantesPath;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };

#pragma warning disable CS8604
        public ImageServiceLocal(IConfiguration configuration)
        {
            _comprobantesPath = Path.Combine(Directory.GetCurrentDirectory(), configuration["ImagesPath"]);

            // Crear la carpeta si no existe
            if (!Directory.Exists(_comprobantesPath))
            {
                Directory.CreateDirectory(_comprobantesPath);
            }
        }
#pragma warning restore CS8604

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null) throw new Exception("Imagen no encontrado.");

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Tipo de archivo no permitido. Solo se permiten archivos jpg, jpeg, png y pdf.");
            }

            var fileName = $"{Guid.NewGuid()}{fileExtension}"; // Nombre único
            var filePath = Path.Combine(_comprobantesPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}