using AutoMapper;
using Property.Data;
using Property.Models;
using Microsoft.EntityFrameworkCore;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.Country;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Bibliography;
using Property.DTOs.Actions;
using System.Numerics;

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

        public async Task<(List<GetProductRealEstateDTO>, Search)> Filter(List<GetProductRealEstateDTO> properties, Search modelView)
        {
            if (modelView.CountryId > 0)
            {
                properties = FilterByCountry(properties, modelView);
            }
            if (modelView.CityId > 0)
            {
                (properties, modelView) = await FilterByCity(properties, modelView);
            }

            properties = FilterByCategory(properties, modelView);
            properties = FilterByPropertyType(properties, modelView);

            return (properties, modelView);
        }

        public List<GetProductRealEstateDTO> FilterByCountry(List<GetProductRealEstateDTO> properties, Search modelView)
        {
            List<GetProductRealEstateDTO> Properties = new List<GetProductRealEstateDTO>();

            if (modelView.CountryId > 0)
            {
                foreach (var property in properties)
                {
                    if (property.Country.Id == modelView.CountryId)
                    {
                        Properties.Add(property);
                    }
                }
                return Properties;
            }

            modelView.CityId = 0;
            return properties;
        }

        public async Task<(List<GetProductRealEstateDTO>, Search)> FilterByCity(List<GetProductRealEstateDTO> properties, Search modelView)
        {
            List<GetProductRealEstateDTO> Properties = new List<GetProductRealEstateDTO>();

            if (modelView.CityId > 0)
            {
                if (modelView.CountryId > 0)
                {
                    var country = await _context.Countries
                        .Include(x => x.Cities)
                        .FirstOrDefaultAsync(x => x.Id == modelView.CountryId);

                    if (country.Cities.FirstOrDefault(x => x.Id == modelView.CityId) is not null)
                    {
                        foreach (var property in properties)
                        {
                            if (property.City.Id == modelView.CityId)
                            {
                                Properties.Add(property);
                            }
                        }
                        return (Properties, modelView);
                    }
                }
            }

            modelView.CityId = 0;
            return (properties, modelView);
        }

        public List<GetProductRealEstateDTO> FilterByCategory(List<GetProductRealEstateDTO> properties, Search modelView)
        {
            List<GetProductRealEstateDTO> Properties = new List<GetProductRealEstateDTO>();

            if (modelView.Category.ToLower() != "all")
            {
                foreach (var property in properties)
                {
                    if (modelView.Category.ToLower() == "rent")
                    {
                        if (property.Rent is not null)
                        {
                            Properties.Add(property);
                        }
                    }
                    else if (modelView.Category.ToLower() == "sale")
                    {
                        if (property.Sell is not null)
                        {
                            Properties.Add(property);
                        }
                    }
                }
                return Properties;
            }

            return properties;
        }

        public List<GetProductRealEstateDTO> FilterByPropertyType(List<GetProductRealEstateDTO> properties, Search modelView)
        {
            List<GetProductRealEstateDTO> Properties = new List<GetProductRealEstateDTO>();

            if (modelView.PropertyTypeId > 0)
            {
                foreach (var property in properties)
                {
                    if (property.PropertyType.Id == modelView.PropertyTypeId)
                    {
                        Properties.Add(property);
                    }
                }
                return Properties;
            }

            return properties;
        }
    }
}