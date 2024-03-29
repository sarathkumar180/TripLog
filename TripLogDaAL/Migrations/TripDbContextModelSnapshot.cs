﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using TripLogDaAL.DBContext;

namespace TripLogDaAL.Migrations
{
    [DbContext(typeof(TripDbContext))]
    partial class TripDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TripLogDaAL.Entities.Accommodation", b =>
                {
                    b.Property<int>("AccommodationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccommodationEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccommodationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccommodationPhone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccommodationId");

                    b.ToTable("Accommodations");
                });

            modelBuilder.Entity("TripLogDaAL.Entities.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActivityId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("TripLogDaAL.Entities.Destination", b =>
                {
                    b.Property<int>("DestinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DestinationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DestinationId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("TripLogDaAL.Entities.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccommodationId")
                        .HasColumnType("int");

                    b.Property<int>("DestinationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TripId");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("TripLogDaAL.Entities.TripActivity", b =>
                {
                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.HasKey("TripId", "ActivityId");

                    b.HasIndex("ActivityId");

                    b.ToTable("TripActivities");
                });

            modelBuilder.Entity("TripLogDaAL.Entities.Trip", b =>
                {
                    b.HasOne("TripLogDaAL.Entities.Accommodation", "Accommodation")
                        .WithMany("Trips")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TripLogDaAL.Entities.Destination", "Destination")
                        .WithMany("Trips")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TripLogDaAL.Entities.TripActivity", b =>
                {
                    b.HasOne("TripLogDaAL.Entities.Activity", "Activity")
                        .WithMany("TripActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripLogDaAL.Entities.Trip", "Trip")
                        .WithMany("TripActivities")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
