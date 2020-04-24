using System.Linq;
using Xunit;
using Moq;
using System.Collections.Generic;
using FruitWorld.Controllers;
using FruitWorld.Models;
using FruitWorld.Models.ViewModels;
using System;
using Microsoft.AspNetCore.Mvc;

namespace FruitWorld.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;
            Product[] prodArray = result.Products.ToArray();

            // Assert
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void CanSendPaginationViewModel()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]{
                new Product {ProductID=1, Name="P1"},
                new Product {ProductID=2, Name="P2"},
                new Product {ProductID=3, Name="P3"},
                new Product {ProductID=4, Name="P4"},
                new Product {ProductID=5, Name="P5"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //act
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;
            PagingInfo pageInfo = result.PagingInfo;

            //assert
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void CanFilterProducts()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category="Cat1"},
                new Product {ProductID = 2, Name = "P2", Category="Cat2"},
                new Product {ProductID = 3, Name = "P3", Category="Cat1"},
                new Product {ProductID = 4, Name = "P4", Category="Cat2"},
                new Product {ProductID = 5, Name = "P5", Category="Cat3"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = controller.List("Cat1", 1).ViewData.Model as ProductsListViewModel;
            Product[] prodArray = result.Products.ToArray();

            // Assert
            Assert.Equal(2, prodArray.Length);
            Assert.True(prodArray[0].Name == "P1" && prodArray[0].Category == "Cat1");
            Assert.True(prodArray[1].Name == "P3" && prodArray[1].Category == "Cat1");
        }

        [Fact]
        public void ShouldGenerateRightNumOfProductsInCategory()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category="Cat1"},
                new Product {ProductID = 2, Name = "P2", Category="Cat2"},
                new Product {ProductID = 3, Name = "P3", Category="Cat1"},
                new Product {ProductID = 4, Name = "P4", Category="Cat2"},
                new Product {ProductID = 5, Name = "P5", Category="Cat3"}
            }).AsQueryable<Product>());

            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            Func<ViewResult, ProductsListViewModel> GetModel = result => result?.ViewData?.Model as ProductsListViewModel;

            // Act
            int? res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            // Assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
