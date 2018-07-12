﻿using Employees.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Employees.Data
{
    public class EmployeesContext : DbContext
    {
        public EmployeesContext()
        {
        }
        public EmployeesContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>(ent => 
            {
                ent.Property(x => x.Address)
                .HasMaxLength(250);
            });
        }
    }
}
