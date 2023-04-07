using Entities;

namespace BLL.Interfaces;

public interface IImgLogic
{
    Task AddImageForItemAsync(Img image); 
    Task<Img> GetImageItemAsync(int imageId);
    Task<bool> RemoveImageAsync(int imageId);
}