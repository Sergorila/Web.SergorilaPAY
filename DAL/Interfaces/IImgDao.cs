using System.Net.Mime;
using Entities;

namespace DAL.Interfaces;

public interface IImgDao
{
    Task AddImageForItemAsync(Img image); 
    Task<Img> GetImageItemAsync(int imageId);
    Task<bool> RemoveImageAsync(int imageId);
}