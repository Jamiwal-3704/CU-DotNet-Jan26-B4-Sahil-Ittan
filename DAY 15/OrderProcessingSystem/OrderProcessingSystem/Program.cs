
using System;

namespace OrderProcessingSystem
{
    class Order
    {

        private int _orderId;
        private string _customerName;
        private decimal _totalAmount;
        private DateTime _orderDate;
        private string _status;
        private bool _discountApplied;


        // Default Constructor
        public Order()
        {
            _orderDate = DateTime.Now;
            _status = "NEW";
            _totalAmount = 0;
            _discountApplied = false;
        }

        // Parameterized Constructor
        public Order(int orderId, string customerName) : this()
        {
            _orderId = orderId;

            if (!string.IsNullOrWhiteSpace(customerName))
            {
                _customerName = customerName;
            }
        }

        // Read-only
        public int OrderId
        {
            get { return _orderId; }
        }

        // Read / Write with validation
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _customerName = value;
                }
            }
        }

        // Read-only (updated internally)
        public decimal TotalAmount
        {
            get { return _totalAmount; }
        }

        public void AddItem(decimal price)
        {
            if (price > 0)
            {
                _totalAmount += price;
            }
        }

        public void ApplyDiscount(decimal percentage)
        {
            if (_discountApplied)
                return;

            if (percentage >= 1 && percentage <= 30)
            {
                decimal discount = (_totalAmount * percentage) / 100;
                _totalAmount -= discount;
                _discountApplied = true;
            }
        }

        public string GetOrderSummary()
        {
            return
                $"Order Id: {_orderId}\n" +
                $"Customer: {_customerName}\n" +
                $"Total Amount: {_totalAmount}\n" +
                $"Status: {_status}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Using parameterized constructor
            Order order = new Order(101, "Rahul");

            // Hard-coded prices (as per sample)
            order.AddItem(500);
            order.AddItem(300);

            // Apply discount
            order.ApplyDiscount(10);

            // Display result
            Console.WriteLine(order.GetOrderSummary());
        }
    }
}


//using System;
//using System.Diagnostics;

//namespace OrderProcessingSystem
//{
//    class Order
//    {

//        // Default constructor
//        public Order()
//        {
//            _status = "NEW";
//            _totalAmount = 0;
//            _discountApplied = false;
//        }

//        // parameterized constructor
//        public Order(int orderId, string customerName)
//        {
//            _orderId = orderId;
//            _customerName = customerName;
//        }

//        // Encapsulation 
//        private int _orderId;
//        private string _customerName;
//        private decimal _totalAmount;
//        private string _status;
//        private bool _discountApplied;

//        // Instance Properties
//        public int OrderId
//        {
//            get { return _orderId; }
//        }

//        public string CustomerName
//        {
//            get { return _customerName; }
//            set
//            {
//                if (!string.IsNullOrEmpty(value))
//                {
//                    _customerName = value;
//                }
//            }
//        }

//        public decimal TotalAmount
//        {
//            get { return _totalAmount; }
//        }

//        public void AddItem(decimal price)
//        {
//            if(price < 0)
//            {
//                Console.WriteLine("price cannot be negative... please re-enter the price: ");
//            }
//            _totalAmount += price;
//        }

//        public void ApplyDiscount(decimal percentage)
//        {
//            if(percentage > 0 || percentage < 30)
//            {
//                if(_discountApplied)
//                {
//                    return;
//                }

//                decimal discountAmount =  (_totalAmount * percentage) / 100;
//                _totalAmount = _totalAmount - discountAmount;
//                _discountApplied = true;

//            }
//        }

//        public string GetOrderSummary()
//        {
//            return
//                $"Order Id: {_orderId}\n" +
//                $"Customer: {_customerName}\n" +
//                $"Total Amount: {_totalAmount}\n" +
//                $"Status: {_status}";
//        }

//    }
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Order order1 = new Order(101,"");

//            while(string.IsNullOrWhiteSpace(order1.CustomerName))
//            {
//                Console.Write("Enter customer name: ");
//                string name = Console.ReadLine();
//                order1.CustomerName = name;

//                if(string.IsNullOrWhiteSpace(order1.CustomerName))
//                {
//                    Console.WriteLine("Name cannot be empty");
//                }
//            }

//            Console.WriteLine("Number of Items want to add: ");
//            int countItem = int.Parse(Console.ReadLine());

//            for (int i = 0; i < countItem; i++)
//            {
//                decimal price = 0;
//                while (price <= 0)
//                {
//                    if (price < 0)
//                    {
//                        Console.WriteLine("price must be greater then 0");
//                    }
//                    Console.Write($"Enter price of item {i + 1}: ");
//                    decimal.TryParse(Console.ReadLine(), out price);
//                }

//                order1.AddItem(price);
//            }

//            // Discount input
//            Console.Write("Enter discount percentage (1–30): ");
//            decimal discount;
//            decimal.TryParse(Console.ReadLine(), out discount);

//            order1.ApplyDiscount(discount);

//            // Display result
//            Console.WriteLine("\nOrder Summary:");
//            Console.WriteLine(order1.GetOrderSummary());
//        }
//    }
//}