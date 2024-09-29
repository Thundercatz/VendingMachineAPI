using System;
using Xunit;
using VendingMachineAPI.Models; // Adjust if the namespace is different
using VendingMachineAPI.Interfaces;
using VendingMachineAPI.Services;
using Microsoft.EntityFrameworkCore;
using VendingMachineAPI.Data;

namespace VendingMachineAPI.Tests;

public class VendingMachinePaymentTests
{


    private VendingMachineDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<VendingMachineDbContext>()
            .UseInMemoryDatabase(databaseName: "TestVendingMachineDb")
            .Options;

        return new VendingMachineDbContext(options);
    }

    [Fact]
    public void ProcessCardPayment_ShouldThrowException_WhenCardIsEmpty()
    {

        // Arrange
        var vendingMachine = new VendingMachine();
        decimal amount = 2;
        string cardNumber = string.Empty;

        // Act & Assert
        //vendingMachine.ProcessCardPayment(amount, cardNumber);

        var exception = Assert.Throws<ArgumentException>(() => vendingMachine.ProcessCardPayment(amount, cardNumber));
        Assert.Equal("Invalid card or amount.", exception.Message);


    }

    [Fact]
    public void ProcessCardPayment_ShouldSucceed_WithValidDetails()
    {
        //Arrange 
        var dbContext = GetInMemoryDbContext();
        var vendingMachine = new VendingMachine(dbContext);

        //Act 
        bool result = vendingMachine.ProcessCardPayment(10.00m, "1234-5678-9012-3456");

        // Assert
        Assert.True(result);
    }

}