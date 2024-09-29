using Microsoft.AspNetCore.Mvc;
using VendingMachineAPI.Interfaces;
using VendingMachineAPI.Models;

namespace VendingMachineAPI.Controllers
{
    public class VendingMachineInventoryController : ControllerBase
    {
        private IVendingMachineInventory _inventory;

        public VendingMachineInventoryController(IVendingMachineInventory inventory)
        {
            _inventory = inventory;
        }


        //IReadOnlyCollection<Product> GetLowStockProducts(int threshold = 3);
        //int GetProductStock(int productNumber);
        //bool IsProductOutOfStock(int productNumber);


        [HttpGet("low-stock/{threshold}")]
        public IActionResult GetLowStockProducts(int threshold = 3)
        {
            //return Ok(_vendingMachineClient?.products);
            var lowStockProducts = _inventory.GetLowStockProducts(threshold);

            if (lowStockProducts == null || !lowStockProducts.Any())
            {
                return NotFound("No low stock products found.");
            }

            return Ok(lowStockProducts);

        }


        [HttpGet("product-stock/{productNumber}")]
        public IActionResult GetProductStock(int productNumber)
        {

            try
            {
                var productStock = _inventory.GetProductStock(productNumber);

                return Ok(productStock);
            }
            catch (InvalidOperationException ex) // If product is not found
            {

                return NotFound(ex);
            }

            
        }


        [HttpGet("product-outofstock/{productNumber}")]
        public IActionResult IsProductOutOfStock(int productNumber)
        {
            bool isProductOutOfStock = _inventory.IsProductOutOfStock(productNumber);

            return Ok(isProductOutOfStock);
        }
    }
}
