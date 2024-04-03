﻿using Property.Models.Products;

namespace Property.DTOs.Subcategories.SubcategoryRealEstate
{
	public class AddSubcategoryRealEstateDTO
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string CategoryId { get; set; } = string.Empty;

		// Foreign Keys
		public Models.Category Category { get; set; }
	}
}
