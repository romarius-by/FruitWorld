using System.Linq;
using Xunit;
using Moq;
using System.Collections.Generic;
using FruitWorld.Components;
using FruitWorld.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FruitWorld.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void CanSelectCategories()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category="Cat1"},
                new Product {ProductID = 2, Name = "P2", Category="Cat3"},
                new Product {ProductID = 3, Name = "P3", Category="Cat1"},
                new Product {ProductID = 4, Name = "P4", Category="Cat2"},
                new Product {ProductID = 5, Name = "P5", Category="Cat2"}
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            // Act
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            // Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Cat1", "Cat2", "Cat3" }, results));
        }

        [Fact]
        public void IndicatesSelectedCategory()
        {
            // Arrange
            string categoryToSelect = "Cat3";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category="Cat1"},
                new Product {ProductID = 4, Name = "P4", Category="Cat2"},
                new Product {ProductID = 5, Name = "P5", Category="Cat3"}
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;


            // Act
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            // Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}
