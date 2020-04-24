namespace FruitWorld.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
