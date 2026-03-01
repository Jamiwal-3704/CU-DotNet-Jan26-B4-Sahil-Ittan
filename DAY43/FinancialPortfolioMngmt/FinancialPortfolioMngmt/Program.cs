using System;
using System.Collections.Generic;

namespace FinancialPortfolioMngmt
{
    internal class Program
    {
        // ================= INTERFACES =================
        interface IRiskAssessable
        {
            string GetRiskCategory();
        }

        interface IReportable
        {
            string GenerateReportLine();
        }

        // ================= CUSTOM EXCEPTION =================
        public class InvalidFinancialDataException : Exception
        {
            public InvalidFinancialDataException(string message) : base(message) { }
        }
        // ================= ABSTRACT BASE CLASS =================
        public abstract class FinancialInstrument
        {
            private int _quantity;
            private double _purchasePrice;
            private double _marketPrice;
            private string _currency;

            public int InstrumentId { get; }
            public string Name { get; }
            public DateOnly PurchaseDate { get; }

            public int Quantity
            {
                get { return _quantity; }
                protected set
                {
                    if (value < 0)
                        throw new InvalidFinancialDataException("Quantity cannot be negative");
                    _quantity = value;
                }
            }

            public double PurchasePrice
            {
                get { return _purchasePrice; }
                protected set
                {
                    if (value < 0)
                        throw new InvalidFinancialDataException("Purchase price cannot be negative");
                    _purchasePrice = value;
                }
            }

            public double MarketPrice
            {
                get { return _marketPrice; }
                protected set
                {
                    if (value < 0)
                        throw new InvalidFinancialDataException("Market price cannot be negative");
                    _marketPrice = value;
                }
            }

            public string Currency
            {
                get { return _currency; }
                protected set
                {
                    if (value.Length != 3)
                        throw new InvalidFinancialDataException("Currency must be 3-letter code");
                    _currency = value.ToUpper();
                }
            }

            protected FinancialInstrument(
                int instrumentId,
                string name,
                string currency,
                DateOnly purchaseDate,
                int quantity,
                double purchasePrice,
                double marketPrice)
            {
                InstrumentId = instrumentId;
                Name = name;
                Currency = currency;
                PurchaseDate = purchaseDate;
                Quantity = quantity;
                PurchasePrice = purchasePrice;
                MarketPrice = marketPrice;
            }
            // virtual methods
            public abstract decimal CalculateCurrentValue();

            public virtual string GetInstrumentSummary()
            {
                return Name + " | " + Quantity + " units | Value: " +
                       CalculateCurrentValue().ToString("C");
            }
        }

        // ================= EQUITY =================
        public class Equity : FinancialInstrument, IRiskAssessable, IReportable
        {
            public Equity(int id, string name, string currency, DateOnly date,
                          int qty, double buy, double market)
                : base(id, name, currency, date, qty, buy, market) { }

            public override decimal CalculateCurrentValue()
            {
                return Quantity * (decimal)MarketPrice;
            }

            public string GetRiskCategory()
            {
                return "High";
            }

            public string GenerateReportLine()
            {
                return "Equity | " + Name + " | " +
                       CalculateCurrentValue().ToString("C");
            }
        }

        // ================= BOND =================
        public class Bond : FinancialInstrument, IRiskAssessable
        {
            public Bond(int id, string name, string currency, DateOnly date,
                        int qty, double buy, double market)
                : base(id, name, currency, date, qty, buy, market) { }

            public override decimal CalculateCurrentValue()
            {
                return Quantity * (decimal)MarketPrice;
            }

            public string GetRiskCategory()
            {
                return "Low";
            }
        }

        // ================= FIXED DEPOSIT =================
        public class FixedDeposit : FinancialInstrument
        {
            public FixedDeposit(int id, string name, string currency, DateOnly date,
                                int qty, double buy, double market)
                : base(id, name, currency, date, qty, buy, market) { }

            public override decimal CalculateCurrentValue()
            {
                return Quantity * (decimal)MarketPrice;
            }
        }

        // ================= MUTUAL FUND =================
        public class MutualFund : FinancialInstrument, IRiskAssessable, IReportable
        {
            public MutualFund(int id, string name, string currency, DateOnly date,
                              int qty, double buy, double market)
                : base(id, name, currency, date, qty, buy, market) { }

            public override decimal CalculateCurrentValue()
            {
                return Quantity * (decimal)MarketPrice;
            }

            public string GetRiskCategory()
            {
                return "Medium";
            }

            public string GenerateReportLine()
            {
                return "MutualFund | " + Name + " | " +
                       CalculateCurrentValue().ToString("C");
            }
        }

