﻿using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext:DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments{ get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        //SecondTAsk
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            

            modelBuilder.Entity<Patient>(entity=> 
            {
                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.FirstName)
                //.IsRequired(true) //false if can be null
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.Property(e => e.LastName)
                //.IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.Property(e => e.Address)
                //.IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(250);

                entity.Property(e => e.Email)
                //.IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(80);

                entity.Property(e => e.HasInsurance)
               .HasDefaultValue(true);
                                
            });

            modelBuilder.Entity<Visitation>(entity => 
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.Date)
                //.IsRequired()
                .HasColumnName("VisitationDate")
                .HasColumnType("DATETIME2")
                .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Comments)
                //.IsRequired(false)
                .IsUnicode()
                .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                .WithMany(p => p.Visitations)
                .HasForeignKey(p => p.PatientId)
                .HasConstraintName("FK_Visitation_Patient");

                //Second Problem
                entity.HasOne(e => e.Doctor)
                .WithMany(d => d.Visitations)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Visitation_Doctor");

                entity.Property(e => e.DoctorId)
                .IsRequired(false);
            });

            modelBuilder.Entity<Diagnose>(entity=> 
            {
                entity.HasKey(e => e.DiagnoseId);

                entity.Property(e => e.Name)
                //.IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

                entity.Property(e => e.Comments)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
               .WithMany(p => p.Diagnoses)
               .HasForeignKey(p => p.PatientId);
               
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.Name)
                .HasMaxLength(50)
                //.IsRequired(true)
                .IsUnicode(true);
            });

            modelBuilder.Entity<PatientMedicament>(entity=> 
            {
                entity.HasKey(e => new { e.PatientId, e.MedicamentId });

                entity.HasOne(e => e.Medicament)
                .WithMany(m => m.Perscriptions)
                .HasForeignKey(e => e.MedicamentId);

                entity.HasOne(e => e.Patient)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(e => e.PatientId);
            });

            //Second Problem
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

                entity.Property(e => e.Speciality)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

                //Visitation -> one to many
            });
        }
    }
}
