﻿using System.Linq;
using FruitWorld.Models;
using Xunit;

namespace FruitWorld.Tests
{
    public class CartTests
    {
        [Fact]
        public void CanAddNewLines()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Category = "Cat1" };
            Product p2 = new Product { ProductID = 2, Name = "P2", Category = "Cat2" };
            Cart target = new Cart();
            target.AddItem(p1, 4);
            target.AddItem(p2, 1);

            //Act
            CartLine[] results = target.Lines.ToArray();

            //Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p2, results[1].Product);
            Assert.Equal(p1, results[0].Product);
        }

        [Fact]
        public void CanAddQuantityForExistingLines()
        {
            // Arrange 
            Product p1 = new Product { ProductID = 1, Name = "P1", Category = "Cat1" };
            Product p2 = new Product { ProductID = 2, Name = "P2", Category = "Cat2" };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            // Act
            CartLine[] results = target.Lines
                .OrderBy(c => c.Product.ProductID).ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void CanRemoveLine()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.Empty(target.Lines.Where(c => c.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void CalculateCartTotal()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            // Act
            decimal? result = target.ComputeTotalValue();

            // Assert
            Assert.Equal(450M, result);
        }

        [Fact]
        public void CanClearContents()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act
            target.Clear();

            // Assert
            Assert.Empty(target.Lines);
        }
    }
}
