using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Property.Controllers.Subcategories;
using Property.DTOs.Agent;
using Property.DTOs.Category;
using Property.DTOs.City;
using Property.DTOs.Continent;
using Property.DTOs.Country;
using Property.DTOs.Product;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.Reservation;
using Property.DTOs.Subcategories.SubcategoryRealEstate;
using Property.DTOs.TransactionType;
using Property.Models;
using Property.Models.Products;
using Property.Models.Subcategories;

namespace Property
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
			// Continent
			CreateMap<Continent, GetContinentDTO>();
			CreateMap<UpdateContinentDTO, Continent>();
			CreateMap<AddContinentDTO, Continent>();

            // Country
            CreateMap<Country, GetCountryDTO>();
            CreateMap<UpdateCountryDTO, Country>();
            CreateMap<AddCountryDTO, Country>();

			// City
			CreateMap<City, GetCityDTO>();
			CreateMap<UpdateCityDTO, City>();
			CreateMap<AddCityDTO, City>();

            // Category
			//CreateMap<GetCategoryDTO, Category>();
			CreateMap<AddCategoryDTO, Category>();
			CreateMap<Category, GetCategoryDTO>();
			CreateMap<UpdateCategoryDTO, Category>();

			// SubCategory Real Estate
			CreateMap<SubcategoryRealEstate, GetSubcategoryRealEstateDTO>();
			CreateMap<UpdateSubcategoryRealEstateDTO, SubcategoryRealEstate>();
			CreateMap<AddSubcategoryRealEstateDTO, SubcategoryRealEstate>();

			// Product Real Estate
			CreateMap<ProductRealEstate, GetProductRealEstateDTO>();
			CreateMap<AddProductRentByDayRealEstateDTO, ProductRealEstate>();
			CreateMap<AddProductRentByMonthRealEstateDTO, ProductRealEstate>();
			CreateMap<AddProductSellRealEstateDTO, ProductRealEstate>();
            CreateMap<UpdateProductRentByDayRealEstateDTO, ProductRealEstate>();
            CreateMap<UpdateProductRentByMonthRealEstateDTO, ProductRealEstate>();
            CreateMap<UpdateProductSellRealEstateDTO, ProductRealEstate>();

            // Transaction Type
            CreateMap<TransactionType, GetTransactionTypeDTO>();
			CreateMap<UpdateTransactionTypeDTO, TransactionType>();
			CreateMap<AddTransactionTypeDTO, TransactionType>();

            // Agent
            CreateMap<Agent, GetAgentDTO>();
            CreateMap<UpdateAgentDTO, Agent>();
            CreateMap<AddAgentDTO, Agent>();

            // Agent
            CreateMap<Reservation, GetReservationDTO>();
            CreateMap<UpdateReservationDTO, Reservation>();
            CreateMap<AddReservationDTO, Reservation>();

        }
    }
}