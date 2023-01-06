using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xblog.Data.Implementation;
using Xblog.Data.Interface;
using XBlog.Models.Entities;
using XBlog.Models.Models;
using XBlog.Services.Implementations;
using XBlog.Services.Interfaces;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services;
var config = builder.Configuration;
services.AddDbContext<AppDbContext>(option =>option.UseSqlServer(config.GetConnectionString("AppConnectionString"), b => b.MigrationsAssembly("XBlog.Api")));
var connectionstring = config.GetConnectionString("AppConnectionString");
JwtConfiguration jwtconfig = new JwtConfiguration();
string sectionName = typeof(JwtConfiguration).Name;
config.GetSection(sectionName).Bind(jwtconfig);
services.AddSingleton(jwtconfig);
services.AddTransient<JwtAuthenticator>();

services.AddIdentity<User, IdentityRole>(option =>
{
    option.Password.RequiredLength = 6;
    option.Password.RequireDigit = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();
services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();

}));
builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddTransient<IArticleService, ArticleService>();
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
