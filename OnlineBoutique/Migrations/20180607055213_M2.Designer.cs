﻿// <auto-generated />
using MarketCore.EmunsAndConst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OnlineBoutique.Data;
using System;

namespace OnlineBoutique.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180607055213_M2")]
    partial class M2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MarketCore.Classes.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DressType");

                    b.Property<int?>("Gender");

                    b.Property<int?>("Style");

                    b.Property<int?>("Year");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("MarketCore.Classes.ColorVariation", b =>
                {
                    b.Property<int>("ColorVariationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.HasKey("ColorVariationId");

                    b.ToTable("ColorVariation");
                });

            modelBuilder.Entity("MarketCore.Classes.FilePath", b =>
                {
                    b.Property<int>("FilePathId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ColorVariationId");

                    b.Property<string>("Path");

                    b.HasKey("FilePathId");

                    b.HasIndex("ColorVariationId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("MarketCore.Classes.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerId");

                    b.Property<int?>("OrderStatus");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MarketCore.Classes.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand");

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("FabricStructure");

                    b.Property<string>("Name");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MarketCore.Classes.ProductVariation", b =>
                {
                    b.Property<int>("ProductVariationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BaseProductProductId");

                    b.Property<int?>("ColorVariationId");

                    b.Property<double>("Price");

                    b.HasKey("ProductVariationId");

                    b.HasIndex("BaseProductProductId");

                    b.HasIndex("ColorVariationId");

                    b.ToTable("ProductVariations");
                });

            modelBuilder.Entity("MarketCore.Classes.Size", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("MaxValue");

                    b.Property<int?>("SizeVariationId");

                    b.Property<int?>("TypeSize");

                    b.Property<double?>("Value");

                    b.HasKey("SizeId");

                    b.HasIndex("SizeVariationId");

                    b.ToTable("Size");
                });

            modelBuilder.Entity("MarketCore.Classes.SizeVariation", b =>
                {
                    b.Property<int>("SizeVariationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountInStore");

                    b.Property<int?>("NamedSize");

                    b.Property<int?>("ProductVariationId");

                    b.HasKey("SizeVariationId");

                    b.HasIndex("ProductVariationId");

                    b.ToTable("SizeVariation");
                });

            modelBuilder.Entity("MarketCore.Classes.UserSizes", b =>
                {
                    b.Property<int>("UserSizesId")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Breast");

                    b.Property<double?>("Height");

                    b.Property<double?>("ShouldersWidth");

                    b.Property<double?>("Thigh");

                    b.Property<double?>("ThighGirth");

                    b.Property<double?>("Waist");

                    b.HasKey("UserSizesId");

                    b.ToTable("UserSizes");
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
                        .ValueGeneratedOnAdd();

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
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("OnlineBoutique.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

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

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int?>("UserSizesId");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserSizesId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("OnlineBoutique.Models.Classes.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int?>("OrderId");

                    b.Property<int?>("ProductVariationId");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductVariationId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("MarketCore.Classes.FilePath", b =>
                {
                    b.HasOne("MarketCore.Classes.ColorVariation", "ColorVariation")
                        .WithMany("ImageURLs")
                        .HasForeignKey("ColorVariationId");
                });

            modelBuilder.Entity("MarketCore.Classes.Order", b =>
                {
                    b.HasOne("OnlineBoutique.Models.ApplicationUser", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("MarketCore.Classes.Product", b =>
                {
                    b.HasOne("MarketCore.Classes.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("MarketCore.Classes.ProductVariation", b =>
                {
                    b.HasOne("MarketCore.Classes.Product", "BaseProduct")
                        .WithMany("ProductVariations")
                        .HasForeignKey("BaseProductProductId");

                    b.HasOne("MarketCore.Classes.ColorVariation", "ColorVariation")
                        .WithMany()
                        .HasForeignKey("ColorVariationId");
                });

            modelBuilder.Entity("MarketCore.Classes.Size", b =>
                {
                    b.HasOne("MarketCore.Classes.SizeVariation", "SizeVariation")
                        .WithMany("ListParams")
                        .HasForeignKey("SizeVariationId");
                });

            modelBuilder.Entity("MarketCore.Classes.SizeVariation", b =>
                {
                    b.HasOne("MarketCore.Classes.ProductVariation", "ProductVariation")
                        .WithMany("SizeVariation")
                        .HasForeignKey("ProductVariationId");
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
                    b.HasOne("OnlineBoutique.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OnlineBoutique.Models.ApplicationUser")
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

                    b.HasOne("OnlineBoutique.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OnlineBoutique.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineBoutique.Models.ApplicationUser", b =>
                {
                    b.HasOne("MarketCore.Classes.UserSizes", "UserSizes")
                        .WithMany()
                        .HasForeignKey("UserSizesId");
                });

            modelBuilder.Entity("OnlineBoutique.Models.Classes.OrderItem", b =>
                {
                    b.HasOne("MarketCore.Classes.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("MarketCore.Classes.ProductVariation", "ProductVariation")
                        .WithMany()
                        .HasForeignKey("ProductVariationId");
                });
#pragma warning restore 612, 618
        }
    }
}
