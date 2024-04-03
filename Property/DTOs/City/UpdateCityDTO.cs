﻿using Property.Models.Products;

namespace Property.DTOs.City
{
	public class UpdateCityDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string CountryId { get; set; }
	}
}
