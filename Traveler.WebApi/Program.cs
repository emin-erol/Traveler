using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;
using Traveler.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7118") // frontend URL'ini buraya yaz
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

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
builder.Services.AddScoped<ILocationDal, LocationRepository>();
builder.Services.AddScoped<ICarDal, CarRepository>();
builder.Services.AddScoped<IPricingDal, PricingRepository>();
builder.Services.AddScoped<ICarPricingDal, CarPricingRepository>();
builder.Services.AddScoped<IFeatureDal, FeatureRepository>();
builder.Services.AddScoped<ICarClassDal, CarClassRepository>();
builder.Services.AddScoped<ICarFeatureDal, CarFeatureRepository>();
builder.Services.AddScoped<IBrandDal, BrandRepository>();
builder.Services.AddScoped<IMileagePackageDal, MileagePackageRepository>();
builder.Services.AddScoped<ICityDal, CityRepository>();
builder.Services.AddScoped<ILocationAvailabilityDal, LocationAvailabilityRepository>();
builder.Services.AddScoped<IPackageOptionDal, PackageOptionRepository>();
builder.Services.AddScoped<ISecurityPackageDal, SecurityPackageRepository>();
builder.Services.AddScoped<ISecurityPackageOptionDal, SecurityPackageOptionRepository>();
builder.Services.AddScoped<IReservationDal, ReservationRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
