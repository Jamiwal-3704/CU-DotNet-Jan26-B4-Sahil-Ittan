using System.Data;

namespace ElectricalKitchenAppliance
{
    interface ITimer
    {
        public TimeOnly SetTime(int time);
    }

    interface IConnectWifi
    {
        public bool ConnectWifi(string pass);
    }

    // base class for all electrical kitchen appliances
    abstract class ElectricalKitchenAppliance 
    {
        public int ElectricalVoltage { get; set; }
        public int PowerInWatt { get; set; }
        public string ModelName { get; set; }
        public double Price { get; set; }

        public ElectricalKitchenAppliance(int electricalVoltage, int powerWatt, string modelName, double price)
            {
                ElectricalVoltage = electricalVoltage;
                PowerInWatt = powerWatt;
                ModelName = modelName;
                Price = price;
            }

        // abstract method --> must be implemented by derived classes
        abstract public void Cook();

        // virtual method --> can be overridden by derived classes, but not mandatory
        virtual public TimeOnly SetTime(int time)
        {
            Console.WriteLine($"Setting timer for {time} minutes.");
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
            return currentTime.AddMinutes(time);
        }

        virtual public bool ConnectWifi(string pass)
        {
            if(pass == "123")
            {
                Console.WriteLine("Connecting to Wi-Fi network...");
                return true;
            }
            else
            {
                Console.WriteLine("Failed to connect to Wi-Fi network. Incorrect password.");
                return false;
            }
        }
    };

    class Oven : ElectricalKitchenAppliance
    {
        
    }

    // child class 
    class Microwave : ElectricalKitchenAppliance
    {
        public TimeOnly Time { get; set; }
        public override TimeOnly SetTime(TimeOnly time)
        {
            Time = time;
            return Time;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<ElectricalKitchenAppliance> appliance = new List<ElectricalKitchenAppliance>()
            {
                new Microwave()
                {


            };
        }
    }
}
