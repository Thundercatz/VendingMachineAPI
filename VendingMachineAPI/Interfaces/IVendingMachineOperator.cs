using VendingMachineAPI.Models;

namespace VendingMachineAPI.Interfaces
{
    public interface IVendingMachineOperator
    {
        void LoadProducts(ICollection<Product> products);
    }
}
