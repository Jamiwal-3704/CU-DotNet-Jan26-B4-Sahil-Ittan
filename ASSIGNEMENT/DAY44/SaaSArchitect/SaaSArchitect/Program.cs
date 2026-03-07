using System.Text;

namespace SaaSArchitect
{

    public abstract class Subscriber : IComparable<Subscriber>
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime JoinDate { get; }

        protected Subscriber(Guid id, string name, DateTime joinDate)
        {
            Id = id;
            Name = name;
            JoinDate = joinDate;
        }

        // abstract method 
        public abstract decimal CalculateMonthlyBill();

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Subscriber other){ return false;}

            return Id == other.Id; ;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public int CompareTo(Subscriber other)
        {
            if (other == null) return 1;

            int dateComparison = JoinDate.CompareTo(other.JoinDate);

            if(dateComparison != 0) return dateComparison;

            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }
    }

    // subclasses 
    public class BusinessSubscriber : Subscriber
    {
        public decimal FixedRate { get; set; }
        public decimal TaxRate { get; set; }

        public BusinessSubscriber(Guid id, string name, DateTime joinDate) : base(id, name, joinDate)
        { }

        // method 
        public override decimal CalculateMonthlyBill()
        {
            return FixedRate * (1 + TaxRate);
        }
    }

    public class ConsumerSubscriber : Subscriber
    {
        public decimal DataUsageGB { get; set; }
        public decimal PricePerGB { get; set; }

        public ConsumerSubscriber(Guid id, string name, DateTime joinDate) : base(id,name,joinDate)
        {

        }

        //methods 
        public override decimal CalculateMonthlyBill()
        {
            return DataUsageGB * PricePerGB;
        }
    }

    public static class ReportGenerator
    {
        public static void PrintRevenueReport(IEnumerable<Subscriber> subscribers)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=========== SaaS Revenue Report ===========");
            sb.AppendLine("Name\t\tType\t\tJoin Date\tMonthly Bill");
            sb.AppendLine("---------------------------------------------------");

            foreach (var sub in subscribers)
            {
                string type = sub is BusinessSubscriber ? "Business" : "Consumer";

                sb.AppendLine(
                    $"{sub.Name}\t{type}\t{sub.JoinDate:yyyy-MM-dd}\t{sub.CalculateMonthlyBill():C}"
                );
            }

            sb.AppendLine("==========================================");

            Console.WriteLine(sb.ToString());
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 1️⃣ Dictionary: Email → Subscriber
            Dictionary<string, Subscriber> subscribers = new Dictionary<string, Subscriber>();

            // 2️⃣ Add mixed subscribers
            subscribers.Add("ceo@techcorp.com", new BusinessSubscriber(Guid.NewGuid(), "TechCorp Pvt Ltd", new DateTime(2024, 1, 10))
                {
                    FixedRate = 10000m,
                    TaxRate = 0.18m
                }
            );

            subscribers.Add("admin@startup.io", new BusinessSubscriber(Guid.NewGuid(), "Startup Inc", new DateTime(2023, 11, 5))
                {
                    FixedRate = 7000m,
                    TaxRate = 0.12m
                }
            );

            subscribers.Add("rahul@gmail.com", new ConsumerSubscriber(Guid.NewGuid(), "Rahul Sharma", new DateTime(2024, 2, 15))
                {
                    DataUsageGB = 120m,
                    PricePerGB = 8m
                }
            );

            subscribers.Add("priya@gmail.com", new ConsumerSubscriber(Guid.NewGuid(), "Priya Verma", new DateTime(2023, 12, 20))
                {
                    DataUsageGB = 80m,
                    PricePerGB = 10m
                }
            );

            subscribers.Add("amit@gmail.com", new ConsumerSubscriber(Guid.NewGuid(), "Amit Singh", new DateTime(2024, 3, 1))
                {
                    DataUsageGB = 150m,
                    PricePerGB = 6m
                }
            );

            // 3️⃣ Sort by Monthly Bill (DESC)
            var sortedSubscribers = subscribers
                .OrderByDescending(x => x.Value.CalculateMonthlyBill())
                .Select(x => x.Value)
                .ToList();

            // 4️⃣ Print report
            ReportGenerator.PrintRevenueReport(sortedSubscribers);

            Console.ReadLine();
        }
    }
}
