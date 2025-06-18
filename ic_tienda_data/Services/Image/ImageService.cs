using ic_tienda_business.IServices.Images;
using Microsoft.AspNetCore.Http;

namespace ic_tienda_data.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public ImageService(IFirebaseStorageService firebaseStorageService)
        {
            _firebaseStorageService = firebaseStorageService;
        }

        public async Task DeleteImageAsync(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
                await _firebaseStorageService.DeleteImageFromFirebaseAsync(fileName);
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile, string prefix)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("No se proporcionó una imagen válida.");

            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Tipo de archivo no permitido. Solo se permiten jpg, jpeg, png, webp.");

            //agregre
            // Asegurar que el prefix no tenga extensión
            prefix = Path.GetFileNameWithoutExtension(prefix);
            //agregre
            var newFileName = $"{prefix}{fileExtension}";

            using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            await _firebaseStorageService.UploadImageToFirebaseAsync(memoryStream, newFileName);

            return newFileName;
        }

        public string GetPublicIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("La URL de la imagen no puede estar vacía.");

            // Si la URL no comienza con "http", asumir que es un nombre de archivo y retornarlo directamente
            if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return $"images/{Path.GetFileNameWithoutExtension(url)}";

            // Manejar la URL como antes si es válida
            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Split('/');
            string fileNameWithExtension = segments.Last();

            return $"images/{Path.GetFileNameWithoutExtension(fileNameWithExtension)}";
        }

    }
}