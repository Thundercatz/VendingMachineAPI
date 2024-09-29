using Microsoft.AspNetCore.Mvc;
using VendingMachineAPI.Interfaces;
using VendingMachineAPI.Models;

namespace VendingMachineAPI.Controllers
{
    public class VendingMachineMaintenanceController : ControllerBase
    {
        private IVendingMachineMaintenance _vendingMachineMaintenance;

        public VendingMachineMaintenanceController(IVendingMachineMaintenance vendingMachineMaintenance)
        {
            _vendingMachineMaintenance = vendingMachineMaintenance;
        }

        [HttpPost("restock")]
        public IActionResult RestockProduct([FromBody] int productNumber, int quantity)
        {
            try
            {
                _vendingMachineMaintenance.RestockProduct(productNumber, quantity);
                return Ok("Product restocked sucessfully");
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{productNumber}")]
        public IActionResult RemoveProduct(int productNumber)
        {
            try
            {
                _vendingMachineMaintenance.RemoveProduct(productNumber);
                return Ok("Product removed successfully.");

            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("diagnostics")]
        public IActionResult GetDiagnostic()
        {
            var report = _vendingMachineMaintenance.GetDiagnostics();
            return Ok(report);
        }

    }
}
