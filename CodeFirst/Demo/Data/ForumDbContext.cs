using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Data
{
    public class ForumDbContext:DbContext
    {
        public ForumDbContext()
        {
        }

        public ForumDbContext(DbContextOptions options)
            : base(options)
        {
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts{ get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //when we override metthods
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configutaion.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //one to many relations
            builder.Entity<Category>()
                .HasMany(x => x.Posts)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            builder.Entity<Post>()
                .HasMany(x => x.Replies)
                .WithOne(x => x.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);

            builder.Entity<User>()
                .HasMany(u => u.Replies)
                .WithOne(r => r.Author)
                .HasForeignKey(r => r.AuthorId);

            //Fluent API for many to many relation. Composite key
            builder.Entity<PostTag>()
                .HasKey(pt=>new { pt.PostId,pt.TagId});
            //Then > Add-Migration  >Update-Database
            //>Update-Databse -Migration:"Initial" >Remove-Migration --to start from initila migration
        }
    }
}
