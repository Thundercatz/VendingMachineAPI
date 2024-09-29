using Microsoft.EntityFrameworkCore;
using System;
using VendingMachineAPI.Data;
using VendingMachineAPI.Interfaces;
using VendingMachineAPI.Models;

namespace VendingMachineAPI.Services
{
    public class VendingMachine : 
        IVendingMachineClient, 
        IVendingMachineOperator, 
        IVendingMachineMaintenance, 
        IVendingMachineInventory,
        IVendingMachinePayment
    {

        private readonly VendingMachineDbContext _context;


        public VendingMachine() { }

        public VendingMachine(VendingMachineDbContext context)
        {
            _context = context;
        }


        private decimal _balance;
        private List<Product> _products = new List<Product>();
        private List<Coin> _insertedCoins = new List<Coin>();

        public decimal Balance => _balance;

        public IReadOnlyCollection<Product>? products => _products.AsReadOnly();

        public Product Buy(int productNumber)
        {
            //var product = _products.FirstOrDefault(p => p.ProductNumber == productNumber);
            var product = _context.Products.FirstOrDefault(p => p.ProductNumber == productNumber);


            if (product.Equals(default(Product)))
            {
                throw new InvalidOperationException("Product not found.");
            }

            if (product.Count <= 0)
            {
                throw new InvalidOperationException("Product out of stock");
            }
            if (_balance < product.Price)
            {
                throw new InvalidOperationException("Insufficient balance");
            }

            _balance -= product.Price;

            //updateProduct = product;
            var updatedProduct = product;
            updatedProduct.Count--;
            _products[_products.IndexOf(product)] = updatedProduct;

            return product;
            

        }

       

        public void InsertCoin(Coin coin)
        {
            _balance += (decimal)coin / 100;
            _insertedCoins.Add(coin);
        }

        public void LoadProducts(ICollection<Product> products)
        {
            foreach (var product in products)
            {
                //var existingProduct = _products.FirstOrDefault(p => p.Name == product.Name);
                var existingProduct = _context.Products.FirstOrDefault(p => p.Name == product.Name);

                ////changed product from struct to class
                //if (!existingProduct.Equals(default(Product)))
                if (existingProduct != null)
                {
                    var updatedProduct = existingProduct;
                    updatedProduct.Count += product.Count;

                    _products[_products.IndexOf(existingProduct)] = updatedProduct;
                    _context.Products.Update(existingProduct);
                }
                else
                {
                    _products.Add(product);
                    _context.Products.Add(product);
                }
            }

            _context.SaveChanges();
        }

        

        public ICollection<Coin> ReturnMoney()
        {
            var returnedCoins = new List<Coin>(_insertedCoins);
            _balance = 0;
            _insertedCoins.Clear();
            return returnedCoins;
        }


        public void RemoveProduct(int productNumber)
        {
            //var product = _products.FirstOrDefault(p => p.ProductNumber == productNumber);
            var product = _context.Products.FirstOrDefault(p => p.ProductNumber == productNumber);

            if (product.Equals(default(Product)))
            {
                throw new InvalidOperationException("Product not found");
            }

            _products.Remove(product);
            _context.Products.Remove(product);


            _context.SaveChanges(); // Commit changes to the database

        }

        //public void RestockProduct(int productNumber, int quantity)
        //{
        //    //var product = _products.FirstOrDefault(p => p.ProductNumber == productNumber);

        //    //if (product.Equals(default(Product)))
        //    //{
        //    //    throw new InvalidOperationException("Product not found");
        //    //}

        //    ////update product count
        //    //product.Count += quantity;
        //    //_products[_products.IndexOf(product)] = product;

        //    //var index = _products.FindIndex(p => p.ProductNumber == productNumber);
        //    var index = _products.FindIndex(p => p.ProductNumber == productNumber);

        //    if (index == -1)
        //    {
        //        throw new InvalidOperationException("Product not found");
        //    }

        //    // modify the product at that index
        //    var product = _products[index];
        //    product.Count += quantity;

        //    // Update the product in the list
        //    _products[index] = product;
        //    _context.Products.Update(product);

        //    _context.SaveChanges(); // Commit changes to the database

        //}

        public void RestockProduct(int productNumber, int quantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductNumber == productNumber);

            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            product.Count += quantity;
            _context.Products.Update(product);
            _context.SaveChanges(); // Commit changes to the database
        }

        public string GetDiagnostics()
        {
            var totalItems = _products.Sum(p => p.Count);
            var totalBalance = _balance;
            var report = $"Total Items: {totalItems}, Total Balance: {totalBalance}";
            return report;
        }

        public IReadOnlyCollection<Product> GetLowStockProducts(int threshold = 3)
        {
            //var lowStockProducts = _products.FindAll(_products => _products.Count <= 3);
            //return lowStockProducts;

            // Find products with a count <= 3
            //var lowStockProducts = _products.FindAll(product => product.Count <= threshold);
            var lowStockProducts = _context.Products.Where(product => product.Count <= threshold).ToList();

            // Return as a read-only collection to prevent modifications
            return lowStockProducts.AsReadOnly();
        }

        public Product GetProductStock(int productNumber)
        {
            //var productStock = _products.FirstOrDefault(p =>p.ProductNumber == productNumber);
            var productStock = _context.Products.FirstOrDefault(p =>p.ProductNumber == productNumber);

            //if (productStock.Equals(default(Product)))
            if (productStock == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            return productStock;


        }

        public bool IsProductOutOfStock(int productNumber)
        {
            //var product = _products.FirstOrDefault(p => p.ProductNumber == productNumber);

            //if (product.Equals(default(Product)))
            //{
            //    throw new InvalidOperationException("Product not found");
            //}

            //if (product.Count <= 0) 
            //{ 
            //    return true;
            //}

            //return false;

            var product = _context.Products.FirstOrDefault(p=> p.ProductNumber == productNumber);

            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            return product.Count <= 0;
        }

        public bool ProcessCardPayment(decimal amount, string cardNumber)
        {
            // Simulate card payment processing
            // For simplicity, we'll assume card processing always succeeds.
            // You can expand this by adding actual logic (e.g., external API calls).
            if (string.IsNullOrEmpty(cardNumber) || amount <= 0)
            {
                throw new ArgumentException("Invalid card or amount.");
            }

            Console.WriteLine($"Processing card payment of {amount:C} with card number {cardNumber}.");
            return true; // Simulate successful card payment

        }

        public bool ProcessMobilePayment(decimal amount, string phoneNumber)
        {
            // Simulate mobile payment processing
            if (string.IsNullOrEmpty(phoneNumber) || amount <= 0)
            {
                throw new ArgumentException("Invalid phone number or amount.");
            }

            Console.WriteLine($"Processing mobile payment of {amount:C} from phone number {phoneNumber}.");
            return true; // Simulate successful mobile payment
        }

        public bool ApplyLoyaltyPoints(int userId, decimal amount)
        {
            // Simulate applying loyalty points
            if (userId <= 0 || amount <= 0)
            {
                throw new ArgumentException("Invalid user ID or amount.");
            }

            Console.WriteLine($"Applying {amount:C} worth of loyalty points for user ID {userId}.");
            return true; // Simulate successful loyalty points application
        }
    }
}
