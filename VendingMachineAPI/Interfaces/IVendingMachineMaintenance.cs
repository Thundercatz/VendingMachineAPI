namespace VendingMachineAPI.Interfaces
{
    public interface IVendingMachineMaintenance
    {
        void RestockProduct(int productNumber, int quantity);
        void RemoveProduct(int productNumber);
        string GetDiagnostics();

    }
}
