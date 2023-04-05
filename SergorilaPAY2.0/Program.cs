using Entities;
using Microsoft.EntityFrameworkCore;
using DAL.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration
    .GetSection("EnvironmentVariables")
    .Get<EnvironmentVariables>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<NpgsqlContext>(
    options =>
    {
        options.UseNpgsql(config?.ConnectionString);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var options = new DbContextOptionsBuilder<NpgsqlContext>();
options.UseNpgsql(config?.ConnectionString);
var context = new NpgsqlContext(options.Options);

var users = context.Users;
var products = context.Products;
var orders = context.Orders;
var categories = context.Categories;
var images = context.Images;

//app.Run();