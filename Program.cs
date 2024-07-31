using LojinhaDaPaulinhaAPI.Data;
using LojinhaDaPaulinhaAPI.Helpers;
using LojinhaDaPaulinhaAPI.Identity;
using LojinhaDaPaulinhaAPI.Repositories;
using LojinhaDaPaulinhaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(cfg =>
{
    cfg.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ¿‡¡·¬‚ƒ‰…È»ËÕÌÃÏŒÓœÔ”Û“Ú‘Ù÷ˆ⁄˙Ÿ˘€˚‹¸—Ò«Á›˝ -_.@";

    cfg.Password.RequireDigit = false;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
    cfg.Password.RequiredLength = 1;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.AddTransient<InitDb>();

builder.Services.AddScoped<IIdentityManager, IdentityManager>();

builder.Services.AddScoped<IDataUnit, DataUnit>();

builder.Services.AddScoped<ControllerHelper>();
builder.Services.AddScoped<DataService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

await InitDatabase(app);
async Task InitDatabase(IHost host)
{
    var scopedFactory = host.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory?.CreateScope();

    var initDb = scope?.ServiceProvider.GetService<InitDb>();

    await initDb.SeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
