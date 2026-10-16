namespace ProductManagement.Models
{
    public class Products
    {
        public int id { get; set;  }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }

    }
}
