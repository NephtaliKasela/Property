using Property.Models;
using Property.Models.Images;
using Property.Models.Products;
using Property.Models.Subcategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Property.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //public DbSet<Store> Stores => Set<Store>();
		//*******************

		
		public DbSet<Continent> Continents => Set<Continent>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubcategoryRealEstate> SubcategoriesRealEstate => Set<SubcategoryRealEstate>();
        public DbSet<TransactionType> TransactionTypes => Set<TransactionType>();
        public DbSet<ProductRealEstate> ProductsRealEstate => Set<ProductRealEstate>();
        public DbSet<ProductImageRealEstate> productImagesRealEstate => Set<ProductImageRealEstate>();
        public DbSet<Agent> Agents => Set<Agent>();


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Agent>()
				.HasOne(a => a.ApplicationUser)
				.WithOne(a => a.Agent)
				.HasForeignKey<ApplicationUser>();
		}

	}
}