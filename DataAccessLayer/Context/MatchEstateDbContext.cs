using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

                entity.HasData(
                    new PropertyType
                    {
                        Id = 1,
                        PropertyName = "Shop",
                        RgbColorForStatistics = "rgba(76, 185, 231, .9)"
                    },
                    new PropertyType
                    {
                        Id = 2,
                        PropertyName = "Land",
                        RgbColorForStatistics = "rgba(76, 185, 231, .7)"
                    },
                    new PropertyType
                    {
                        Id = 3,
                        PropertyName = "Commercial Unit",
                        RgbColorForStatistics = "rgba(76, 185, 231, .5)"
                    },
                    new PropertyType
                    {
                        Id = 4,
                        PropertyName = "Apartment",
                        RgbColorForStatistics = "rgba(76, 185, 231, .3)"
                    },
                    new PropertyType
                    {
                        Id = 5,
                        PropertyName = "Farmland",
                        RgbColorForStatistics = "rgba(76, 185, 231, .1)"
                    });
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
                entity.HasMany(c => c.Listings)
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

                var hash = new PasswordHasher<User>();

                entity.HasData(
                    new User
                    {
                        Id = "7ccbcb73-1a84-40bc-8361-0ec4dacc26ad",
                        UserName = "adminBerke",
                        NormalizedUserName = "ADMIN",
                        Email = "berke.yildirimm44@gmail.com",
                        PhoneNumber = "5537531375",
                        NameSurname = "Berke Yıldırım",
                        PasswordHash = "AQAAAAIAAYagAAAAEPwZZFryFv96B2o0ftzaEM8gLstcedFD1pQZD5GUDi/LcS+v4Ejpk0In1C61ox+/Xg==",
                        SecurityStamp = "8da61664-d01e-420f-b884-b2fbb4ffdb4d",
                        ConcurrencyStamp = "3F80B602-7C9B-476D-A1B4-8746974528D9"
                    });
            });

            modelBuilder.Entity<PropertyStatus>(entity =>
            {
                entity.ToTable("PropertyStatuses");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasData(
                    new PropertyStatus
                    {
                        Id = 1,
                        Name = "For Sale",
                        RgbColorForStatistics = "rgba(76, 185, 231, .7)"
                    },
                    new PropertyStatus
                    {
                        Id = 2,
                        Name = "For Rent",
                        RgbColorForStatistics = "rgba(76, 185, 231, .5)"
                    },
                    new PropertyStatus
                    {
                        Id = 3,
                        Name = "Daily Rent",
                        RgbColorForStatistics = "rgba(76, 185, 231, .5)"
                    });
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasData(
                        new Role
                        {
                            Id = "B3F4CA26-0C00-41D3-B595-C3F6CEC3F4C1",
                            Name = "User",
                            NormalizedName = "USER",
                            ConcurrencyStamp = "09E5C58F-F81B-45CC-A9F9-53AF119614F9"
                        },
                        new Role
                        {
                            Id = "7A903292-EE2C-448F-8F65-360C0B47262D",
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            ConcurrencyStamp = "DA95C9AF-5BC9-4EB6-A0CF-55C7280D9A24"
                        }
                    );
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
