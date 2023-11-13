using System;
using Microsoft.EntityFrameworkCore;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Repository
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
        : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Lease> Leases { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Brand és Car közötti egy-egyhez-több kapcsolat
        //    modelBuilder.Entity<Brand>()
        //        .HasMany(b => b.Cars)
        //        .WithOne(c => c.Brand)
        //        .HasForeignKey(c => c.BrandId);

        //    // Car és RentalEvent közötti egy-egyhez-több kapcsolat
        //    modelBuilder.Entity<Car>()
        //        .HasMany(c => c.RentalEvents)
        //        .WithOne(re => re.Car)
        //        .HasForeignKey(re => re.CarId);

        //    // További konfigurációk
        //    // Például egyéni indexek, táblanév beállítások, stb.
        //}
        // ... a XYZDbContext osztályon belül ...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand { BrandId = 1, Name = "Toyota" },
                new Brand { BrandId = 2, Name = "Honda" }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car { CarId = 1, Model = "Corolla", BrandId = 1 },
                new Car { CarId = 2, Model = "Civic", BrandId = 2 }
            );

            modelBuilder.Entity<Lease>().HasData(
                new Lease { LeaseId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), CarId = 1 },
                new Lease { LeaseId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), CarId = 2 }
            );
        }

    }
}
