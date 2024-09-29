using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineAPI.Models;
using VendingMachineAPI.Services;
using VendingMachineAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VendingMachineAPI.Tests
{
    public class VendingMachineMaintenanceTests
    {


        [Fact]
        public void RestockProduct_ShouldIncreaseProductCount()
        {
            // Arrange
            var vendingMachine = new VendingMachine();
            var products = new List<Product>
        {
            new Product { ProductNumber = 1, Name = "Soda", Price = 1.50m, Count = 5 }
        };
            vendingMachine.LoadProducts(products);

            // Act
            vendingMachine.RestockProduct(1, 10); // Restocking product with product number 1 with 10 items.

            // Assert
            Assert.Equal(15, vendingMachine.products?.First(p => p.ProductNumber == 1).Count);
        }

        [Fact]
        public void GetDiagnostics_ShouldReturnCorrectReport()
        {
            // Arrange
            var vendingMachine = new VendingMachine();
            var products = new List<Product>
            {
                new Product { ProductNumber = 1, Name = "Soda", Price = 1.50m, Count = 10 },
                new Product { ProductNumber = 2, Name = "Chips", Price = 1.00m, Count = 20 }
            };
            vendingMachine.LoadProducts(products);
            vendingMachine.InsertCoin(Coin.Euro1);
            vendingMachine.InsertCoin(Coin.Euro2);

            // Act
            var report = vendingMachine.GetDiagnostics();

            // Assert
            Assert.Contains("Total Items: 30", report);
            Assert.Contains("Total Balance: 3", report);
        }


        [Fact]
        public void RestockProduct_ShouldThrowException_WhenProductNotFound()
        {
            // Arrange
            var vendingMachine = new VendingMachine();
            var products = new List<Product>();

            vendingMachine.LoadProducts(products);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => vendingMachine.RestockProduct(99, 10)); // Product 99 doesn't exist
            Assert.Equal("Product not found.", exception.Message);
        }

        
    }

}
