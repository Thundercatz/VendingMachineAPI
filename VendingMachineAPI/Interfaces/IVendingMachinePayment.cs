namespace VendingMachineAPI.Interfaces
{
    public interface IVendingMachinePayment
    {
        bool ProcessCardPayment(decimal amount, string cardNumber);
        bool ProcessMobilePayment(decimal amount, string phoneNumber);
        bool ApplyLoyaltyPoints(int userId, decimal amount);

    }
}
