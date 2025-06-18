// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using ic_tienda_business.IServices.Images;
// using Microsoft.AspNetCore.Http;

// namespace ic_tienda_data.Services.Image
// {
//     //isjadhwadwadwawdwadawd
//     //wadkjdawjnawdawd
//     public class ImageFirebaseService : IImageFirebaseService
//     {
//         private readonly IFirebaseStorageService _image;
//         public ImageFirebaseService(IFirebaseStorageService firebaseStorage)
//         {
//             _image = firebaseStorage;
//         }

//         public async Task DeleteImageAsync(string imageUrl)
//         {
//             if (string.IsNullOrEmpty(imageUrl))
//             {
//                 return;
//             }

//             // Eliminar la imagen en Firebase
//             var fileName = Path.GetFileName(imageUrl);
//             await _image.DeleteImageFromFirebaseAsync(fileName);
//         }

//         public async Task<string> UploadImageAsync(IFormFile image, string entityId)
//         {
//             // Verificar que la imagen no sea nula o vacía
//             if (image == null || image.Length == 0)
//             {
//                 throw new ArgumentException("No se proporcionó una imagen válida.");
//             }

//             // Validar la extensión de la imagen
//             var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
//             var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
//             if (!allowedExtensions.Contains(fileExtension))
//             {
//                 throw new ArgumentException("Tipo de archivo no permitido. Solo se permiten archivos jpg, jpeg, png, webp.");
//             }

//             // Crear un nuevo nombre para el archivo basado en el ID de la entidad
//             var newFileName = $"{entityId}{fileExtension}";

//             // Crear un MemoryStream para la imagen
//             using var memoryStream = new MemoryStream();
//             await image.CopyToAsync(memoryStream);
//             memoryStream.Position = 0; // Reiniciar el stream

//             // Subir la imagen a Firebase y obtener la URL
//             var imageUrl = await _image.UploadImageToFirebaseAsync(memoryStream, newFileName);
//             return imageUrl;
//         }
//     }
// }