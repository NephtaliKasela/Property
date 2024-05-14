using Property.Models.Subcategories;
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

        //public int BedRoom { get; set; }
        //public int BathRoom { get; set; }
        //public int Garage { get; set; }
        //public double Area { get; set; }
        //public bool Active { get; set; }

        // Foreign Keys
        public Country Country { get; set; }
        public City? City { get; set; }
        public SubcategoryRealEstate SubcategoryRealEstate { get; set; }
        public List<Models.Images.ProductImageRealEstate>? ProductImages { get; set; }
        public Agent Agent { get; set; }
        public RentRealEstate? Rent { get; set; }
        public SellRealEstate? Sell { get; set; }
        public List<Reservation>? Reservations { get; set; }
       
    }
}
