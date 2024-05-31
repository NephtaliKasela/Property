using AutoMapper;
using Property.Data;
using Property.Models;
using Microsoft.EntityFrameworkCore;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.Country;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Bibliography;
using Property.DTOs.Actions;

namespace Property.Services.OtherServices
{
    public class OtherServices : IOtherServices
    {
        private readonly ApplicationDbContext _context;

        public OtherServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public (bool, int) CheckIfInteger(string number)
        {
            try
            {
                int convNumber = Convert.ToInt32(number);
                return (true, convNumber);
            }
            catch
            {
            }
            return (false, 0);
        }

        public List<GetProductRealEstateDTO> Filter(List<GetProductRealEstateDTO> properties, Search modelView)
        {
            if (modelView.CountryId > 0)
            {
                properties = FilterByCountry(properties, modelView.CountryId);
            }
            if (modelView.CityId > 0)
            {
                properties = FilterByCity(properties, modelView.CountryId, modelView.CityId);
            }

            return properties;
        }

        public List<GetProductRealEstateDTO> FilterByCountry(List<GetProductRealEstateDTO> properties, int countryId)
        {
            List <GetProductRealEstateDTO> Properties = new List<GetProductRealEstateDTO>();

            foreach (var property in properties)
            {
                if(property.Country.Id == countryId)
                {
                    Properties.Add(property);
                }
            }

            return Properties;
        }

        public List<GetProductRealEstateDTO> FilterByCity(List<GetProductRealEstateDTO> properties, int countryId, int cityId)
        {
            List<GetProductRealEstateDTO> Properties = new List<GetProductRealEstateDTO>();

            foreach (var property in properties)
            {
                if (property.Country.Id == countryId && property.City.Id == cityId)
                { 
                    Properties.Add(property);
                }
            }

            return Properties;
        }

        public List<GetProductRealEstateDTO> FilterByCategory(List<GetProductRealEstateDTO> properties, string category)
        {
            List<GetProductRealEstateDTO> Properties = new List<GetProductRealEstateDTO>();

            foreach (var property in properties)
            {
                if (category.ToLower() == "rent")
                {
                    if (property.Rent is not null)
                    {
                        Properties.Add(property);
                    }
                }
                else if (category.ToLower() == "sale")
                {
                    if (property.Sell is not null)
                    {
                        Properties.Add(property);
                    }
                }
            }

            return Properties;
        }
    }
}