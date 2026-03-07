using HeightApplication;

namespace HeightApplication
{
    class Height
    {
        // properties
        public int Feet { get; set; }
        public double Inches { get; set; }
        
        // default constructor
        public Height()
        {
            Feet = 0;
            Inches = 0.0;
        }

        // parameterized constructor
        public Height(int feet, double inches)
        {
            Feet = feet;
            Inches = inches;
        }

        // constructor with only inches
        public Height(double inches)
        {
            Feet = (int)(inches / 12);
            Inches = inches % 12;
        }

        // method to add heights
        public Height AddHeights(Height h2)
        {
            int totalFeet = this.Feet + h2.Feet;
            double totalInches = this.Inches + h2.Inches;
            // convert extra inches to feet
            if (totalInches >= 12)
            {
                totalFeet += (int)(totalInches / 12);
                totalInches = totalInches % 12;
            }
            return new Height(totalFeet, totalInches);
        }

        // override ToString()
        public override string ToString()
        {
            return $"Height - {Feet} feet {Inches} inches";
        }
    }
}


internal class Program
{
    static void Main(string[] args)
    {
        Height person1 = new Height(5, 6.5);
        Height person2 = new Height(5, 7.5);
        Height person3 = new Height(70.0); // 70 inches
        Console.WriteLine(person3); 

        // Add heights
        Height totalHeight = person1.AddHeights(person2);

        // Print results
        Console.WriteLine(person1);
        Console.WriteLine(person2);
        Console.WriteLine(totalHeight);
    }
}

