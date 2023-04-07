using Entities;
using DAL.Interfaces;
using DAL.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ImgDao : BaseDao, IImgDao
{
    public ImgDao(NpgsqlContext dbContext) : base(dbContext)
    {
    }

    public async Task AddImageForItemAsync(Img image)
    {
        await DbContext.Images.AddAsync(image);
        await DbContext.SaveChangesAsync();
    }

    public async Task<Img> GetImageItemAsync(int imageId)
    {
        var image = await DbContext.Images
            .FirstAsync(item => item.Id == imageId);
        return image;
    }

    public async Task<bool> RemoveImageAsync(int imageId)
    {
        var image = await GetImageItemAsync(imageId);
        DbContext.Images.Remove(image);
        var removedCount = await DbContext.SaveChangesAsync();
        return removedCount != 0;
    }
}