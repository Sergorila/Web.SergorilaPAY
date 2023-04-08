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
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterUser(modelBuilder);
            RegisterProduct(modelBuilder);
            RegisterOrder(modelBuilder);
            RegisterCategory(modelBuilder);
            RegisterImage(modelBuilder);
        }

        private void RegisterUser(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();
            
            user.HasKey(i => i.Id);
            user.Property(i => i.Login).IsRequired();
            user.Property(i => i.Password).IsRequired();
            user.Property(i => i.Phone).IsRequired();
            user.Property(i => i.FIO).IsRequired();
            user.Property(i => i.TelegramID).IsRequired();
        }
        
        private void RegisterProduct(ModelBuilder modelBuilder)
        {
            var product = modelBuilder.Entity<Product>();
            
            product.HasKey(i => i.Id);
            product.Property(i => i.Title).IsRequired();
            product.Property(i => i.Price).IsRequired();
            product.Property(i => i.Img).IsRequired();
            product.Property(i => i.Description).IsRequired();
        }
        
        private void RegisterOrder(ModelBuilder modelBuilder)
        {
            var order = modelBuilder.Entity<Order>();
            
            order.HasKey(i => i.Id);
            order.Property(i => i.Status).IsRequired();
            order.Property(i => i.Sum).IsRequired();
            order.Property(i => i.Address).IsRequired();
            order.HasMany(s => s.Products)
                .WithMany(i => i.Orders);
            order.HasOne(s => s.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(s => s.UserId);
        }

        private void RegisterCategory(ModelBuilder modelBuilder)
        {
            var categories = modelBuilder.Entity<Category>();
    
            categories.HasKey(c => c.Id);
            categories.Property(c => c.Title).IsRequired();
            categories.HasMany(c => c.Elems)
                .WithMany(i => i.Categories);
        }
        
        private void RegisterImage(ModelBuilder modelBuilder)
        {
            var img = modelBuilder.Entity<Img>();
        
            img.HasKey(i => i.Id);
            img.Property(i => i.Image).IsRequired();
        
            img.HasOne(i => i.Product)
                .WithMany(i => i.Images)
                .HasForeignKey(i => i.ProductId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Img> Images { get; set; }
}