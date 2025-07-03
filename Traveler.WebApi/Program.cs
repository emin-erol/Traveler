using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;
using Traveler.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TravelerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<TravelerDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IManagementDal, ManagementRepository>();
builder.Services.AddScoped<IAboutDal, AboutRepository>();
builder.Services.AddScoped<IBannerDal, BannerRepository>();
builder.Services.AddScoped<IServiceDal, ServiceRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
