using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Property.DTOs.Agent;
using Property.DTOs.City;
using Property.DTOs.Continent;
using Property.DTOs.Country;
using Property.DTOs.Product;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.PropertyTypeRealEstate;
using Property.DTOs.Reservation;
using Property.DTOs.UserApplication;
using Property.Models;
using Property.Models.Products;

namespace Property
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
			// User
			CreateMap<ApplicationUser, GetApplicationUserDTO>();

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

			// SubCategory Real Estate
			CreateMap<PropertyTypeRealEstate, GetPropertyTypeRealEstateDTO>();
			CreateMap<UpdatePropertyTypeRealEstateDTO, PropertyTypeRealEstate>();
			CreateMap<AddPropertyTypeRealEstateDTO, PropertyTypeRealEstate>();

			// Product Real Estate
			CreateMap<ProductRealEstate, GetProductRealEstateDTO>();
			CreateMap<AddProductRentByDayRealEstateDTO, ProductRealEstate>();
			CreateMap<AddProductRentByMonthRealEstateDTO, ProductRealEstate>();
			CreateMap<AddProductSellRealEstateDTO, ProductRealEstate>();
            CreateMap<UpdateProductRentByDayRealEstateDTO, ProductRealEstate>();
            CreateMap<UpdateProductRentByMonthRealEstateDTO, ProductRealEstate>();
            CreateMap<UpdateProductSellRealEstateDTO, ProductRealEstate>();

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