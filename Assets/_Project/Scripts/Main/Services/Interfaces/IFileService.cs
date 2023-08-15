namespace _Project.Scripts.Main.AppServices
{
    public interface IFileService : IService
    {
        string StorageFolder { get; }
    }
}