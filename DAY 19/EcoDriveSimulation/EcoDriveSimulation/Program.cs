namespace EcoDriveSimulation
{
    internal class Program
    {

        abstract class Vehicle
        {
            public string ModelName { get; set; }


            public abstract void Move();

            public virtual string GetFuelStatus()
            {
                return "Fuel level is stable";
            }

        }

        class ElectricCar : Vehicle
        {
            public override void Move()
            {
                //Console.WriteLine($"{ModelName} is gliding silently on battery power.");
                Console.WriteLine($"{ModelName} is gliding silently on battery power.");
            }

            public override string GetFuelStatus()
            {
                return ModelName + "battery is at 80 %.";
            }
        }

        class HeavyTruck : Vehicle
        {
            public override void Move()
            {
                Console.WriteLine($"{ModelName} is hauling cargo with high-torque diesel power.");
            }
        }

        class CargoPlane : Vehicle
        {
            public override void Move()
            {
                Console.WriteLine($"{ModelName} is ascending to 30,000 feet.");
            }

            public override string GetFuelStatus()
            {
                // Call base implementation and extend it
                return base.GetFuelStatus() + " Checking jet fuel reserves...";
            }

        }

        static void Main(string[] args)
        {
            Vehicle[] fleet =
           {
                new ElectricCar { ModelName = "Tesla Model X" },
                new HeavyTruck { ModelName = "Volvo FH16" },
                new CargoPlane { ModelName = "Boeing 747 Freighter" }
            };

            foreach (Vehicle vehicle in fleet)
            {
                vehicle.Move();
                Console.WriteLine(vehicle.GetFuelStatus());
                Console.WriteLine();
            }
        }
    }
}
