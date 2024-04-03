﻿using AutoMapper;
using Property.Data;
using Property.DTOs.Product;
using Property.DTOs.Product.ProductRealEstate;
using Property.Models;
using Property.Models.Products;
using Property.Services.OtherServices;
using Microsoft.EntityFrameworkCore;

namespace Property.Services.ProductService.ProductServicesRealEstate
{
    public class ProductServicesRealEstate : IProductServicesRealEstate
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOtherServices _otherServices;

        public ProductServicesRealEstate(DataContext context, IMapper mapper, IOtherServices otherServices)
        {
            _otherServices = otherServices;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetProductRealEstateDTO>>> GetAllProducts()
        {
            var products = await _context.ProductsRealEstate
                .Include(p => p.SubcategoryRealEstate)
                .Include(p => p.Store)
                .Include(p => p.Country)
                .Include(p => p.City)
                .Include(p => p.ProductImages)
                .ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetProductRealEstateDTO>>()
            {
                Data = products.Select(p => _mapper.Map<GetProductRealEstateDTO>(p)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductRealEstateDTO>> GetProductById(int id)
        {
            var serviceResponse = new ServiceResponse<GetProductRealEstateDTO>();
            try
            {
                var product = await _context.ProductsRealEstate
                    .Include(p => p.Store)
                    .Include(p => p.SubcategoryRealEstate)
                    .Include(p => p.Country)
                    .Include(p => p.City)
                    .Include(p => p.ProductImages)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (product is null) { throw new Exception($"Product with Id '{id}' not found"); }

                serviceResponse.Data = _mapper.Map<GetProductRealEstateDTO>(product);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductRealEstateDTO>>> AddProduct(AddProductRealEstateDTO newProduct)
        {
            var serviceResponse = new ServiceResponse<List<GetProductRealEstateDTO>>();
            var product = _mapper.Map<ProductRealEstate>(newProduct);

            bool result; int number;

            // Get Subcategory
            (result, number) = _otherServices.CheckIfInteger(newProduct.ProductSubCategoryId);
            if (result == true)
            {
                var subcategory = await _context.SubcategoriesRealEstate.FirstOrDefaultAsync(sc => sc.Id == number);
                if (subcategory is not null)
                {
                    product.SubcategoryRealEstate = subcategory;
                }
            }

            // Get Store
            (result, number) = _otherServices.CheckIfInteger(newProduct.StoreId);
            if (result == true)
            {
                var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == number);
                if (store is not null)
                {
                    product.Store = store;
                }
            }

			// Get Country
			(result, number) = _otherServices.CheckIfInteger(newProduct.CountryId);
			if (result == true)
			{
				var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == number);
				if (country is not null)
				{
					product.Country = country;
				}
			}

			// Get City
			(result, number) = _otherServices.CheckIfInteger(newProduct.CityId);
			if (result == true)
			{
				var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == number);
				if (city is not null)
				{
					product.City = city;
				}
			}

            product.PublicationDate = DateTime.Now;
            product.Availability = true;

			//Save product
			_context.ProductsRealEstate.Add(product);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.ProductsRealEstate
                .Select(p => _mapper.Map<GetProductRealEstateDTO>(p)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductRealEstateDTO>> UpdateProduct(UpdateProductRealEsteDTO updatedProduct)
        {
            var serviceResponse = new ServiceResponse<GetProductRealEstateDTO>();

            try
            {
                var product = await _context.ProductsRealEstate
                    .FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
                if (product is null) { throw new Exception($"Product with Id '{updatedProduct.Id}' not found"); }

                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                product.Description = updatedProduct.Description;
                // add more *******

                bool result; int IdNumber;
				// Get the Subcategory
				(result, IdNumber) = _otherServices.CheckIfInteger(updatedProduct.ProductSubCategoryId);
                if (result == true)
                {
                    var subcategory = await _context.SubcategoriesRealEstate.FirstOrDefaultAsync(sc => sc.Id == IdNumber);
                    product.SubcategoryRealEstate = subcategory;
                }

                // Get Store
                (result, IdNumber) = _otherServices.CheckIfInteger(updatedProduct.StoreId);
                if (result == true)
                {
                    var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == IdNumber);
                    product.Store = store;
                }

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetProductRealEstateDTO>(product);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductRealEstateDTO>>> DeleteProduct(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetProductRealEstateDTO>>();

            try
            {
                var product = await _context.ProductsRealEstate.FirstOrDefaultAsync(p => p.Id == id);
                if (product is null) { throw new Exception($"Product with Id '{id}' not found"); }

                _context.ProductsRealEstate.Remove(product);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.ProductsRealEstate
                    .Select(p => _mapper.Map<GetProductRealEstateDTO>(p)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
