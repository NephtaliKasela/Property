using AutoMapper;
using Property.Data;
using Property.DTOs.Product.ProductRealEstate;
using Property.Models;
using Property.Models.Products;
using Property.Services.OtherServices;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Property.Services.ProductService.ProductServicesRealEstate
{
    public class ProductServicesRealEstate : IProductServicesRealEstate
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOtherServices _otherServices;

        public ProductServicesRealEstate(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
        {
            _otherServices = otherServices;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetProductRealEstateDTO>>> GetAllProducts()
        {
            var products = await _context.ProductsRealEstate
                                    .Include(p => p.SubcategoryRealEstate)
                                    .Include(p => p.Country)
                                    .Include(p => p.City)
                                    .Include(p => p.Rent)
                                    .Include(p => p.Rent.RentRealEstatePerDay)
                                    .Include(p => p.Rent.RentRealEstatePerMounth)
                                    .Include(p => p.Sell)
                                    .Include(p => p.ProductImages)
                                    .Include(p => p.Agent)
                                    .ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetProductRealEstateDTO>>()
            {
                Data = products.Select(p => _mapper.Map<GetProductRealEstateDTO>(p)).ToList()
            };
            return serviceResponse;
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetProductRealEstateDTO>> GetProductById(int id)
        {
            var serviceResponse = new ServiceResponse<GetProductRealEstateDTO>();
            try
            {
                var product = await _context.ProductsRealEstate
                    .Include(p => p.SubcategoryRealEstate)
                    .Include(p => p.Country)
                    .Include(p => p.City)
                    .Include(p => p.Rent)
                    .Include(p => p.Sell)
                    .Include(p => p.ProductImages)
                    .Include(p => p.Agent)
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
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetProductRealEstateDTO>>> AddProductRentByDay(AddProductRentByDayRealEstateDTO newProduct)
        {
            var serviceResponse = new ServiceResponse<List<GetProductRealEstateDTO>>();
            var product = _mapper.Map<ProductRealEstate>(newProduct);

            bool result; int number;

            // Get Agent
            var agent = await _context.Agents.FirstOrDefaultAsync(x => x.ApplicationUser.Id == newProduct.User.Id);
            if (agent is not null)
            {
                product.Agent = agent;
            }

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

			// Add additional attributes
			var rent = new RentRealEstate();
			rent.ProductRealEstate = product;

            _context.RentsRealEstate.Add(rent);

			var rentByDay = new RentRealEstatePerDay();
            rentByDay.RentRealEstate = rent;
			rentByDay.NumberOfPoeple = newProduct.NumberOfPoeple;
			rentByDay.AdditionalPerson = newProduct.AdditionalPerson;
			rentByDay.AdditionalPrice = newProduct.AdditionalPrice;

			//Save product
			_context.rentsRealEstatePerDay.Add(rentByDay);
            await _context.SaveChangesAsync();

			//Return all products
			serviceResponse.Data = await _context.ProductsRealEstate
                .Select(p => _mapper.Map<GetProductRealEstateDTO>(p)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductRealEstateDTO>> UpdateProduct(UpdateProductRealEsteDTO updatedProduct)
        {
            var serviceResponse = new ServiceResponse<GetProductRealEstateDTO>();

            try
            {
                // Get Agent
                var agent = await _context.Agents.FirstOrDefaultAsync(x => x.ApplicationUser.Id == updatedProduct.User.Id);
                if (agent is not null)
                {
                    var product = await _context.ProductsRealEstate
                        .Include(p => p.SubcategoryRealEstate)
                        .Include(p => p.Country)
                        .Include(p => p.City)
                        .Include(p => p.Rent)
                        .Include(p => p.Sell)
                        .Include(p => p.Agent)
                        .FirstOrDefaultAsync(p => p.Id == updatedProduct.Id && p.Agent.Id == agent.Id);
                    if (product is null) { throw new Exception($"Product with Id '{updatedProduct.Id}' not found"); }

                    product.Name = updatedProduct.Name;
                    product.Description = updatedProduct.Description;
                    product.Price = updatedProduct.Price;
                    product.Room = updatedProduct.Room;
                    product.Address = updatedProduct.Address;
                    product.YearOfConstruction = updatedProduct.YearOfConstruction;

                    bool result; int number;

                    // Get Subcategory
                    (result, number) = _otherServices.CheckIfInteger(updatedProduct.ProductSubCategoryId);
                    if (result == true)
                    {
                        var subcategory = await _context.SubcategoriesRealEstate.FirstOrDefaultAsync(sc => sc.Id == number);
                        if (subcategory is not null)
                        {
                            product.SubcategoryRealEstate = subcategory;
                        }
                    }

                    // Get Country
                    (result, number) = _otherServices.CheckIfInteger(updatedProduct.CountryId);
                    if (result == true)
                    {
                        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == number);
                        if (country is not null)
                        {
                            product.Country = country;
                        }
                    }

                    // Get City
                    (result, number) = _otherServices.CheckIfInteger(updatedProduct.CityId);
                    if (result == true)
                    {
                        var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == number);
                        if (city is not null)
                        {
                            product.City = city;
                        }
                    }

                    // Get transaction type
                    (result, number) = _otherServices.CheckIfInteger(updatedProduct.TransactionTypeId);
                    if (result == true)
                    {
                        //var transactionType = await _context.TransactionTypes.FirstOrDefaultAsync(sc => sc.Id == number);
                        //if (transactionType is not null)
                        //{
                        //	product.TransactionType = transactionType;
                        //}
                    }

                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetProductRealEstateDTO>(product);
                }

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
