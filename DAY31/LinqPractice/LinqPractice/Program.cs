using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqPractice
{
    // ================= QUESTION 1 =================
    class Student
    {
        public int Id;
        public string Name;
        public string Class;
        public int Marks;

        public override string ToString()
        {
            return $"Id:{Id}, Name:{Name}, Class:{Class}, Marks:{Marks}";
        }
    }

    // ================= QUESTION 2 =================
    class Employee
    {
        public int Id;
        public string Name;
        public string Dept;
        public double Salary;
        public DateTime JoinDate;
    }

    // ================= QUESTION 3 =================
    class Product
    {
        public int Id;
        public string Name;
        public string Category;
        public double Price;
    }

    class Sale
    {
        public int ProductId;
        public int Qty;
    }

    // ================= QUESTION 4 =================
    class Book
    {
        public string Title;
        public string Author;
        public string Genre;
        public int Year;
        public double Price;
    }

    // ================= QUESTION 5 =================
    class Customer
    {
        public int Id;
        public string Name;
        public string City;
    }

    class Order
    {
        public int OrderId;
        public int CustomerId;
        public double Amount;
        public DateTime OrderDate;
    }

    // ================= QUESTION 6 =================
    class Movie
    {
        public string Title;
        public string Genre;
        public double Rating;
        public int ReleaseYear;
    }

    // ================= QUESTION 7 =================
    class Transaction
    {
        public int Acc;
        public double Amount;
        public string Type;
    }

    // ================= QUESTION 8 =================
    class CartItem
    {
        public string Name;
        public string Category;
        public double Price;
        public int Qty;
    }

    // ================= QUESTION 9 =================
    class User
    {
        public int Id;
        public string Name;
        public string Country;
    }

    class Post
    {
        public int UserId;
        public int Likes;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // ================= QUESTION 1 =================
            Console.WriteLine("================= QUESTION 1 =================");
            var students = new List<Student>
            {
                new Student{Id=1, Name="Amit", Class="10A", Marks=85},
                new Student{Id=2, Name="Neha", Class="10A", Marks=72},
                new Student{Id=3, Name="Rahul", Class="10B", Marks=90},
                new Student{Id=4, Name="Pooja", Class="10B", Marks=60},
                new Student{Id=5, Name="Kiran", Class="10A", Marks=95}
            };

            Console.WriteLine("Top 3 Students:");
            students.OrderByDescending(s => s.Marks).Take(3)
                .ToList().ForEach(Console.WriteLine);

            Console.WriteLine("\nClass Average:");
            var classAvg = students.GroupBy(s => s.Class)
                .Select(g => new { Class = g.Key, Avg = g.Average(x => x.Marks) });
            foreach (var c in classAvg)
                Console.WriteLine($"{c.Class} - {c.Avg}");

            Console.WriteLine("\nBelow Class Average:");
            var belowAvg = students.Where(s =>
                s.Marks < students.Where(x => x.Class == s.Class).Average(x => x.Marks));
            belowAvg.ToList().ForEach(Console.WriteLine);

            // ================= QUESTION 2 =================
            Console.WriteLine("================= QUESTION 2 =================");
            var employees = new List<Employee>
            {
                new Employee{Id=1, Name="Ravi", Dept="IT", Salary=80000, JoinDate=new DateTime(2019,1,10)},
                new Employee{Id=2, Name="Anita", Dept="HR", Salary=60000, JoinDate=new DateTime(2021,3,5)},
                new Employee{Id=3, Name="Suresh", Dept="IT", Salary=120000, JoinDate=new DateTime(2018,7,15)},
                new Employee{Id=4, Name="Meena", Dept="Finance", Salary=90000, JoinDate=new DateTime(2022,9,1)}
            };

            Console.WriteLine("\nSalary Stats by Department:");
            var salaryStats = employees.GroupBy(e => e.Dept)
                .Select(g => new
                {
                    Dept = g.Key,
                    Max = g.Max(e => e.Salary),
                    Min = g.Min(e => e.Salary),
                    Count = g.Count()
                });
            foreach (var s in salaryStats)
                Console.WriteLine($"{s.Dept} Max:{s.Max} Min:{s.Min} Count:{s.Count}");

            // ================= QUESTION 3 =================
            Console.WriteLine("================= QUESTION 3 =================");
            var products = new List<Product>
            {
                new Product{Id=1, Name="Laptop", Category="Electronics", Price=50000},
                new Product{Id=2, Name="Phone", Category="Electronics", Price=20000},
                new Product{Id=3, Name="Table", Category="Furniture", Price=5000}
            };

            var sales = new List<Sale>
            {
                new Sale{ProductId=1, Qty=10},
                new Sale{ProductId=2, Qty=20}
            };

            Console.WriteLine("\nProduct Revenue:");
            var revenue = products.GroupJoin(
                sales,
                p => p.Id,
                s => s.ProductId,
                (p, s) => new
                {
                    Product = p.Name,
                    Revenue = s.Sum(x => x.Qty * p.Price)
                });

            foreach (var r in revenue)
                Console.WriteLine($"{r.Product} - {r.Revenue}");

            // ================= QUESTION 4 =================
            Console.WriteLine("================= QUESTION 4 =================");
            var books = new List<Book>
            {
                new Book{Title="C# Basics", Author="John", Genre="Tech", Year=2018, Price=500},
                new Book{Title="Java Advanced", Author="Mike", Genre="Tech", Year=2016, Price=700},
                new Book{Title="History India", Author="Raj", Genre="History", Year=2019, Price=400}
            };

            Console.WriteLine("\nDistinct Authors:");
            books.Select(b => b.Author).Distinct().ToList().ForEach(Console.WriteLine);

            // ================= QUESTION 5 =================
            Console.WriteLine("================= QUESTION 5 =================");
            var customers = new List<Customer>
            {
                new Customer{Id=1, Name="Ajay", City="Delhi"},
                new Customer{Id=2, Name="Sunita", City="Mumbai"}
            };

            var orders = new List<Order>
            {
                new Order{OrderId=1, CustomerId=1, Amount=20000},
                new Order{OrderId=2, CustomerId=1, Amount=40000}
            };

            Console.WriteLine("\nCustomer Spending:");
            var spending = customers.GroupJoin(
                orders,
                c => c.Id,
                o => o.CustomerId,
                (c, o) => new { c.Name, Total = o.Sum(x => x.Amount) })
                .OrderByDescending(x => x.Total);

            foreach (var s in spending)
                Console.WriteLine($"{s.Name} - {s.Total}");

            // ================= QUESTION 6 =================
            Console.WriteLine("================= QUESTION 6 =================");
            var movies = new List<Movie>
            {
                new Movie{Title="Inception", Genre="SciFi", Rating=9, ReleaseYear=2010},
                new Movie{Title="Avatar", Genre="SciFi", Rating=8.5, ReleaseYear=2009},
                new Movie{Title="Titanic", Genre="Drama", Rating=8, ReleaseYear=1997}
            };

            Console.WriteLine("\nTop Rated Movies:");
            movies.OrderByDescending(m => m.Rating).Take(3)
                .ToList().ForEach(m => Console.WriteLine(m.Title));

            // ================= QUESTION 7 =================
            Console.WriteLine("================= QUESTION 7 =================");
            var transactions = new List<Transaction>
            {
                new Transaction{Acc=101, Amount=5000, Type="Credit"},
                new Transaction{Acc=101, Amount=2000, Type="Debit"},
                new Transaction{Acc=102, Amount=10000, Type="Debit"}
            };

            Console.WriteLine("\nAccount Balances:");
            var balances = transactions.GroupBy(t => t.Acc)
                .Select(g => new
                {
                    Acc = g.Key,
                    Balance = g.Sum(t => t.Type == "Credit" ? t.Amount : -t.Amount)
                });

            foreach (var b in balances)
                Console.WriteLine($"{b.Acc} - {b.Balance}");

            // ================= QUESTION 8 =================
            Console.WriteLine("================= QUESTION 8 =================");
            var cart = new List<CartItem>
            {
                new CartItem{Name="TV", Category="Electronics", Price=30000, Qty=1},
                new CartItem{Name="Sofa", Category="Furniture", Price=15000, Qty=1}
            };

            Console.WriteLine("\nCart Total:");
            Console.WriteLine(cart.Sum(c => c.Price * c.Qty));

            // ================= QUESTION 9 =================
            Console.WriteLine("================= QUESTION 9 =================");
            var users = new List<User>
            {
                new User{Id=1, Name="A", Country="India"},
                new User{Id=2, Name="B", Country="USA"}
            };

            var posts = new List<Post>
            {
                new Post{UserId=1, Likes=100},
                new Post{UserId=1, Likes=50}
            };

            Console.WriteLine("\nUser Likes:");
            var likes = users.GroupJoin(
                posts,
                u => u.Id,
                p => p.UserId,
                (u, p) => new { u.Name, TotalLikes = p.Sum(x => x.Likes) });

            foreach (var l in likes)
                Console.WriteLine($"{l.Name} - {l.TotalLikes}");
        }
    }
}
