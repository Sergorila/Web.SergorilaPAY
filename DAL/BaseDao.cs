using DAL.DBContext;
namespace DAL;

public class BaseDao : IAsyncDisposable, IDisposable
{
    protected readonly NpgsqlContext DbContext;

    protected BaseDao(NpgsqlContext dbContext)
    {
        DbContext = dbContext;
    }

    public async ValueTask DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}