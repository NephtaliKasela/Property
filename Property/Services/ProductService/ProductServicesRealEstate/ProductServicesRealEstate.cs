using AutoMapper;
using Property.Data;
using Property.DTOs.Product.ProductRealEstate;
using Property.Models;
using Property.Models.Products;
using Property.Services.OtherServices;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DocumentFormat.OpenXml.Office2010.Excel;

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
					.Include(p => p.Rent.RentRealEstatePerDay)
					.Include(p => p.Rent.RentRealEstatePerMounth)
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
        }

		public async Task<ServiceResponse<List<GetProductRealEstateDTO>>> GetProductByAgentId(int agentId)
		{
			var serviceResponse = new ServiceResponse<List<GetProductRealEstateDTO>>();
			try
			{
                var agent = await _context.Agents.FirstOrDefaultAsync(x => x.Id == agentId);
                if (agent is not null)
                {
					var product = await _context.ProductsRealEstate
					.Include(p => p.SubcategoryRealEstate)
					.Include(p => p.Country)
					.Include(p => p.City)
					.Include(p => p.Rent)
					.Include(p => p.Rent.RentRealEstatePerDay)
					.Include(p => p.Rent.RentRealEstatePerMounth)
					.Include(p => p.Sell)
					.Include(p => p.ProductImages)
					.Include(p => p.Agent)
					.Where(p => p.Agent.Id == agent.Id).ToListAsync();
					if (product is null) { throw new Exception($"Agent Product with Id '{agentId}' not found"); }

					serviceResponse.Data = _mapper.Map<List<GetProductRealEstateDTO>>(product);
				}
				
			}

			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}
			return serviceResponse;
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

		public async Task<ServiceResponse<List<GetProductRealEstateDTO>>> AddProductRentByMonth(AddProductRentByMonthRealEstateDTO newProduct)
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

			var rentByMonth = new RentRealEstatePerMonth();
			rentByMonth.RentRealEstate = rent;

			//Save product
			_context.rentsRealEstatePerMonth.Add(rentByMonth);
			await _context.SaveChangesAsync();

			//Return all products
			serviceResponse.Data = await _context.ProductsRealEstate
				.Select(p => _mapper.Map<GetProductRealEstateDTO>(p)).ToListAsync();
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetProductRealEstateDTO>>> AddProductSell(AddProductSellRealEstateDTO newProduct)
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
			var sell = new SellRealEstate();
			sell.ProductRealEstate = product;

			//Save product
			_context.sellsRealEstate.Add(sell);
			await _context.SaveChangesAsync();


			//Return all products
			serviceResponse.Data = await _context.ProductsRealEstate
				.Select(p => _mapper.Map<GetProductRealEstateDTO>(p)).ToListAsync();
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetProductRealEstateDTO>> UpdateProductRentByDay(UpdateProductRentByDayRealEstateDTO updatedProduct)
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
                        .Include(p => p.Rent.RentRealEstatePerDay)
                        .Include(p => p.Agent)
                        .FirstOrDefaultAsync(p => p.Id == updatedProduct.Id && p.Agent.Id == agent.Id);
                    if (product is null) { throw new Exception($"Product with Id '{updatedProduct.Id}' not found"); }

                    product.Name = updatedProduct.Name;
                    product.Description = updatedProduct.Description;
                    product.Price = updatedProduct.Price;
                    product.Room = updatedProduct.Room;
                    product.Address = updatedProduct.Address;
                    product.YearOfConstruction = updatedProduct.YearOfConstruction;

                    if(product.Rent != null && product.Rent.RentRealEstatePerDay != null)
                    {
                        product.Rent.RentRealEstatePerDay.NumberOfPoeple = updatedProduct.NumberOfPoeple;
                        product.Rent.RentRealEstatePerDay.AdditionalPerson = updatedProduct.AdditionalPerson;
                        product.Rent.RentRealEstatePerDay.AdditionalPrice = updatedProduct.AdditionalPrice;
                    }

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

        public async Task<ServiceResponse<GetProductRealEstateDTO>> UpdateProductRentByMonth(UpdateProductRentByMonthRealEstateDTO updatedProduct)
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
                        .Include(p => p.Rent.RentRealEstatePerMounth)
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

        public async Task<ServiceResponse<GetProductRealEstateDTO>> UpdateProductSell(UpdateProductSellRealEstateDTO updatedProduct)
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
