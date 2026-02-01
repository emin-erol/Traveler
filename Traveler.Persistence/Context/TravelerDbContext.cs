using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Domain.Entities;

namespace Traveler.Persistence.Context
{
    public class TravelerDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public TravelerDbContext(DbContextOptions<TravelerDbContext> options) : base(options) { }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ModelFeature> ModelFeatures { get; set; }
        public DbSet<ModelPricing> ModelPricings { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CarClass> CarClasses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MileagePackage> MileagePackages { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<SecurityPackage> SecurityPackages { get; set; }
        public DbSet<PackageOption> PackageOptions { get; set; }
        public DbSet<SecurityPackageOption> SecurityPackageOptions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>()
                .HasOne(x => x.PickUpLocation)
                .WithMany(y => y.PickUpReservation)
                .HasForeignKey(z => z.PickUpLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Reservation>()
                .HasOne(x => x.DropOffLocation)
                .WithMany(y => y.DropOffReservation)
                .HasForeignKey(z => z.DropOffLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

}
