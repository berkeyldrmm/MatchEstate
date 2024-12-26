﻿// <auto-generated />
using System;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(MatchEstateDbContext))]
    [Migration("20241225190035_client-phonenumber-mig")]
    partial class clientphonenumbermig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityLayer.Entities.Apartment", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AgeOfBuilding")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlockNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dues")
                        .HasColumnType("int");

                    b.Property<int?>("Floor")
                        .HasColumnType("int");

                    b.Property<bool>("HasElevator")
                        .HasColumnType("bit");

                    b.Property<bool>("HasParkingLot")
                        .HasColumnType("bit");

                    b.Property<string>("HeatingType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsEligibleForLoan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFurnished")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInResidentalComplex")
                        .HasColumnType("bit");

                    b.Property<string>("ListingId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("NumberOfBalcony")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfBathRooms")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfFloors")
                        .HasColumnType("int");

                    b.Property<string>("NumberOfRooms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParselNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PricePerSquareMeter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SquareMeterSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SquareMeterSizeGross")
                        .HasColumnType("int");

                    b.Property<string>("UsageState")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ListingId")
                        .IsUnique();

                    b.ToTable("Apartments", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.Client", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NameSurname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.CommercialUnit", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BlockNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ParselNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PricePerSquareMeter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SquareMeterSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ListingId")
                        .IsUnique();

                    b.ToTable("CommercialUnits", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.Farmland", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BlockNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ParselNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PricePerSquareMeter")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<string>("SheetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SquareMeterSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TitleDeedState")
                        .HasColumnType("bit");

                    b.Property<bool>("ZoningStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ListingId")
                        .IsUnique();

                    b.ToTable("Farmlands", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.Land", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BlockNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LandShareEligibility")
                        .HasColumnType("bit");

                    b.Property<string>("ListingId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ParselNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PricePerSquareMeter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SheetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SquareMeterSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TitleSheetState")
                        .HasColumnType("bit");

                    b.Property<bool>("ZoningStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ListingId")
                        .IsUnique();

                    b.ToTable("Lands", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.PropertyListing", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Commission")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Earning")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IsForSaleOrRent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSoldOrRented")
                        .HasColumnType("bit");

                    b.Property<string>("Neighbourhood")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PropertyTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("PropertyTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("PropertyListings", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.PropertyRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsForSaleOrRent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MaximumPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinimumPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumberOfRooms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("PropertyTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("PropertyRequests", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.PropertyType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("PropertyTypes", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.Shop", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AgeOfBuilding")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlockNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dues")
                        .HasColumnType("int");

                    b.Property<int?>("Floor")
                        .HasColumnType("int");

                    b.Property<bool>("HasElevator")
                        .HasColumnType("bit");

                    b.Property<bool>("HasParkingLot")
                        .HasColumnType("bit");

                    b.Property<string>("HeatingType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsEligibleForLoan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFurnished")
                        .HasColumnType("bit");

                    b.Property<string>("ListingId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("NumberOfFloors")
                        .HasColumnType("int");

                    b.Property<string>("NumberOfRooms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParselNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PricePerSquareMeter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SquareMeterSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SquareMeterSizeGross")
                        .HasColumnType("int");

                    b.Property<string>("UsageState")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ListingId")
                        .IsUnique();

                    b.ToTable("Shops", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("IncomeExpenses")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NameSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tasks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EntityLayer.Entities.Apartment", b =>
                {
                    b.HasOne("EntityLayer.Entities.PropertyListing", "Listing")
                        .WithOne("Apartment")
                        .HasForeignKey("EntityLayer.Entities.Apartment", "ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");
                });

            modelBuilder.Entity("EntityLayer.Entities.Client", b =>
                {
                    b.HasOne("EntityLayer.Entities.User", "User")
                        .WithMany("Clients")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EntityLayer.Entities.CommercialUnit", b =>
                {
                    b.HasOne("EntityLayer.Entities.PropertyListing", "Listing")
                        .WithOne("CommercialUnit")
                        .HasForeignKey("EntityLayer.Entities.CommercialUnit", "ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");
                });

            modelBuilder.Entity("EntityLayer.Entities.Farmland", b =>
                {
                    b.HasOne("EntityLayer.Entities.PropertyListing", "Listing")
                        .WithOne("Farmland")
                        .HasForeignKey("EntityLayer.Entities.Farmland", "ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");
                });

            modelBuilder.Entity("EntityLayer.Entities.Land", b =>
                {
                    b.HasOne("EntityLayer.Entities.PropertyListing", "Listing")
                        .WithOne("Land")
                        .HasForeignKey("EntityLayer.Entities.Land", "ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");
                });

            modelBuilder.Entity("EntityLayer.Entities.PropertyListing", b =>
                {
                    b.HasOne("EntityLayer.Entities.Client", "Client")
                        .WithMany("Listings")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EntityLayer.Entities.PropertyType", "PropertyType")
                        .WithMany("Listings")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Entities.User", "User")
                        .WithMany("Listings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("PropertyType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EntityLayer.Entities.PropertyRequest", b =>
                {
                    b.HasOne("EntityLayer.Entities.Client", "Client")
                        .WithMany("Requests")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EntityLayer.Entities.PropertyType", "PropertyType")
                        .WithMany("Requests")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Entities.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("PropertyType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EntityLayer.Entities.Shop", b =>
                {
                    b.HasOne("EntityLayer.Entities.PropertyListing", "Listing")
                        .WithOne("Shop")
                        .HasForeignKey("EntityLayer.Entities.Shop", "ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("EntityLayer.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EntityLayer.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EntityLayer.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("EntityLayer.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EntityLayer.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityLayer.Entities.Client", b =>
                {
                    b.Navigation("Listings");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("EntityLayer.Entities.PropertyListing", b =>
                {
                    b.Navigation("Apartment");

                    b.Navigation("CommercialUnit");

                    b.Navigation("Farmland");

                    b.Navigation("Land");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("EntityLayer.Entities.PropertyType", b =>
                {
                    b.Navigation("Listings");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("EntityLayer.Entities.User", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Listings");

                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
