namespace CorporatePulsePortal.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }

        public Employee(int id, string name, string position, int salary) 
        {
            Id = id;
            Name = name;
            Position = position;
            Salary = salary;
        }
    }
}
