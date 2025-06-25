using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public partial class MatchEstateDbContext : IdentityDbContext<User, Role, string>
    {
        public virtual DbSet<Land> Lands { get; set; }
        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<CommercialUnit> CommercialUnits { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<PropertyListing> PropertyListings { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<PropertyRequest> PropertyRequests { get; set; }
        public virtual DbSet<Farmland> Farmlands { get; set; }
        public virtual DbSet<PropertyStatus> PropertyStatuses { get; set; }

        public MatchEstateDbContext(DbContextOptions<MatchEstateDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Land>(entity =>
            {
                entity.ToTable("Lands");

                entity.Property(e => e.Id).HasMaxLength(50);

            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("Apartments");

                entity.Property(e => e.Id).HasMaxLength(50);
                entity.Property(e => e.HeatingType).HasMaxLength(50);
                entity.Property(e => e.UsageState).HasMaxLength(50);

            });

            modelBuilder.Entity<CommercialUnit>(entity =>
            {
                entity.ToTable("CommercialUnits");

                entity.Property(e => e.Id).HasMaxLength(50);

            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shops");

                entity.Property(e => e.Id).HasMaxLength(50);
                entity.Property(e => e.HeatingType).HasMaxLength(50);
                entity.Property(e => e.UsageState).HasMaxLength(50);

            });

            modelBuilder.Entity<PropertyType>(entity =>
            {
                entity.ToTable("PropertyTypes");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.PropertyName).HasMaxLength(50);

            });

            modelBuilder.Entity<PropertyListing>(entity =>
            {
                entity.ToTable("PropertyListings");
                entity.HasKey(l => l.Id);

                entity.Property(l => l.Id).HasMaxLength(50);

                entity.HasOne(i => i.PropertyType)
                .WithMany(itt => itt.Listings)
                .HasForeignKey(i => i.PropertyTypeId);

                entity.HasOne(i => i.Land)
                .WithOne(a => a.Listing)
                .HasForeignKey<Land>(l => l.ListingId);

                entity.HasOne(i => i.Apartment)
                .WithOne(a => a.Listing)
                .HasForeignKey<Apartment>(a => a.ListingId);

                entity.HasOne(i => i.CommercialUnit)
                .WithOne(a => a.Listing)
                .HasForeignKey<CommercialUnit>(cu => cu.ListingId);

                entity.HasOne(i => i.Shop)
                .WithOne(a => a.Listing)
                .HasForeignKey<Shop>(s => s.ListingId);

                entity.HasOne(i => i.Farmland)
                .WithOne(a => a.Listing)
                .HasForeignKey<Farmland>(f => f.ListingId);

                entity.HasOne(l => l.PropertyRequest)
                .WithOne(r => r.PropertyListing)
                .HasForeignKey<PropertyListing>(l => l.PropertyRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");

                entity.Property(e => e.Id).HasMaxLength(50);
                entity.Property(e => e.NameSurname).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.HasIndex(c => c.PhoneNumber).IsUnique();
                entity.HasMany(c=>c.Listings)
                .WithOne(l => l.Client)
                .HasForeignKey(l => l.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(c => c.Requests)
                .WithOne(l => l.Client)
                .HasForeignKey(l => l.ClientId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<PropertyRequest>(entity =>
            {
                entity.ToTable("PropertyRequests");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.HasOne(t => t.PropertyType)
                .WithMany(itt => itt.Requests)
                .HasForeignKey(t => t.PropertyTypeId);
            });

            modelBuilder.Entity<Farmland>(entity =>
            {
                entity.ToTable("Farmlands");

                entity.Property(e => e.Id).HasMaxLength(50);
                entity.Property(e => e.PricePerSquareMeter).HasColumnType("decimal(18, 0)");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.Clients)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

                entity.HasMany(u => u.Listings)
                .WithOne(i => i.User)
                .HasForeignKey(s => s.UserId);

                entity.HasMany(u => u.Requests)
                .WithOne(t => t.User)
                .HasForeignKey(s => s.UserId);
            });

            modelBuilder.Entity<PropertyStatus>(entity =>
            {
                entity.ToTable("PropertyStatuses");

                entity.Property(e => e.Name).HasMaxLength(50);

            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
