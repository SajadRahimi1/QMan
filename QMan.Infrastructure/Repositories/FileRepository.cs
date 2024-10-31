using Microsoft.AspNetCore.Http;
using QMan.Application.Interfaces;

namespace QMan.Infrastructure.Repositories;

public class FileRepository : IFileRepository
{
    private const string UploadDirectory = "uploads";

    public FileRepository()
    {
        if (!Directory.Exists(UploadDirectory))
            Directory.CreateDirectory(UploadDirectory);
    }

    public void DeleteFile(string filePath)
    {
        var path = Path.Combine(UploadDirectory, filePath);
        File.Delete(path);
    }

    public byte[] GetFile(string filePath)
    {
        var path = Path.Combine(UploadDirectory, filePath);
        return File.ReadAllBytes(path);
    }

    public async Task<string> SaveFileAsync(IFormFile file, string section)
    {
        if (!Directory.Exists($"{UploadDirectory}/{section}"))
        {
            Directory.CreateDirectory($"{UploadDirectory}/{section}");
        }

        var uniqueFilename = file.FileName.Split('.').First() + "_" + Guid.NewGuid() + "." +
                             file.FileName.Split('.').Last();
        var path = Path.Combine(UploadDirectory, section, uniqueFilename);
        await file.CopyToAsync(new FileStream(path, FileMode.Create));
        return $"{section}/{uniqueFilename}";
    }
}