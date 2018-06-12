using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;
using System;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext :DbContext
    {
        public FootballBettingContext()
        {
        }

        public FootballBettingContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Color> Colors{ get; set; }

        public DbSet<Country> Countries{ get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerStatistic> PlayersStatistics { get; set; }

        public DbSet<Position> Positions{ get; set; }

        public DbSet<Team> Teams{ get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Team>(ent => 
            {
                ent.HasKey(t => t.TeamId);

                ent.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(80);


                ent.Property(t => t.Initials)
                .IsRequired()
                .HasColumnType("NCHAR(3)");

                ent.Property(t => t.LogoUrl)
               .IsUnicode(false);               

                ent.HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                ent.HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                ent.HasOne(t => t.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(t => t.TownId);
            });

            builder.Entity<Game>(ent=> 
            {
                ent.HasKey(g => g.GameId);

                ent.HasOne(t => t.HomeTeam)
                .WithMany(g => g.HomeGames)
                .HasForeignKey(t => t.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

                ent.HasOne(t => t.AwayTeam)
                .WithMany(g => g.AwayGames)
                .HasForeignKey(t => t.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Town>(ent=> 
            {
                ent.HasKey(t => t.TownId);

                ent.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(80);

                ent.HasOne(c => c.Country)
                .WithMany(t => t.Towns)
                .HasForeignKey(c => c.CountryId);
            });

            builder.Entity<Player>(ent => 
            {
                ent.HasKey(p => p.PlayerId);

                ent.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

                ent.Property(p => p.IsInjured)
                .HasDefaultValue(false);

                ent.HasOne(t => t.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(t => t.TeamId);

                ent.HasOne(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.PositionId);
            });

            builder.Entity<PlayerStatistic>(ent=>
            {
                ent.HasKey(e => new { e.GameId,e.PlayerId });

                ent.HasOne(g => g.Game)
                .WithMany(ps => ps.PlayerStatistics)
                .HasForeignKey(g => g.GameId);

                ent.HasOne(p => p.Player)
                .WithMany(ps => ps.PlayerStatistics)
                .HasForeignKey(p => p.PlayerId);
                //.OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Bet>(ent=> 
            {
                ent.HasKey(b => b.BetId);

                ent.HasOne(g => g.Game)
                .WithMany(b => b.Bets)
                .HasForeignKey(g => g.GameId);

                ent.HasOne(u => u.User)
                .WithMany(b => b.Bets)
                .HasForeignKey(u => u.UserId);

                ent.Property(b => b.Prediction)
                .IsRequired();
            });

            builder.Entity<Color>(ent => 
            {
                ent.HasKey(c => c.ColorId);

                ent.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);
            });

            builder.Entity<Country>(ent =>
            {
                ent.HasKey(c => c.CountryId);

                ent.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80);
            });

            builder.Entity<Position>(ent =>
            {
                ent.HasKey(c => c.PositionId);

                ent.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);
            });

            builder.Entity<User>(ent =>
            {
                ent.HasKey(c => c.UserId);

                ent.Property(u => u.Name)                
                .HasMaxLength(100);

                ent.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(30);

                ent.Property(u => u.Password)
                .IsRequired()
                 .HasMaxLength(20);

                ent.Property(u => u.Email)
                .IsRequired()
                 .HasMaxLength(60);

                ent.Property(u => u.Balance)
                 .HasDefaultValue(0);

            });
        }    
    
    }
}
