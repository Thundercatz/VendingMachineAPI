using Microsoft.AspNetCore.Mvc;
using VendingMachineAPI.Interfaces;
using VendingMachineAPI.Models;

namespace VendingMachineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendingMachineOperatorController : ControllerBase
    {
        private IVendingMachineOperator _vendingMachineOperator;

        public VendingMachineOperatorController(IVendingMachineOperator vendingMachineOperator)
        {
            _vendingMachineOperator = vendingMachineOperator;
        }



        [HttpPost("load-products")]
        public IActionResult LoadProduct([FromBody] ICollection<Product> products)
        {
            _vendingMachineOperator.LoadProducts(products);
            return Ok("Products loaded sucessfully");
        }
    }
}
