using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace CargoMenifestOptimizer
{
    public class Item
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Category { get; set; }

        public Item(string name, double weight, string category)
        {
            Name = name;
            Weight = weight;
            Category = category; 
        }
    }

    public class Container
    {
        public string ContainerId { get; set; }
        public List<Item> Items { get; private set; }

        public Container(string id, List<Item> items)
        {
            ContainerId = id;
            Items = items ?? new List<Item>(); 
        }
    }

    public class CargoManager
    {
        List<List<Container>> cargoBay;
        public CargoManager(List<List<Container>> cargoBay)
        {
            this.cargoBay = cargoBay ?? new List<List<Container>>();
        }

        // task A 
        public List<string> FindHeavyContainers(double weightThreshold)
        {
            //var heavyContainers = new List<string>();
            //foreach(var containerList in cargoBay)
            //{
            //    foreach(var container in containerList)
            //    {
            //        var totalWeight = container.Items.Sum(item => item.Weight);
            //        if(totalWeight > weightThreshold)
            //        {
            //            heavyContainers.Add(container.ContainerId);
            //        }
            //    }
            //}
            //return heavyContainers;

            return cargoBay.SelectMany(row => row)
                .Where(container => container.Items.Sum(item => item.Weight) > weightThreshold)
                .Select(container => container.ContainerId).ToList();
        }

        // Task B
        public Dictionary<string,int> GetItemCountsByCategory()
        {
            return cargoBay.Where(row => row != null)
                .SelectMany(row => row)
                .Where(container => container != null)
                .SelectMany(container => container.Items)
                .GroupBy(item => item.Category)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        // Task c
        // 1.	Flatten the List<List<Container>> into a single List<Item>.
        // 2.	Remove any duplicate items (based on Name).
        // 3.	Sort the final list first by Category (alphabetical) and then by Weight (descending).
        public List<Item> FlatternAndShipment()
        {
            return cargoBay.Where(row => row != null)
               .SelectMany(row => row)
               .Where(Container => Container != null)
               .SelectMany(Container => Container.Items)
               .GroupBy(Item => Item.Name)
               .Select(group => group.First())
               .OrderBy(Item => Item.Category)
               .ThenByDescending(Item => Item.Weight).ToList();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            //var cargoBay = new List<List<Container>>
            //{
            //    new List<Container>
            //    {
            //        new Container("C001",new List<Item>
            //        {
            //            new Item("Laptop",2.5, "Electronic"),
            //            new Item("Monitor", 5.0, "Tech"),
            //            new Item("Smartphone", 0.5, "Tech")
            //        }),
            //        new Container("C002",new List<Item>
            //        {
            //            new Item("Server Rack", 45.0, "Tech"),
            //            new Item("Cables", 1.2, "Tech")
            //        }),
            //        new Container("C003",new List<Item>
            //        {
            //             new Item("Apple", 0.2, "Food"),
            //            new Item("Banana", 0.2, "Food"),
            //            new Item("Milk", 1.0, "Food")
            //        }),
            //    },

            //    new List<Container>
            //    {
            //        new Container("C101",new List<Item>
            //        {
            //            new Item("Vase", 3.0, "Decor"),
            //            new Item("Mirror", 12.0, "Decor")
            //        }),
            //        new Container("C102", new List<Item>
            //        {
            //            new Item("Gaming Laptop", 3.2, "Tech"),
            //            new Item("Wireless Mouse", 0.2, "Accessories"),
            //            new Item("Mechanical Keyboard", 1.1, "Accessories")
            //        }),
            //        new Container("C103", new List<Item>
            //        {
            //            new Item("LED Monitor", 4.8, "Tech"),
            //            new Item("External SSD", 0.3, "Storage"),
            //            new Item("Webcam", 0.4, "Accessories")
            //        }),
            //    },

            //    new List<Container>
            //    {
            //        new Container("C201", new List<Item>
            //        {
            //            new Item("Office Chair", 9.5, "Furniture"),
            //            new Item("Wooden Desk", 25.0, "Furniture"),
            //            new Item("Desk Lamp", 2.0, "Decor")
            //        }),
            //        new Container("C202", new List<Item>
            //        {
            //            new Item("Refrigerator", 60.0, "Appliances"),
            //            new Item("Microwave Oven", 18.0, "Appliances"),
            //            new Item("Toaster", 3.5, "Appliances")
            //        }),
            //        new Container("C203", new List<Item>
            //        {
            //            new Item("Basketball", 0.6, "Sports"),
            //            new Item("Tennis Racket", 0.8, "Sports"),
            //            new Item("Yoga Mat", 1.2, "Fitness")
            //        }),
            //    },
            //};

            //var manager = new CargoManager(cargoBay);

            var cargoBay = new List<List<Container>>
            {
                // ROW 0: High-Value Tech Row
                new List<Container>
                {
                    new Container("C001", new List<Item>
                    {
                        new Item("Laptop", 2.5, "Tech"),
                        new Item("Monitor", 5.0, "Tech"),
                        new Item("Smartphone", 0.5, "Tech")
                    }),
                    new Container("C104", new List<Item>
                    {
                        new Item("Server Rack", 45.0, "Tech"), // Heavy Item
                        new Item("Cables", 1.2, "Tech")
                    })
                },

                // ROW 1: Mixed Consumer Goods
                new List<Container>
                {
                    new Container("C002", new List<Item>
                    {
                        new Item("Apple", 0.2, "Food"),
                        new Item("Banana", 0.2, "Food"),
                        new Item("Milk", 1.0, "Food")
                    }),
                    new Container("C003", new List<Item>
                    {
                        new Item("Table", 15.0, "Furniture"),
                        new Item("Chair", 7.5, "Furniture")
                    })
                },

                // ROW 2: Fragile & Perishables (Includes an Empty Container)
                new List<Container>
                {
                    new Container("C205", new List<Item>
                    {
                        new Item("Vase", 3.0, "Decor"),
                        new Item("Mirror", 12.0, "Decor")
                    }),
                    new Container("C206", new List<Item>()) // EDGE CASE: Container with no items
                },

                // ROW 3: EDGE CASE - Empty Row
                new List<Container>() // A row that exists but has no containers



            };
        }
    }
}
