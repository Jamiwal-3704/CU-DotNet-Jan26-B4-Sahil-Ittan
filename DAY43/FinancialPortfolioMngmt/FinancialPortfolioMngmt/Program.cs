namespace FinancialPortfolioMngmt
{
    internal class Program
    {

        interface IRiskAssessable
        {
            string GetRiskCategory();
        }
        interface IReportable
        {
            string GenerateReportLine();
        }
        abstract class FinancialInstrument
        {
            // common properties 
            public int InstrumentId { get; set; }
            public string Name { get; set; }
            public string Currency { get; set; }
            public DateOnly PurchaseDate { get; set; }
            public int Quantity { get; set; }
            public double PurchasePrice { get; set; }
            public double MarketPrice { get; set; }

            public FinancialInstrument(int instrumentId, string name, string currency, DateOnly purchaseDate, int quantity, double purchasePrice, double marketPrice)
            {
                InstrumentId = instrumentId;
                Name = name;
                Currency = currency;
                PurchaseDate = purchaseDate;
                Quantity = quantity;
                PurchasePrice = purchasePrice;
                MarketPrice = marketPrice;
            }

            // abstract methods
            public abstract decimal CalculateCurrentValue();
            // virual method
            public virtual string GetInstrumentSummary() 
            {
                //return $"{InstrumentId} {Name} {Currency} {PurchaseDate} {Quantity} {PurchasePrice} {MarketPrice}";
                return $"{Name} | {Quantity} units | Value: {CalculateCurrentValue():C}";
            }
        }

        public class Equity : FinancialInstrument,IRiskAssessable,IReportable
        {
            public int InstrumentId { get; set; }
            public string Name { get; set; }
            public string Currency { get; set; }
            public DateOnly PurchaseDate { get; set; }
            public int Quantity { get; set; }
            public double PurchasePrice { get; set; }
            public double MarketPrice { get; set; }

            public override decimal CalculateCurrentValue()
            {
                return CalculateCurrentValue();
            }

        }
        public class Bond
        {
            public int InstrumentId { get; set; }
            public string Name { get; set; }
            public string Currency { get; set; }
            public DateOnly PurchaseDate { get; set; }
            public int Quantity { get; set; }
            public double PurchasePrice { get; set; }
            public double MarketPrice { get; set; }
        }
        public class FixedDeposit
        {
            public int InstrumentId { get; set; }
            public string Name { get; set; }
            public string Currency { get; set; }
            public DateOnly PurchaseDate { get; set; }
            public int Quantity { get; set; }
            public double PurchasePrice { get; set; }
            public double MarketPrice { get; set; }
        }
        public class MutualFund
        {
            public int InstrumentId { get; set; }
            public string Name { get; set; }
            public string Currency { get; set; }
            public DateOnly PurchaseDate { get; set; }
            public int Quantity { get; set; }
            public double PurchasePrice { get; set; }
            public double MarketPrice { get; set; }
        }
        static void Main(string[] args)
        {
              
        }
    }
}
