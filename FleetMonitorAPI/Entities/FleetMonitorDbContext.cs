using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FleetMonitorAPI.Entities
{
    public class FleetMonitorDbContext : DbContext
    {
        public FleetMonitorDbContext(DbContextOptions<FleetMonitorDbContext> options) : base(options)
        {

        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Device>()
                .Property(e => e.Id)
                .IsRequired();

            modelBuilder.Entity<Device>()
                .Property(e => e.Name)
                .HasMaxLength(30);

            modelBuilder.Entity<Positions>()
                .Property(e=> e.DeviceId)
                .IsRequired();

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.VIN)
                .IsRequired();

            modelBuilder.Entity<Driver>()
                .Property(e => e.FirstName)
                .IsRequired();

            modelBuilder.Entity<Driver>()
                .Property(e => e.LastName)
                .IsRequired();

            modelBuilder.Entity<Device>()
                .HasOne(e => e.Vehicle)
                .WithOne(e => e.Device)
                .HasForeignKey<Vehicle>(e => e.DeviceId);
        }
    }
}
