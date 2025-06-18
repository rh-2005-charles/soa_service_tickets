using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ic_tienda_business.IServices.Images;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ic_tienda_data.Services.Image
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        // private readonly StorageClient _storageClient;
        // private readonly string _bucketName;

        private readonly Cloudinary _cloudinary;

#pragma warning disable CS8618
#pragma warning disable CS8602
        public FirebaseStorageService(IConfiguration configuration)
        {
            var credentialPath = configuration["Cloudinary:CredentialPath"];

            if (string.IsNullOrEmpty(credentialPath) || !File.Exists(credentialPath))
            {
                throw new FileNotFoundException("No se encontró el archivo de credenciales de Cloudinary.");
            }

            var json = File.ReadAllText(credentialPath);
            var cloudinaryConfig = JsonConvert.DeserializeObject<CloudinaryConfig>(json);

            var account = new Account(cloudinaryConfig.CloudName, cloudinaryConfig.ApiKey, cloudinaryConfig.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
#pragma warning restore CS8602
#pragma warning restore CS8618

        public async Task<string> UploadImageToFirebaseAsync(Stream imageStream, string fileName)
        {
            try
            {
                // Eliminar la extensión del nombre de archivo para el PublicId
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, imageStream),
                    PublicId = $"images/{fileNameWithoutExtension}"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception($"Error al subir la imagen a Cloudinary: {uploadResult.Error.Message}");
                }

                //return uploadResult.SecureUrl.AbsoluteUri;
                // Retornar solo el nombre del archivo para guardarlo en la base de datos
                return $"{fileNameWithoutExtension}{Path.GetExtension(fileName)}";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al subir la imagen a Cloudinary.", ex);
            }
        }

        public async Task DeleteImageFromFirebaseAsync(string fileName)
        {
            try
            {
                // Asegurar que solo se pase el nombre del archivo sin la extensión
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string publicId = $"images/{fileNameWithoutExtension}";

                Console.WriteLine($"Eliminando imagen con PublicId: {publicId}");

                //string publicId = $"{fileName}";

                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);

                Console.WriteLine($"Resultado de eliminación: {result.Result}");

                if (result.Error != null)
                {
                    throw new Exception($"Error al eliminar la imagen de Cloudinary: {result.Error.Message}");
                }

                if (result.Result != "ok")
                {
                    throw new Exception("No se pudo eliminar la imagen de Cloudinary.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la imagen de Cloudinary.", ex);
            }
        }
    }
    public class CloudinaryConfig
    {
        public string? CloudName { get; set; }
        public string? ApiKey { get; set; }
        public string? ApiSecret { get; set; }
    }
}