using System.Globalization;

namespace OlaDriverRideManager
{
    public class OlaDriver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VehicleNo { get; set; }

        // Driver can take multiple rides
        public List<Ride> Rides { get; set; } = new List<Ride>();

        // Add a ride to the driver
        public void AddRide(Ride ride)
        {
            Rides.Add(ride);
        }

        // Calculate total fare collected by driver
        public decimal GetTotalFare()
        {
            return Rides.Sum(r => r.Fare);
        }

        // Display driver-wise rides and total fare
        public void DisplayDriverReport()
        {
            Console.WriteLine($"Driver ID   : {Id}");
            Console.WriteLine($"Name        : {Name}");
            Console.WriteLine($"Vehicle No : {VehicleNo}");
            Console.WriteLine("Rides:");

            if (Rides.Count == 0)
            {
                Console.WriteLine("  No rides taken.");
            }
            else
            {
                foreach (Ride ride in Rides)
                {
                    Console.WriteLine("  " + ride);
                }
            }

            Console.WriteLine($"Total Fare Collected: {GetTotalFare():C2}");
            Console.WriteLine("-------------------------------------------");
        }
    }

    public class Ride
    {
        public int RideID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Fare { get; set; }

        public override string ToString()
        {
            return $"RideID: {RideID}, From: {From}, To: {To}, Fare: {Fare:C2}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<OlaDriver> drivers = new List<OlaDriver>();

            // Driver 1
            OlaDriver driver1 = new OlaDriver
            {
                Id = 1,
                Name = "Sahil",
                VehicleNo = "MH12AB1234"
            };

            driver1.AddRide(new Ride
            {
                RideID = 101,
                From = "Pune",
                To = "Mumbai",
                Fare = 1500
            });

            driver1.AddRide(new Ride
            {
                RideID = 102,
                From = "Mumbai",
                To = "Navi Mumbai",
                Fare = 500
            });

            // Driver 2
            OlaDriver driver2 = new OlaDriver
            {
                Id = 2,
                Name = "Rohit",
                VehicleNo = "MH14XY5678"
            };

            driver2.AddRide(new Ride
            {
                RideID = 201,
                From = "Delhi",
                To = "Noida",
                Fare = 600
            });

            driver2.AddRide(new Ride
            {
                RideID = 202,
                From = "Noida",
                To = "Gurgaon",
                Fare = 800
            });

            // Add drivers to list
            drivers.Add(driver1);
            drivers.Add(driver2);

            // Display driver-wise rides and total fare
            Console.WriteLine("====== OLA DRIVER RIDE REPORT ======\n");

            foreach (OlaDriver driver in drivers)
            {
                driver.DisplayDriverReport();
            }

            Console.ReadLine();
        }
    }
}
