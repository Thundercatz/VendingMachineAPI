using System;
using Xunit;
using VendingMachineAPI.Models; // Adjust if the namespace is different
using VendingMachineAPI.Interfaces;
using VendingMachineAPI.Services;

namespace VendingMachineAPI.Tests;

public class VendingMachineTests
{
    [Fact]
    public void InsertCoin_ShouldIncreaseBalance()
    {
        // Arrange
        var vendingMachine = new VendingMachine();

        // Act
        vendingMachine.InsertCoin(Coin.Euro1);

        // Assert
        Assert.Equal(1.00m, vendingMachine.Balance);
    }

    [Fact]
    public void BuyProduct_ShouldReduceProductCount_AndReduceBalance()
    {
        // Arrange
        var vendingMachine = new VendingMachine();
        var products = new List<Product>
        {
            new Product { ProductNumber = 1, Name = "Soda", Price = 1.50m, Count = 10 }
        };
        vendingMachine.LoadProducts(products);
        vendingMachine.InsertCoin(Coin.Euro2);

        // Act
        var product = vendingMachine.Buy(1);

        // Assert
        Assert.Equal(9, vendingMachine.products?.First(p => p.ProductNumber == 1).Count);
        Assert.Equal(0.50m, vendingMachine.Balance); // Remaining balance after buying Soda for 1.50 EUR
    }
}