using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.Data.Configuration;
using TeamBuilder.Models;

namespace TeamBuilder.Data
{
    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext()
        {
        }
        public TeamBuilderContext( DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Invitation> Invitaions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(ConnectionConfiguration.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventTeamConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTeamConfiguration());
        }
    }
}
