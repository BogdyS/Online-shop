namespace Shop.Services.Interfaces
{
    public interface IS3Service
    {
        Task UploadFile(Stream fileStream, string key);
        Task<string> GetFileUrl(string? key);
    }
}
