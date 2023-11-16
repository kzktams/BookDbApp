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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne(t => t.Brands)
                .WithMany(t => t.Cars)
                .HasForeignKey(t => t.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lease>()
                .HasOne(t => t.Cars)
                .WithMany(t => t.Leases)
                .HasForeignKey(t => t.CarId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Brand>().HasData(
                new Brand { BrandId = 1, Name = "BrandA", Headquarters = "CityA", YearEstablished = 1950, Founder = "FounderA" },
                new Brand { BrandId = 2, Name = "BrandB", Headquarters = "CityB", YearEstablished = 1960, Founder = "FounderB" }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car { CarId = 1, Model = "ModelX", Color = "Red", Year = 2020, Price = 30000, BrandId = 1 },
                new Car { CarId = 2, Model = "ModelY", Color = "Blue", Year = 2021, Price = 35000, BrandId = 2 }
            );


            modelBuilder.Entity<Lease>().HasData(
                new Lease { LeaseId = 1, StartDate = new DateTime(2023, 1, 1), EndDate = new DateTime(2023, 12, 31), LeaseAmount = 1000, LesseeName = "John Doe", IsActive = true, CarId = 1 },
                new Lease { LeaseId = 2, StartDate = new DateTime(2023, 2, 1), EndDate = new DateTime(2023, 11, 30), LeaseAmount = 900, LesseeName = "Jane Smith", IsActive = true, CarId = 2 }
            );
        }

    }
}
