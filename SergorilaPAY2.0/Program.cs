using System.Text.Json.Serialization;
using AutoMapper;
using BLL;
using BLL.Interfaces;
using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;
using DAL.DBContext;
using DAL.Interfaces;
using SergorilaPAY2._0;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration
    .GetSection("EnvironmentVariables")
    .Get<EnvironmentVariables>();

var mappingConfig = new MapperConfiguration(mc =>
{
    var mapping = new Mapping();
    mc.AddProfile(mapping);
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();  

//var options = new DbContextOptionsBuilder<NpgsqlContext>();
//options.UseNpgsql(config?.ConnectionString);

builder.Services.AddScoped<IUserDao, UserDao>();
//builder.Services.AddScoped<ICategoryDao, CategoryDao>();
//builder.Services.AddScoped<IProductDao, ProductDao>();
//builder.Services.AddScoped<IOrderDao, OrderDao>();
//builder.Services.AddScoped<IImgDao, ImgDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
//builder.Services.AddScoped<ICategoryLogic, CategoryLogic>();
//builder.Services.AddScoped<IProductLogic, ProductLogic>();
//builder.Services.AddScoped<IOrderLogic, OrderLogic>();
//builder.Services.AddScoped<IImgLogic, ImgLogic>();

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        var jsonConverter = new JsonStringEnumConverter();
        options.JsonSerializerOptions.Converters.Add(jsonConverter);
    }
);

builder.Services.AddDbContext<NpgsqlContext>(
    optionsAction =>
    {
        optionsAction
            .UseNpgsql(config?.ConnectionString);
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

app.Run();