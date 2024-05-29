namespace Property.DTOs.Actions
{
    public class Search
    {
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Category { get; set; } = string.Empty;
		public int PropertyTypeId { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int MinBed { get; set; }
        public int MinBath { get; set; }
        public int MinGarage { get; set; }
        public int MinArea { get; set; }
        public int MaxArea { get; set; }
    }
}
