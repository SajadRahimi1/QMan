using Microsoft.AspNetCore.Http;

namespace QMan.Application.Interfaces;

public interface IFileRepository
{
    void DeleteFile(string filePath);
    Task<string> SaveFileAsync(IFormFile file,string section);
    byte[] GetFile(string filePath);
}