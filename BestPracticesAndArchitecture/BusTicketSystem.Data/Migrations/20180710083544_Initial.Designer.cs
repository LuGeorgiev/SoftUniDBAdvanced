﻿// <auto-generated />
using System;
using BusTicketsSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusTicketsSystem.Data.Migrations
{
    [DbContext(typeof(BusTicketsSystemContext))]
    [Migration("20180710083544_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusTicketsSystem.Models.BankAccount", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<int>("CustomerId");

                    b.HasKey("id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BusCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("BusCompanies");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BusStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("TownId");

                    b.HasKey("Id");

                    b.HasIndex("TownId");

                    b.ToTable("BusStations");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankAccountId");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("DATE");

                    b.Property<string>("FirstName")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<int?>("HomeTownId");

                    b.Property<string>("LastName")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("HomeTownId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusCompanyId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<int>("CustomerId");

                    b.Property<float>("Grade");

                    b.Property<DateTime>("PublishingDatetime");

                    b.HasKey("Id");

                    b.HasIndex("BusCompanyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId");

                    b.Property<decimal>("Price");

                    b.Property<int?>("Seat");

                    b.Property<int?>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TripId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArrivalTime")
                        .IsRequired();

                    b.Property<int>("BusCompanyId");

                    b.Property<string>("DepertureTime")
                        .IsRequired();

                    b.Property<int>("DestinationStationId");

                    b.Property<int>("OriginStationId");

                    b.HasKey("Id");

                    b.HasIndex("BusCompanyId");

                    b.HasIndex("DestinationStationId");

                    b.HasIndex("OriginStationId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BankAccount", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Customer", "Customer")
                        .WithOne("BankAccount")
                        .HasForeignKey("BusTicketsSystem.Models.BankAccount", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BusStation", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Town", "Town")
                        .WithMany("BusStations")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Customer", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Town", "HomeTown")
                        .WithMany("Customers")
                        .HasForeignKey("HomeTownId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Review", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.BusCompany", "BusCompany")
                        .WithMany("Reviews")
                        .HasForeignKey("BusCompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusTicketsSystem.Models.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Ticket", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Customer", "Customer")
                        .WithMany("Tickets")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusTicketsSystem.Models.Trip", "Trip")
                        .WithMany("Tickets")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Trip", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.BusCompany", "BusCompany")
                        .WithMany("Trips")
                        .HasForeignKey("BusCompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BusTicketsSystem.Models.BusStation", "DestinationStation")
                        .WithMany("DestinationStationTrips")
                        .HasForeignKey("DestinationStationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusTicketsSystem.Models.BusStation", "OriginStation")
                        .WithMany("OriginStationTrips")
                        .HasForeignKey("OriginStationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
