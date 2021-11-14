using Microsoft.EntityFrameworkCore;
using TripLogDaAL.Entities;

namespace TripLogDaAL.DBContext
{
    public sealed class TripDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Accommodation> Accommodations { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<TripActivity> TripActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Trip>()
                .HasOne<Destination>(e => e.Destination)
                .WithMany(d => d.Trips)
                .HasForeignKey(e => e.DestinationId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Trip>()
                .HasOne<Accommodation>(e => e.Accommodation)
                .WithMany(d => d.Trips)
                .HasForeignKey(e => e.AccommodationId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<TripActivity>()
                .HasKey(bc => new { bc.TripId, bc.ActivityId });
            modelBuilder.Entity<TripActivity>()
                .HasOne(bc => bc.Trip)
                .WithMany(b => b.TripActivities)
                .HasForeignKey(bc => bc.TripId);
            modelBuilder.Entity<TripActivity>()
                .HasOne(bc => bc.Activity)
                .WithMany(c => c.TripActivities)
                .HasForeignKey(bc => bc.ActivityId);

            base.OnModelCreating(modelBuilder);

        }

        public TripDbContext(DbContextOptions<TripDbContext> options)
            : base(options)
        {
            
        }
    }
}
