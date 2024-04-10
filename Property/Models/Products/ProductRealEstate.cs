﻿using Property.Models.Subcategories;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property.Models.Products
{
    public class ProductRealEstate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Room { get; set; }
        public string Address { get; set; }
        public DateTime YearOfConstruction { get; set; }
        public bool Availability { get; set; }
        public DateTime PublicationDate { get; set; }

        // Foreign Keys
        public Country Country { get; set; }
        public City? City { get; set; }
        public SubcategoryRealEstate SubcategoryRealEstate { get; set; }
        public List<Models.Images.ProductImageRealEstate>? ProductImages { get; set; }
        public TransactionType TransactionType { get; set; }
        public Agent Agent { get; set; }
    }
}
