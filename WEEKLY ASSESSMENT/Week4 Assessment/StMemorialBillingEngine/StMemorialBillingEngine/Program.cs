using System.Diagnostics.CodeAnalysis;

namespace StMemorialBillingEngine
{
    public class Patient
    {
        public string Name { get; set; }
        public decimal BaseFee { get; set; }
        
        public virtual decimal CalculateFinalBill()
        {
            return BaseFee;
        }
        public virtual string GetBillBreakdown()
        {
            return $"Base Fee: {BaseFee.ToString("C2")}";
        }

    }

    public class Inpatient : Patient
    {
        public int DaysStayed { get; set; }
        public decimal DailyRate { get; set; }
        decimal totalBill;

        // logic for the Inpatient

        public override decimal CalculateFinalBill()
        {
            totalBill = base.CalculateFinalBill() + (DaysStayed * DailyRate);
            return totalBill;
        }
        public virtual string GetBillBreakdown()
        {
            return $"Base Fee: {BaseFee.ToString("C2")}";
        }

    }

    public class OutPatient : Patient
    {
        public decimal ProcedureFee { get; set; }
        decimal totalBill;

        // Logic for the OutPatient
        public override decimal CalculateFinalBill()
        {
            return totalBill = base.CalculateFinalBill() + ProcedureFee;
        }
        public override string GetBillBreakdown()
        {
            return
                $"Base Fee: {BaseFee.ToString("C2")}\n" +
                $"Procedure Fee: {ProcedureFee.ToString("C2")}";
        }
    }

    public class EmergencyPatient : Patient
    {
        public int SeverityLevel { get; set; }
        decimal totalBill;
        // Logic for the EmergencyPatient

        public override decimal CalculateFinalBill()
        {
            totalBill = base.CalculateFinalBill() * SeverityLevel;
            return totalBill;
        }
        public override string GetBillBreakdown()
        {
            return
                $"Base Fee: {BaseFee.ToString("C2")}\n" +
                $"Severity Level: {SeverityLevel} × Base Fee";
        }

    }

    public class HospitalBilling
    {
        // 4 methods
        private List<Patient> patients = new List<Patient>();

        public void AddPatient(Patient p)
        {
            patients.Add(p);
        }

        public void GenerateDailyReport()
        {
            Console.WriteLine("---- Daily Billing Report ----");

            foreach (Patient patient in patients)
            {
                decimal bill = patient.CalculateFinalBill();
                Console.WriteLine($"Patient Name: {patient.Name}");
                Console.WriteLine("Bill Breakdown:");
                Console.WriteLine(patient.GetBillBreakdown()); 
                Console.WriteLine($"Final Bill: {bill.ToString("C2")}");
                Console.WriteLine("----------------------------------");
            }

        }

        public decimal CalculateTotalRevenue()
        {
            decimal sum = 0;
            foreach(Patient patient in patients)
            {
                sum += patient.CalculateFinalBill();
            }
            return sum;
        }   

        public int GetInpatientCount()
        {
            int count = 0;
            foreach(Patient p in patients)
            {
                if(p is Inpatient)
                {
                    count++;
                }
            }
            return count;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            
            HospitalBilling billing = new HospitalBilling();

            billing.AddPatient(
                new Inpatient
                {
                    Name = "Sahil",
                    BaseFee = 1500,
                    DaysStayed = 5,
                    DailyRate = 1500
                });

            billing.AddPatient(
                new Inpatient
                {
                    Name = "Rohit",
                    BaseFee = 1800,
                    DaysStayed = 3,
                    DailyRate = 2200
                });

            billing.AddPatient(
                new OutPatient
                {
                    Name = "Shivam",
                    BaseFee = 2000,
                    ProcedureFee = 2000
                });

            billing.AddPatient(
                new EmergencyPatient
                {
                    Name = "Om",
                    BaseFee = 500,
                    SeverityLevel = 4
                });

            billing.GenerateDailyReport();

            Console.WriteLine();

            Console.WriteLine($"Total Revenue : {billing.CalculateTotalRevenue().ToString("C2")}");
            Console.WriteLine($"Total Inpatients: {billing.GetInpatientCount()}");
        }
    }
}
