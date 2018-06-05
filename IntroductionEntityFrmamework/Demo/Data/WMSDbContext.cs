using System;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Data
{
    public class WMSDbContext : DbContext
    {
        public WMSDbContext()
        {
        }

        public WMSDbContext(DbContextOptions<WMSDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<PartModel> Models { get; set; }
        public DbSet<OrderPart> OrderParts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartNeeded> PartsNeeded { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.Property(e => e.FinishDate).HasColumnType("date");

                entity.Property(e => e.IssueDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Pending')");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jobs__ClientId__37A5467C");

                entity.HasOne(d => d.Mechanic)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.MechanicId)
                    .HasConstraintName("FK__Jobs__MechanicId__38996AB5");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jobs__ModelId__34C8D9D1");
            });

            modelBuilder.Entity<Mechanic>(entity =>
            {
                entity.HasKey(e => e.MechanicId);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PartModel>(entity =>
            {
                entity.HasKey(e => e.ModelId);

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Models__737584F683254BF0")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderPart>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.PartId });

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderParts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPart__Order__3F466844");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.OrderParts)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPart__PartI__403A8C7D");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.Delivered).HasDefaultValueSql("((0))");

                entity.Property(e => e.IssueDate).HasColumnType("date");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__JobId__3B75D760");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(e => e.PartId);

                entity.HasIndex(e => e.SerialNumber)
                    .HasName("UQ__Parts__048A0008A35C2AF9")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Parts__VendorId__300424B4");
            });

            modelBuilder.Entity<PartNeeded>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.PartId });

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.PartsNeeded)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PartsNeed__JobId__44FF419A");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.PartsNeeded)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PartsNeed__PartI__45F365D3");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.VendorId);

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Vendors__737584F68E793B72")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
