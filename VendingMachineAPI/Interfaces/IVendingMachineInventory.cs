
using VendingMachineAPI.Models;

namespace VendingMachineAPI.Interfaces

{
    public interface IVendingMachineInventory
    {

        IReadOnlyCollection<Product> GetLowStockProducts(int threshold = 3);
        Product GetProductStock(int productNumber);
        bool IsProductOutOfStock(int productNumber);
    }
}
