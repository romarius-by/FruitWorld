using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FruitWorld.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product { Name = "Avocado", Category = "Fruit", Country = "Spain", Description = "Vkusnyatina", Price = 19M, Quantity = 1 },
                new Product { Name = "Apple", Category = "Fruit", Country = "Poland", Description = "Vkusnyatina", Price = 10M, Quantity = 4 },
                new Product { Name = "Strawberry", Category = "Berries", Country = "Belarus", Description = "Vkusnyatina", Price = 39M, Quantity = 0 },
                new Product { Name = "Orange", Category = "Fruit", Country = "Italy", Description = "Vkusnyatina", Price = 29M, Quantity = 15 }
                );
                context.SaveChanges();
            }
        }
    }
}
