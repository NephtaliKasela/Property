﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Property.Models;
using Property.Models.Images;
using Property.Models.Products;
using System.Reflection.Emit;

namespace Property.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Continent> Continents => Set<Continent>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<PropertyTypeRealEstate> PropertyTypeRealEstate => Set<PropertyTypeRealEstate>();
        public DbSet<ProductRealEstate> ProductsRealEstate => Set<ProductRealEstate>();
        public DbSet<ProductImageRealEstate> ProductImagesRealEstate => Set<ProductImageRealEstate>();
        public DbSet<Agent> Agents => Set<Agent>();
        public DbSet<RentRealEstate> RentsRealEstate => Set<RentRealEstate>();
        public DbSet<RentRealEstatePerDay> rentsRealEstatePerDay => Set<RentRealEstatePerDay>();
        public DbSet<RentRealEstatePerMonth> rentsRealEstatePerMonth => Set<RentRealEstatePerMonth>();
        public DbSet<SellRealEstate> sellsRealEstate => Set<SellRealEstate>();
        public DbSet<Reservation> Reservations => Set<Reservation>();


        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//builder.HasDefaultSchema("Identity");
			//builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaims"));
			//builder.Entity<IdentityRole<string>>(entity => entity.ToTable(name: "Roles"));
			//builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaims"));
			//builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));
			//builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));

			builder.Entity<ApplicationUser>(entity => entity.ToTable(name: "Users"));

			//builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserTokens"));

			builder.Entity<ApplicationUser>()
				.HasOne(a => a.Agent)
				.WithOne(a => a.ApplicationUser)
				.HasForeignKey<Agent>();
        }
	}
}
