using ProductManagement.Models;

namespace ProductManagement.Common
{
    public class DataStore
    {
        public static List<User> Users = new()
        {
            new User { Username = "admin", Password = "password123" }
        };
        public static List<Products> Products = new()
        {
            new Products { id = 1, Name = "Laptop Gaming", Description = "Laptop Core i7 8GB", Price = 15000000, CreatedAt = DateTime.UtcNow.AddDays(-2) },
            new Products { id = 2, Name = "Mouse Wireless", Description = "Mouse ergonomis tanpa kabel", Price = 250000, CreatedAt = DateTime.UtcNow.AddDays(-1) }
        };
    }
}
