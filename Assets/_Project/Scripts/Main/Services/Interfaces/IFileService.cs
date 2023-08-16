namespace Main.Services
{
    public interface IFileService : IService
    {
        string StorageFolder { get; }
    }
}