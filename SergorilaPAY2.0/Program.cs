using System.Text.Json.Serialization;
using BLL;
using BLL.Interfaces;
using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;
using DAL.DBContext;
using DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<ICategoryDao, CategoryDao>();
builder.Services.AddScoped<IProductDao, ProductDao>();
builder.Services.AddScoped<IOrderDao, OrderDao>();
builder.Services.AddScoped<IImgDao, ImgDao>();

builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IHrDeparmentLogic, HrDepartmentLogic>();


builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        var jsonConverter = new JsonStringEnumConverter();
        options.JsonSerializerOptions.Converters.Add(jsonConverter);
    }
);

builder.Services.AddDbContextFactory<FactoryContext>(
    optionsAction =>
    {
        optionsAction
            .UseNpgsql(config?.NpgsqlConnectionString);
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

app.Run();