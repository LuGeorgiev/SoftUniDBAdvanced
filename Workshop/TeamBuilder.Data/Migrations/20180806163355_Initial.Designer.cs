﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamBuilder.Data;

namespace TeamBuilder.Data.Migrations
{
    [DbContext(typeof(TeamBuilderContext))]
    [Migration("20180806163355_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamBuilder.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TeamBuilder.Models.EventTeam", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<int>("TeamId");

                    b.HasKey("EventId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("EventTeams");
                });

            modelBuilder.Entity("TeamBuilder.Models.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InvitedUserId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("InvitedUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Invitaions");
                });

            modelBuilder.Entity("TeamBuilder.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acronym")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TeamBuilder.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Age");

                    b.Property<string>("FirstName")
                        .HasMaxLength(25);

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .HasMaxLength(25);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamBuilder.Models.UserTeam", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("TeamId");

                    b.HasKey("UserId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("UserTeam");
                });

            modelBuilder.Entity("TeamBuilder.Models.Event", b =>
                {
                    b.HasOne("TeamBuilder.Models.User", "Creator")
                        .WithMany("CreatedEvents")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TeamBuilder.Models.EventTeam", b =>
                {
                    b.HasOne("TeamBuilder.Models.Event", "Event")
                        .WithMany("ParticipatingEventTeams")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamBuilder.Models.Team", "Team")
                        .WithMany("Events")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamBuilder.Models.Invitation", b =>
                {
                    b.HasOne("TeamBuilder.Models.User", "InvitedUser")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("InvitedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TeamBuilder.Models.Team", "Team")
                        .WithMany("Invittaions")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamBuilder.Models.Team", b =>
                {
                    b.HasOne("TeamBuilder.Models.User", "Creator")
                        .WithMany("CreatedTeams")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TeamBuilder.Models.UserTeam", b =>
                {
                    b.HasOne("TeamBuilder.Models.Team", "Team")
                        .WithMany("UserTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamBuilder.Models.User", "User")
                        .WithMany("UserTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
