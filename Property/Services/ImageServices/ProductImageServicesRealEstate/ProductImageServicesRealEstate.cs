using AutoMapper;
using Property.Data;
using Property.DTOs.Product;
using Property.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Property.DTOs.Images;
using Property.Models.Images;

namespace Property.Services.ImageServices
{
    public class ProductImageServicesRealEstate : IProductImageServicesRealEstate
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductImageServicesRealEstate(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddProductImage(AddProductImageRealEstateDTO newProductImages)
        {
            if(newProductImages.files.Count > 0)
            {
				foreach (var file in newProductImages.files)
                {
					if (file != null && file.Length > 0)
					{
						using (var memoryStream = new MemoryStream())
						{
							file.CopyTo(memoryStream);
							var imageData = memoryStream.ToArray();

							//Get Product by id
							var product = await _context.ProductsRealEstate.FirstOrDefaultAsync(p => p.Id == newProductImages.productId);

							// Save the imageData to the database using your data access logic
							// For example, using Entity Framework Core:
							ProductImageRealEstate image = new ProductImageRealEstate
							{
								// Set other properties of the model
								ImageData = imageData,
								FileName = file.FileName,
								ContentType = GetImageContentType(file.FileName),
							};

							if (product != null)
							{
								image.ProductRealEstate = product;
							}

							// Save the model to the database
							_context.productImagesRealEstate.Add(image);
							
						}
					}
				}
				await _context.SaveChangesAsync();
			}
        }

        public async Task<ServiceResponse<List<ProductImageRealEstate>>> GetImageByProductId(int productId)
        {
            var serviceResponse = new ServiceResponse<List<ProductImageRealEstate>>();
            var images = await _context.productImagesRealEstate.Where(x => x.ProductRealEstate.Id == productId).ToListAsync();

            serviceResponse.Data = images;

            return serviceResponse;
        }


        private string GetImageContentType(string fileName)
        {
            return Path.GetExtension(fileName)?.ToLowerInvariant();

            //return string extension = Path.GetExtension(fileName)?.ToLowerInvariant();

            //switch (extension)
            //{
            //    case ".jpg":
            //    case ".jpeg":
            //        return "image/jpeg";
            //    case ".png":
            //        return "image/png";
            //    case ".gif":
            //        return "image/gif";
            //    // Add more cases for other image formats if needed
            //    default:
            //        return "application/octet-stream"; // Default to binary data if the format is unknown
            //}
        }
    }
}
