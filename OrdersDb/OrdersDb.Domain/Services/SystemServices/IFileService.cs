namespace OrdersDb.Domain.Services.SystemServices
{
    public interface IFileService
    {
        void WriteAllBytes(string path, byte[] bytes);
        string GetTemporaryFolder();
        byte[] ReadAllBytes(string path);
    }
}