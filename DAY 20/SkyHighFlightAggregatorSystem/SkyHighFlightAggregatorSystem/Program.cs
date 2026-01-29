namespace SkyHighFlightAggregatorSystem
{
    class Flight : IComparable<Flight>
    {
        // Implementation of flight aggregation logic goes here
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime DepartureTime { get; set; }
        public int CompareTo(Flight? other)
        {
            if (other == null) return 1;
            return this.Price.CompareTo(other.Price);
        }

        public override string ToString()
        {
            return $"FlightNumber: {FlightNumber,-8}, Price: {Price,-8}, Duration: {Duration}, DepartureTime: {DepartureTime}";
        }
    }

    class DurationComparer : IComparer<Flight>
    {
        public int Compare(Flight? x, Flight? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.Duration.CompareTo(y.Duration);
        }
    }

    class DepartureComparer : IComparer<Flight>
    {
        public int Compare(Flight? x, Flight? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.DepartureTime.CompareTo(y.DepartureTime);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Flight> flights = new List<Flight>
            {
                new Flight
                {
                    FlightNumber = "SH123",
                    Price = 199.99m,
                    Duration = TimeSpan.FromHours(2),
                    DepartureTime = new DateTime(2024, 7, 1, 8, 0, 0)
                },

                new Flight
                {
                    FlightNumber = "SH456",
                    Price = 149.99m,
                    Duration = TimeSpan.FromHours(3),
                    DepartureTime = new DateTime(2024, 7, 1, 9, 0, 0)
                },
                new Flight
                {
                    FlightNumber = "SH789",
                    Price = 179.99m,
                    Duration = TimeSpan.FromHours(1.5),
                    DepartureTime = new DateTime(2024, 7, 1, 7, 30, 0)
                }
            };
            // Economical flights sorted by price
            Console.WriteLine("Economical Flights (Sorted by Price):");
            flights.Sort();
            printFlights(flights);

            // Business flights sorted by duration
            Console.WriteLine("\nBusiness Flights (Sorted by Duration):");
            flights.Sort(new DurationComparer());
            printFlights(flights);

            // Early Bird View (Earliest Departure)
            Console.WriteLine("\nEarly Bird Flights (Sorted by Departure Time):");
            flights.Sort(new DepartureComparer());
            printFlights(flights);
        }

        private static void printFlights(List<Flight> flights)
        {
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }
    }
}
