using Microsoft.AspNetCore.Mvc;
using VendingMachineAPI.Interfaces;
using VendingMachineAPI.Models;

namespace VendingMachineAPI.Controllers
{
    public class VendingMachineClientController : ControllerBase
    {
        private readonly IVendingMachineClient? _vendingMachineClient;

        public VendingMachineClientController(IVendingMachineClient vendingMachineClient)
        {
            _vendingMachineClient = vendingMachineClient;
        }


        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            return Ok(_vendingMachineClient?.Balance);
        }


        //[HttpGet("products")]
        //public IActionResult SetBalance(int balance) { }


        [HttpGet("products")]
        public IActionResult GetProducts() 
        {
            return Ok(_vendingMachineClient?.products);
        }


        [HttpPost("insert-coin")]
        public IActionResult InsertCoin([FromBody] Coin coin)
        {
            _vendingMachineClient?.InsertCoin(coin);

#pragma warning disable IDE0037 // Use inferred member name
            return Ok(new {Balance = _vendingMachineClient?.Balance});
#pragma warning restore IDE0037 // Use inferred member name
        }


        [HttpPost("buy/{productNumber}")]
        public IActionResult BuyProduct(int productNumber) 
        {
            try
            {
                var product = _vendingMachineClient?.Buy(productNumber);
                if (product == null)
                {
                    return BadRequest("Product is null");
                }


                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("return-money")]
        public IActionResult ReturnMoney()
        {
            var coins = _vendingMachineClient?.ReturnMoney();
            return Ok(new { Coins = coins, Balance = _vendingMachineClient?.Balance });
        }
    }
}
