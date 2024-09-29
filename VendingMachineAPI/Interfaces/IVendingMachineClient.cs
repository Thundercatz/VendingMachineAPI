using VendingMachineAPI.Models;

namespace VendingMachineAPI.Interfaces
{
    public interface IVendingMachineClient
    {
        decimal Balance { get;}
        IReadOnlyCollection<Product>? products { get; }
        void InsertCoin(Coin coin);
        ICollection<Coin> ReturnMoney();
        Product Buy(int productNumber);
    }
}