        // ================= PORTFOLIO =================
        public class Portfolio
        {
            private List<FinancialInstrument> instruments = new List<FinancialInstrument>();

            private Dictionary<int, FinancialInstrument> lookup =new Dictionary<int, FinancialInstrument>();

            public void AddInstrument(FinancialInstrument instrument)
            {
                if (lookup.ContainsKey(instrument.InstrumentId))
                    throw new Exception("Duplicate Instrument ID");

                instruments.Add(instrument);
                lookup[instrument.InstrumentId] = instrument;
            }

            public void RemoveInstrument(int id)
            {
                if (lookup.ContainsKey(id))
                {
                    FinancialInstrument inst = lookup[id];
                    instruments.Remove(inst);
                    lookup.Remove(id);
                }
            }

            public decimal GetTotalPortfolioValue()
            {
                decimal total = 0;

                foreach (FinancialInstrument inst in instruments)
                {
                    total += inst.CalculateCurrentValue();
                }

                return total;
            }

            public FinancialInstrument GetInstrumentById(int id)
            {
                if (lookup.ContainsKey(id))
                    return lookup[id];

                return null;
            }

            public List<FinancialInstrument> GetInstrumentsByRisk(string risk)
            {
                List<FinancialInstrument> result = new List<FinancialInstrument>();

                foreach (FinancialInstrument inst in instruments)
                {
                    if (inst is IRiskAssessable)
                    {
                        IRiskAssessable riskInst = (IRiskAssessable)inst;

                        if (riskInst.GetRiskCategory() == risk)
                            result.Add(inst);
                    }
                }

                return result;
            }

            public List<FinancialInstrument> GetAllInstruments()
            {
                return instruments;
            }
        }

        // ================= MAIN =================
        //static void Main(string[] args)
        //{
        //    Portfolio portfolio = new Portfolio();

        //    portfolio.AddInstrument(
        //        new Equity(1, "INFY", "INR",
        //        DateOnly.FromDateTime(DateTime.Now),
        //        100, 1500, 1650));

        //    portfolio.AddInstrument(
        //        new Bond(2, "Gov Bond", "INR",
        //        DateOnly.FromDateTime(DateTime.Now),
        //        50, 1000, 1050));

        //    Console.WriteLine("Total Portfolio Value: " +
        //        portfolio.GetTotalPortfolioValue().ToString("C"));
        //}
        static void Main(string[] args)
        {
            Portfolio portfolio = new Portfolio();

            // 1️⃣ AddInstrument()
            portfolio.AddInstrument(
                new Equity(1, "INFY", "INR",
                DateOnly.FromDateTime(DateTime.Now),
                100, 1500, 1650));

            portfolio.AddInstrument(
                new Bond(2, "Gov Bond", "INR",
                DateOnly.FromDateTime(DateTime.Now),
                50, 1000, 1050));

            portfolio.AddInstrument(
                new MutualFund(3, "Axis MF", "INR",
                DateOnly.FromDateTime(DateTime.Now),
                200, 120, 140));

            Console.WriteLine("✔ Instruments added\n");

            // 2️⃣ GetAllInstruments()
            Console.WriteLine("All Instruments:");
            foreach (FinancialInstrument inst in portfolio.GetAllInstruments())
            {
                Console.WriteLine(inst.GetInstrumentSummary());
            }

            // 3️⃣ GetTotalPortfolioValue()
            Console.WriteLine("\nTotal Portfolio Value: " +
                portfolio.GetTotalPortfolioValue().ToString("C"));

            // 4️⃣ GetInstrumentById()
            Console.WriteLine("\nGet Instrument By ID (1):");
            FinancialInstrument instrument = portfolio.GetInstrumentById(1);
            if (instrument != null)
                Console.WriteLine(instrument.GetInstrumentSummary());

            // 5️⃣ GetInstrumentsByRisk()
            Console.WriteLine("\nHigh Risk Instruments:");
            List<FinancialInstrument> highRisk =
                portfolio.GetInstrumentsByRisk("High");

            foreach (FinancialInstrument inst in highRisk)
            {
                Console.WriteLine(inst.GetInstrumentSummary());
            }

            // 6️⃣ RemoveInstrument()
            Console.WriteLine("\nRemoving Instrument with ID 2...");
            portfolio.RemoveInstrument(2);

            Console.WriteLine("\nInstruments after removal:");
            foreach (FinancialInstrument inst in portfolio.GetAllInstruments())
            {
                Console.WriteLine(inst.GetInstrumentSummary());
            }
        }
    }
}