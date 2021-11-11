using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripLog.Models;

namespace TripLog.DBContext
{
    public class TripDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Accommodation> Accommodations { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<TripActivity> TripActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            Database.EnsureCreated();
        }
    }
}
