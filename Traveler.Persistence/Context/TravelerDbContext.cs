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
        public DbSet<CarFeature> CarFeatures { get; set; }
        public DbSet<CarPricing> CarPricings { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CarClass> CarClasses { get; set; }
    }

}
