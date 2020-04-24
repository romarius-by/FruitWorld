using System.Collections.Generic;
using System.Linq;

namespace FruitWorld.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product{Name="Avocado", Category="Fruit", Country="Spain", Description="Vkusnyatina", Price=19M, Quantity=1},
            new Product{Name="Apple", Category="Fruit", Country="Poland", Description="Vkusnyatina", Price=10M, Quantity=4},
            new Product{Name="Strawberry", Category="Berries", Country="Belarus", Description="Vkusnyatina", Price=39M, Quantity=0},
            new Product{Name="Orange", Category="Fruit", Country="Italy", Description="Vkusnyatina", Price=29M, Quantity=15}
        }.AsQueryable<Product>();
    }
}
