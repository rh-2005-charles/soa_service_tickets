

namespace ic_tienda_business.IServices.Images
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadImageToFirebaseAsync(Stream imageStream, string fileName);
        Task DeleteImageFromFirebaseAsync(string fileName);
    }
}