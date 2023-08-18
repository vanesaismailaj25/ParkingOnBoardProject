using Microsoft.EntityFrameworkCore;
using ParkingOnBoard.Models;

namespace ParkingOnBoard.Context;

public partial class ParkingOnBoardContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {  
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLExpress;Database=ParkingOnBoardDB;Integrated Security=true;TrustServerCertificate=true");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Street> Streets { get; set; }
    public DbSet<ParkingSlot> ParkingSlots { get; set; }
    public DbSet<City> Cities { get; set; }

    //Data seeding
   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<City>().HasData(
            new City
            {
                Id = 1, CityName = "Tirana"
            },
            new City
            {
                Id = 2, CityName = "Berat"
            },
            new City
            {
                Id = 3, CityName = "Skrapar"
            }
            );

        modelBuilder.Entity<Street>().HasData(
            new Street
            {
                Id = 1, Name = "Naim Frasheri", HasTwoSides = false, IsClosed = false, CityId = 1,
            },
            new Street
            {
                Id = 2, Name = "Sami Frasheri", HasTwoSides = false,IsClosed = false, CityId = 1,
            },
            new Street
            {
                Id = 3, Name = "Antipatrea", HasTwoSides = true,IsClosed = false, CityId = 2,                
            }
        );

        modelBuilder.Entity<ParkingSlot>().HasData(
            new ParkingSlot
            {
                Id = 1, SlotNumber = 1, IsBusy = false, IsClosed = false, StreetId = 1,
            },
            new ParkingSlot
            {
                Id = 2, SlotNumber = 2, IsBusy = false, IsClosed = false, StreetId = 1,
            },     
            new ParkingSlot
            {
                Id = 3, SlotNumber = 3, IsBusy = false, IsClosed = false, StreetId = 3,
            },
            new ParkingSlot
            {
                Id = 4, SlotNumber = 4, IsBusy = false, IsClosed = false, StreetId = 3, 
            },
            new ParkingSlot
            {
                Id = 5, SlotNumber = 5, IsBusy = false, IsClosed = false, StreetId = 3, 
            },
            new ParkingSlot
            {
                Id = 6, SlotNumber = 6, IsBusy = false, IsClosed = false, StreetId = 3,
            },
            new ParkingSlot
            {
                Id = 7, SlotNumber = 7, IsBusy = false, IsClosed = false, StreetId = 2,
            },
            new ParkingSlot
            {
                Id= 8, SlotNumber = 8, IsBusy = false, IsClosed = false, StreetId = 2,
            }
            );
    }
}
