using DAL.Interfaces;
using BLL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace BLL;

public class ImgLogic : BaseLogic, IImgLogic
{
    private readonly IImgDao _dao;
    
    public ImgLogic(ILogger<BaseLogic> logger, IImgDao dao) : base(logger)
    {
        _dao = dao;
    }

    public async Task AddImageForItemAsync(Img image)
    {
        try
        {
            Logger.LogInformation("Trying add image: {Id}", image.Id);
            await _dao.AddImageForItemAsync(image);
            Logger.LogInformation("Complete adding image: {Id}", image.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while adding image: {Id}", image.Id);
            throw;
        }
    }

    public async Task<Img> GetImageItemAsync(int imageId)
    {
        try
        {
            Logger.LogInformation("Trying get image by ID: {Id}", imageId);
            
            var res = await _dao.GetImageItemAsync(imageId);
            
            Logger.LogInformation("Compete getting image by ID: {Id}", imageId);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while getting image by ID: {Id}", imageId);
            throw;
        }
    }

    public async Task<bool> RemoveImageAsync(int imageId)
    {
        try
        {
            Logger.LogInformation("Trying remove image by ID: {Id}", imageId);
            var isRemoved = await _dao.RemoveImageAsync(imageId);
            Logger.LogInformation("Complete removing image by ID: {Id}", imageId);
            return isRemoved;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while removing image by ID: {Id}", imageId);
            throw;
        }
    }
}