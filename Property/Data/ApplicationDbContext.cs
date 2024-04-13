using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Property.Models;
using Property.Models.Images;
using Property.Models.Products;
using Property.Models.Subcategories;
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
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubcategoryRealEstate> SubcategoriesRealEstate => Set<SubcategoryRealEstate>();
        public DbSet<TransactionType> TransactionTypes => Set<TransactionType>();
        public DbSet<ProductRealEstate> ProductsRealEstate => Set<ProductRealEstate>();
        public DbSet<ProductImageRealEstate> productImagesRealEstate => Set<ProductImageRealEstate>();
        public DbSet<Agent> Agents => Set<Agent>();


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Agent>()
        //        .HasOne(a => a.ApplicationUser)
        //        .WithOne(a => a.Agent)
        //        .HasForeignKey<ApplicationUser>();

        //    builder.Entity<ApplicationUser>()
        //        .HasMany(a => a.OrdersRealEstate)
        //        .WithOne(a => a.ApplicationUser)
        //        .HasForeignKey(a => a.ApplicationUserId);
        //}

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
