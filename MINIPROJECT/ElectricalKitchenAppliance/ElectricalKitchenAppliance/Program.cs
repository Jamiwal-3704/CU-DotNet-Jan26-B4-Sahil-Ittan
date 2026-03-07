using System.Data;

namespace ElectricalKitchenAppliance
{
    //interface ITimer
    //{
    //    public TimeOnly SetTime(int time);
    //}

    //interface IConnectWifi
    //{
    //    public bool ConnectWifi(string pass);
    //}

    //// base class for all electrical kitchen appliances
    //abstract class ElectricalKitchenAppliance 
    //{
    //    public int ElectricalVoltage { get; set; }
    //    public int PowerInWatt { get; set; }
    //    public string ModelName { get; set; }
    //    public double Price { get; set; }

    //    public ElectricalKitchenAppliance(int electricalVoltage, int powerWatt, string modelName, double price)
    //        {
    //            ElectricalVoltage = electricalVoltage;
    //            PowerInWatt = powerWatt;
    //            ModelName = modelName;
    //            Price = price;
    //        }

    //    // abstract method --> must be implemented by derived classes
    //    abstract public void Cook();

    //    // virtual method --> can be overridden by derived classes, but not mandatory
    //    virtual public TimeOnly SetTime(int time)
    //    {
    //        Console.WriteLine($"Setting timer for {time} minutes.");
    //        TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
    //        return currentTime.AddMinutes(time);
    //    }

    //    virtual public bool ConnectWifi(string pass)
    //    {
    //        if(pass == "123")
    //        {
    //            Console.WriteLine("Connecting to Wi-Fi network...");
    //            return true;
    //        }
    //        else
    //        {
    //            Console.WriteLine("Failed to connect to Wi-Fi network. Incorrect password.");
    //            return false;
    //        }
    //    }
    //};

    //class Oven : ElectricalKitchenAppliance
    //{

    //}

    //// child class 
    //class Microwave : ElectricalKitchenAppliance
    //{
    //    public TimeOnly Time { get; set; }
    //    public override TimeOnly SetTime(TimeOnly time)
    //    {
    //        Time = time;
    //        return Time;
    //    }
    //}

    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        List<ElectricalKitchenAppliance> appliance = new List<ElectricalKitchenAppliance>()
    //        {
    //            new Microwave()
    //            {

    //            }
    //        };
    //    }
    //}

    interface ITimer
    {
        void SetTime(int minutes);
    }

    interface IWifiConnectable
    {
        bool ConnectWifi(string password);
    }

    abstract class ElectricalKitchenAppliance
    {
        public string ModelName { get; set; }
        public int PowerInWatt { get; set; }

        protected ElectricalKitchenAppliance(string modelName, int powerInWatt)
        {
            ModelName = modelName;
            PowerInWatt = powerInWatt;
        }

        public abstract void Cook();

        public virtual void Preheat()
        {
            Console.WriteLine("No preheating required.");
        }
    }

    class Microwave : ElectricalKitchenAppliance, ITimer
    {
        public Microwave() : base("LG Microwave", 1200) { }

        public void SetTime(int minutes)
        {
            Console.WriteLine($"Microwave timer set for {minutes} minutes.");
        }

        public override void Cook()
        {
            Console.WriteLine("Microwave cooking food.");
        }
    }

    class Oven : ElectricalKitchenAppliance, ITimer, IWifiConnectable
    {
        public Oven() : base("Samsung Oven", 2000) { }

        public void SetTime(int minutes)
        {
            Console.WriteLine($"Oven timer set for {minutes} minutes.");
        }

        public bool ConnectWifi(string password)
        {
            return password == "123";
        }

        public override void Preheat()
        {
            Console.WriteLine("Oven preheating...");
        }

        public override void Cook()
        {
            Preheat();
            Console.WriteLine("Oven cooking food.");
        }
    }

    class AirFryer : ElectricalKitchenAppliance
    {
        public AirFryer() : base("Philips AirFryer", 1500) { }

        public override void Cook()
        {
            Console.WriteLine("AirFryer cooking food quickly.");
        }
    }

    class Program
    {
        static void Main()
        {
            List<ElectricalKitchenAppliance> appliances = new()
        {
            new Microwave(),
            new Oven(),
            new AirFryer()
        };

            foreach (var appliance in appliances)
            {
                appliance.Cook();

                if (appliance is IWifiConnectable wifi)
                {
                    Console.WriteLine($"WiFi Connected: {wifi.ConnectWifi("123")}");
                }

                Console.WriteLine();
            }
        }
    }
}
