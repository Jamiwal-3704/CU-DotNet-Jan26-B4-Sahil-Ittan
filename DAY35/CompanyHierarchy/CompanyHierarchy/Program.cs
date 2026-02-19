namespace CompanyHierarchy
{
    public class EmployeeNode
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public List<EmployeeNode> Reports { get; set; }

        public EmployeeNode(string name, string position)
        {
            Name = name;
            Position = position;
            Reports = new List<EmployeeNode>();
        }

        public void AddReport(EmployeeNode employee)
        {
            Reports.Add(employee);
        }
    }

    public class OrganizationTree
    {
        public EmployeeNode Root { get; set; }

        public OrganizationTree(EmployeeNode rootEmployee)
        {
            Root = rootEmployee;
        }

        // Public method that starts the recursion
        public void Display()
        {
            if (Root == null) return;
            Console.WriteLine("ORGANIZATION STRUCTURE");
            Console.WriteLine("======================");
            PrintRecursive(Root, 0);
        }

        // Private helper method for recursion
        private void PrintRecursive(EmployeeNode current, int depth)
        {
            // Create indentation based on depth
            string indent = new string(' ', depth * 4);
            string connector = depth == 0 ? "" : "└── ";

            Console.WriteLine($"{indent}{connector}{current.Name} ({current.Position})");

            // Recursively call for each child
            foreach (var report in current.Reports)
            {
                PrintRecursive(report, depth + 1);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Initialize Nodes
            var ceo = new EmployeeNode("Jordan Smith", "CEO");
            var cto = new EmployeeNode("Alex Chen", "CTO");
            var manager = new EmployeeNode("Sarah Vane", "Dev Manager");
            var dev1 = new EmployeeNode("Leo G.", "Senior Dev");
            var dev2 = new EmployeeNode("Maya R.", "Junior Dev");
            var cfo = new EmployeeNode("Sahil Ittan", "CFO");

            // 2. Build the Tree Structure
            var company = new OrganizationTree(ceo);

            ceo.AddReport(cto);
            cto.AddReport(manager);
            manager.AddReport(dev1);
            manager.AddReport(dev2);
            cfo.AddReport(cfo);

            // 3. Execute Display
            company.Display();

            // Prevent console from closing immediately
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

}
