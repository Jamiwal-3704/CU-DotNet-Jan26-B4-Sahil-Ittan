namespace SwiftRouteLogistics
{
    interface ILoggable
    {
        void SaveLog(string message);
    }

    class LogManager : ILoggable
    {
        string logFile = @"..\..\..\shipment_audit.log";

        public void SaveLog(string message)
        {
            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                writer.WriteLine($"{DateTime.Now:dd-MM-yyyy}  : {message}");
            }
        }
    }
    class RestrictedDestinationException : Exception
    {
        public string DeniedLocation { get; }

        public RestrictedDestinationException(string location) 
            : base($"Shipment denied to restricted destination:  {location}")
        {
            Console.WriteLine("RestrictedDestinationException triggered.");
            DeniedLocation = location;
            Console.WriteLine();
        }
    }

    class InsecurePackagingException : Exception
    {
        public InsecurePackagingException(string message): base(message) 
        {
            Console.WriteLine("InsecurePackagingException triggered.");
            Console.WriteLine(message);
            Console.WriteLine();
        }
    }

    abstract class Shipment
    {
        public string TrackingId { get; set; }
        public double Weight { get; set; }
        public string Destination { get; set; }

        private List<string> restrictedZones = new List<string>
    {
        "North Pole",
        "Unknown Island"
    };

        public bool IsRestrictedDestination(string destination)
        {
            return restrictedZones.Contains(destination);
        }
        public abstract void ProcessShipment();
    }


    class ExpressShipment : Shipment
    {
        public bool IsFragile { get; set; }
        public bool IsReinforced { get; set; }

        public override void ProcessShipment()
        {
            if (Weight <= 0)
                throw new ArgumentOutOfRangeException("Weight must be greater than zero.");

            if (IsRestrictedDestination(Destination))
                throw new RestrictedDestinationException(Destination);

            if (IsFragile && !IsReinforced)
                throw new InsecurePackagingException(
                    "Fragile shipment must have reinforced packaging."
                );

            Console.WriteLine($"Express shipment {TrackingId} processed successfully.");
        }
    }

    class HeavyFreight : Shipment
    {
        public bool HasHeavyLiftPermit { get; set; }

        public override void ProcessShipment()
        {
            if (Weight <= 0)
                throw new ArgumentOutOfRangeException("Weight must be greater than zero.");

            if (IsRestrictedDestination(Destination))
                throw new RestrictedDestinationException(Destination);

            if (Weight > 1000 && !HasHeavyLiftPermit)
                throw new Exception("Heavy Lift Permit required for shipment over 1000kg.");

            Console.WriteLine($"Heavy freight {TrackingId} processed successfully.");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            LogManager logger = new LogManager();

            List<Shipment> shipments = new()
            {
                new ExpressShipment
                {
                    TrackingId = "sahil001",
                    Weight = 100,
                    Destination = "Ahmedabad",
                    IsFragile = true,
                    IsReinforced = false
                },
                new HeavyFreight
                {
                    TrackingId = "shivam001",
                    Weight = 1500,
                    Destination = "Tamil Nadu",
                    HasHeavyLiftPermit = false
                },
                new ExpressShipment
                {
                    TrackingId = "om002",
                    Weight = -5,
                    Destination = "Palika Bazar",
                    IsFragile = true,
                    IsReinforced = false
                },
                new HeavyFreight
                {
                    TrackingId = "lovish002",
                    Weight = 500,
                    Destination = "North Pole",
                    HasHeavyLiftPermit = true
                },
                new HeavyFreight
                {
                    TrackingId = "shudanshu003",
                    Weight = 800,
                    Destination = "Rajiv Chauk",
                    HasHeavyLiftPermit = true
                }
            };

            foreach (var shipment in shipments)
            {
                try
                {
                    shipment.ProcessShipment();
                    logger.SaveLog($"SUCCESS: Shipment {shipment.TrackingId} processed.");
                }
                catch (RestrictedDestinationException ex)
                {
                    logger.SaveLog($"SECURITY ALERT: {ex.Message} | Location: {ex.DeniedLocation}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    logger.SaveLog($"DATA ENTRY ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    logger.SaveLog($"GENERAL ERROR: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"Processing attempt finished for ID: {shipment.TrackingId}");
                }
            }

            Console.WriteLine("\nAll shipments processed. Check shipment_audit.log for details.");
        }
    }
}
