using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineAPI.Controllers;
using VendingMachineAPI.Interfaces;

namespace VendingMachineAPI.Tests
{
    public class VendingMachineClientControllerTests
    {


        [Fact]
        public void GetBalance_ShouldReturnCorrectBalance()
        {
            // Arrange
            var mockVendingMachineClient = new Mock<IVendingMachineClient>();
            mockVendingMachineClient.Setup(vm => vm.Balance).Returns(1.50m);

            var controller = new VendingMachineClientController(mockVendingMachineClient.Object);

            // Act
            var result = controller.GetBalance();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1.50m, okResult.Value);
        }
    }
}
