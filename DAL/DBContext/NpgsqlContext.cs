using System.Net.Mime;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DBContext;

public sealed class NpgsqlContext : DbContext
{
    public NpgsqlContext(DbContextOptions<NpgsqlContext> options) 
            : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Category> Categories { get; set; }
}