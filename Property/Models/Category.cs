using Property.Models.Subcategories;
using System.ComponentModel.DataAnnotations;

namespace Property.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Other properties specific to the category

        // Foreign Keys
        public List<SubcategoryRealEstate>? SubcategoriesRealEstate { get; set; }
    }
}
