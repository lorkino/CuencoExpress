﻿// <auto-generated />
using System;
using Film.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Film.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190721184003_getme2")]
    partial class getme2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Film.Models.Images", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Img");

                    b.Property<string>("JobId");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Film.Models.Job", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<short>("Status");

                    b.Property<string>("Tittle");

                    b.Property<string>("UserCreatorId");

                    b.Property<string>("UserWorkerId");

                    b.HasKey("Id");

                    b.HasIndex("UserCreatorId");

                    b.HasIndex("UserWorkerId");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("Film.Models.JobKnowledges", b =>
                {
                    b.Property<string>("JobId");

                    b.Property<string>("KnowledgesId");

                    b.HasKey("JobId", "KnowledgesId");

                    b.HasIndex("KnowledgesId");

                    b.ToTable("JobKnowledges");
                });

            modelBuilder.Entity("Film.Models.JobPreWorker", b =>
                {
                    b.Property<string>("JobId");

                    b.Property<string>("UserPreWorkeId");

                    b.HasKey("JobId", "UserPreWorkeId");

                    b.HasIndex("UserPreWorkeId");

                    b.ToTable("JobPreWorkers");
                });

            modelBuilder.Entity("Film.Models.Knowledges", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Explanation");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Knowledges");
                });

            modelBuilder.Entity("Film.Models.Notifications", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Readed");

                    b.Property<short>("Type");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Film.Models.Suscription", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Auth");

                    b.Property<string>("Endpoint");

                    b.Property<string>("P256DH");

                    b.HasKey("Id");

                    b.ToTable("Suscriptions");
                });

            modelBuilder.Entity("Film.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Admin");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("Status");

                    b.Property<string>("SuscriptionId");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SuscriptionId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Film.Models.UserDates", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Name");

                    b.Property<string>("PersonalInfo");

                    b.Property<string>("Phone");

                    b.Property<string>("PostalCode");

                    b.Property<byte[]>("ProfileImg");

                    b.Property<double>("Score");

                    b.Property<string>("State");

                    b.Property<string>("Surname");

                    b.Property<bool>("Suscribed");

                    b.HasKey("Id");

                    b.ToTable("UserDates");
                });

            modelBuilder.Entity("Film.Models.UserKnowledges", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("KnowledgesId");

                    b.HasKey("UserId", "KnowledgesId");

                    b.HasIndex("KnowledgesId");

                    b.ToTable("UserKnowledges");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Film.Models.Images", b =>
                {
                    b.HasOne("Film.Models.Job")
                        .WithMany("JobImages")
                        .HasForeignKey("JobId");
                });

            modelBuilder.Entity("Film.Models.Job", b =>
                {
                    b.HasOne("Film.Models.User", "UserCreator")
                        .WithMany("JobsCreator")
                        .HasForeignKey("UserCreatorId");

                    b.HasOne("Film.Models.User", "UserWorker")
                        .WithMany("JobsWorker")
                        .HasForeignKey("UserWorkerId");
                });

            modelBuilder.Entity("Film.Models.JobKnowledges", b =>
                {
                    b.HasOne("Film.Models.Job", "Job")
                        .WithMany("JobKnowledges")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Film.Models.Knowledges", "Knowledges")
                        .WithMany("JobKnowledges")
                        .HasForeignKey("KnowledgesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Film.Models.JobPreWorker", b =>
                {
                    b.HasOne("Film.Models.Job", "Job")
                        .WithMany("UserPreWorker")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Film.Models.User", "UserPreWorker")
                        .WithMany("JobPreWorker")
                        .HasForeignKey("UserPreWorkeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Film.Models.Notifications", b =>
                {
                    b.HasOne("Film.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Film.Models.User", b =>
                {
                    b.HasOne("Film.Models.Suscription", "Suscription")
                        .WithMany()
                        .HasForeignKey("SuscriptionId");
                });

            modelBuilder.Entity("Film.Models.UserDates", b =>
                {
                    b.HasOne("Film.Models.User", "User")
                        .WithOne("UserDates")
                        .HasForeignKey("Film.Models.UserDates", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Film.Models.UserKnowledges", b =>
                {
                    b.HasOne("Film.Models.Knowledges", "Knowledges")
                        .WithMany("UserKnowledges")
                        .HasForeignKey("KnowledgesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Film.Models.User", "User")
                        .WithMany("UserKnowledges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Film.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Film.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Film.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Film.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
