# 🏦 Financial Portfolio Management & Reporting System

## 📋 Project Overview

A comprehensive console-based Financial Portfolio Management System designed to demonstrate advanced object-oriented programming concepts through a real-world banking application. The system manages different financial instruments, processes transactions, and generates detailed portfolio reports.

## 🎯 Educational Objective

This project serves as a capstone exercise for mastering:

* **Object-Oriented Design** (Abstraction, Inheritance, Polymorphism, Encapsulation)
* **Interface Implementation** and contract-based programming
* **Collection Management** (Lists, Dictionaries, Arrays)
* **LINQ Operations** for data querying and analysis
* **File I/O Operations** with proper exception handling
* **Custom Exception Creation** for business rule validation
* **String Manipulation** and parsing techniques

---

## 🏗️ System Architecture

**text**

```
┌─────────────────────────────────────────────────────────────┐
│                    FINANCIAL PORTFOLIO                       │
│                     MANAGEMENT SYSTEM                         │
└─────────────────────────────────────────────────────────────┘
                              │
            ┌─────────────────┼─────────────────┐
            │                 │                 │
            ▼                 ▼                 ▼
    ┌─────────────┐   ┌─────────────┐   ┌─────────────┐
    │  Portfolio  │   │ Transaction │   │   Report    │
    │  Management │   │   Module    │   │  Generator  │
    └─────────────┘   └─────────────┘   └─────────────┘
            │                 │                 │
            └─────────────────┼─────────────────┘
                              │
                              ▼
              ┌─────────────────────────────┐
              │    Financial Instruments     │
              │    (Inheritance Hierarchy)   │
              └─────────────────────────────┘
```

---

## 📊 Financial Instrument Hierarchy

### Abstract Base Class: `FinancialInstrument`

**csharp**

```
public abstract class FinancialInstrument
{
    // Common Properties
    public string InstrumentId { get; protected set; }
    public string Name { get; protected set; }
    public string Currency { get; protected set; }
    public DateTime PurchaseDate { get; protected set; }
    public decimal PurchasePrice { get; protected set; }
    public decimal MarketPrice { get; protected set; }

    // Abstract Method - Must be implemented by derived classes
    public abstract decimal CalculateCurrentValue();

    // Virtual Method - Can be overridden
    public virtual string GetInstrumentSummary()
    {
        return $"{InstrumentId}: {Name} - Purchased: {PurchaseDate:d}";
    }
}
```

### Derived Instrument Classes

| Instrument Type        | Description             | Unique Characteristics        |
| ---------------------- | ----------------------- | ----------------------------- |
| **Equity**       | Stock shares            | Dividend yield, voting rights |
| **Bond**         | Fixed income securities | Coupon rate, maturity date    |
| **FixedDeposit** | Term deposits           | Interest rate, lock-in period |
| **MutualFund**   | Pooled investments      | NAV, expense ratio            |

---

## 🔌 Interface Contracts

### Interface 1: `IRiskAssessable`

**csharp**

```
public interface IRiskAssessable
{
    string GetRiskCategory(); // Returns "Low", "Medium", "High"
}
```

### Interface 2: `IReportable`

**csharp**

```
public interface IReportable
{
    string GenerateReportLine(); // Formats instrument data for reporting
}
```

### Risk Classification Matrix

| Instrument Type | Risk Category Criteria         |
| --------------- | ------------------------------ |
| Equity          | Based on volatility/market cap |
| Bond            | Based on credit rating         |
| FixedDeposit    | Low (insured)                  |
| MutualFund      | Based on fund type             |

---

## ⚠️ Custom Exception Handling

### `InvalidFinancialDataException`

**csharp**

```
public class InvalidFinancialDataException : Exception
{
    public InvalidFinancialDataException(string message) : base(message) { }
    public InvalidFinancialDataException(string message, Exception inner) 
        : base(message, inner) { }
}
```

**Trigger Conditions:**

* ❌ Negative quantity values
* ❌ Negative purchase or market price
* ❌ Invalid currency format (not 3-letter code, e.g., "USD", "INR", "EUR")
* ❌ Null or empty instrument ID

---

## 📦 Portfolio Management

### `Portfolio` Class Responsibilities

| Component                    | Implementation                              | Purpose                |
| ---------------------------- | ------------------------------------------- | ---------------------- |
| **Instrument Storage** | `List<FinancialInstrument>`               | Polymorphic collection |
| **Fast Lookup**        | `Dictionary<string, FinancialInstrument>` | O(1) access by ID      |
| **Add Instrument**     | Validation + Storage                        | Ensures data integrity |
| **Remove Instrument**  | ID-based removal                            | Portfolio maintenance  |
| **Get Total Value**    | LINQ Sum                                    | Portfolio valuation    |
| **Find by ID**         | Dictionary lookup                           | Quick access           |
| **Filter by Risk**     | LINQ + Interface check                      | Risk analysis          |

### Key LINQ Operations

**csharp**

```
// Total Portfolio Value
decimal totalValue = instruments.Sum(i => i.CalculateCurrentValue());

// Group by Instrument Type
var groupedByType = instruments.GroupBy(i => i.GetType().Name);

// Filter by Risk Category
var highRisk = instruments.OfType<IRiskAssessable>()
                          .Where(r => r.GetRiskCategory() == "High");
```

---

## 💳 Transactions Module

### `Transaction` Class Structure

**csharp**

```
public class Transaction
{
    public string TransactionId { get; set; }
    public string InstrumentId { get; set; }
    public TransactionType Type { get; set; } // Buy or Sell
    public int Units { get; set; }
    public DateTime Date { get; set; }
}

public enum TransactionType
{
    Buy,
    Sell
}
```

### Transaction Processing Flow

**text**

```
┌─────────────────────┐
│  Input Transaction  │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Validate:          │
│  • Instrument exists│
│  • Units > 0        │
│  • Sufficient units │
│    for Sell         │
└──────────┬──────────┘
           ↓
    ┌──────┴──────┐
    │ Valid?       │
    │  ┌───┴───┐  │
    │ Yes      No  │
    └──┬───────┬┘  │
       ↓       └───┘
┌─────────────────────┐
│ Update Portfolio    │
│ holdings            │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Add to Transaction  │
│ History Array       │
└─────────────────────┘
```

### Transaction Validation Rules

| Scenario           | Validation                     | Exception                     |
| ------------------ | ------------------------------ | ----------------------------- |
| Buy Order          | Units > 0                      | InvalidFinancialDataException |
| Sell Order         | Units ≤ Current Holdings      | InvalidOperationException     |
| Unknown Instrument | Instrument exists in portfolio | KeyNotFoundException          |

---

## 📈 Reporting Engine

### `ReportGenerator` Class Capabilities

#### 1. Console Report Generation

**text**

```
===== PORTFOLIO SUMMARY =====
Generated: 2024-01-15 14:30:45

Instrument Type: Equity
  Total Investment: ₹500,000.00
  Current Value:    ₹575,000.00
  Profit/Loss:      ₹75,000.00 (+15.0%)

Instrument Type: Bond
  Total Investment: ₹200,000.00
  Current Value:    ₹210,000.00
  Profit/Loss:      ₹10,000.00 (+5.0%)

Instrument Type: FixedDeposit
  Total Investment: ₹100,000.00
  Current Value:    ₹108,000.00
  Profit/Loss:      ₹8,000.00 (+8.0%)

================================
Overall Portfolio Value: ₹893,000.00

Risk Distribution:
  Low Risk:    3 instruments
  Medium Risk: 2 instruments
  High Risk:   1 instrument
================================
```

#### 2. File Report Generation

**File Format:** `PortfolioReport_YYYYMMDD_HHMMSS.txt`

**text**

```
========================================
FINANCIAL PORTFOLIO REPORT
Generated: 2024-01-15 14:30:45
========================================

INSTRUMENT DETAILS
----------------------------------------
ID: EQ001 | INFOSYS LTD
Type: Equity | Currency: INR
Purchase: 100 units @ ₹1,500.00
Current: ₹1,650.00 | Value: ₹165,000.00
P/L: ₹15,000.00 (+10.0%) | Risk: Medium
----------------------------------------
ID: BD001 | GOVT BOND 2030
Type: Bond | Currency: INR
Purchase: 50 units @ ₹1,000.00
Current: ₹1,050.00 | Value: ₹52,500.00
P/L: ₹2,500.00 (+5.0%) | Risk: Low
----------------------------------------

AGGREGATED SUMMARY
========================================
Total Investment:      ₹800,000.00
Total Current Value:   ₹893,000.00
Total Profit/Loss:     ₹93,000.00 (+11.6%)

Risk Distribution:
  Low:    2 instruments
  Medium: 2 instruments
  High:   1 instrument
========================================
```

---

## 📝 CSV Data Handling

### Input Format Specification

**text**

```
[InstrumentId],[Type],[Name],[Currency],[PurchaseDate],[Units],[PurchasePrice],[MarketPrice]
```

### Sample CSV Lines

**text**

```
EQ001,Equity,INFOSYS LTD,INR,2023-01-15,100,1500.00,1650.00
BD001,Bond,GOVT BOND 2030,INR,2023-02-20,50,1000.00,1050.00
FD001,FixedDeposit,SBI FIXED DEPOSIT,INR,2023-03-10,10,50000.00,54000.00
MF001,MutualFund,AXIS BLUECHIP FUND,INR,2023-04-05,200,250.00,275.00
```

### Parsing Logic

**csharp**

```
public FinancialInstrument ParseFromCSV(string csvLine)
{
    string[] parts = csvLine.Split(',');

    // Validate format
    if (parts.Length != 8)
        throw new InvalidFinancialDataException("Invalid CSV format");

    // Parse based on instrument type
    string type = parts[1];

    return type switch
    {
        "Equity" => new Equity(parts[0], parts[2], parts[3], 
                               DateTime.Parse(parts[4]), int.Parse(parts[5]), 
                               decimal.Parse(parts[6]), decimal.Parse(parts[7])),
        // ... other types
        _ => throw new ArgumentException($"Unknown instrument type: {type}")
    };
}
```

---

## 🔍 Polymorphism in Action

### Example: Processing Portfolio

**csharp**

```
public class Portfolio
{
    private List<FinancialInstrument> _instruments = new();

    public decimal CalculateTotalValue()
    {
        // Polymorphic call - each instrument calculates its own value
        return _instruments.Sum(i => i.CalculateCurrentValue());
    }

    public void DisplayAllSummaries()
    {
        foreach (var instrument in _instruments)
        {
            // Polymorphic behavior - each instrument may have custom summary
            Console.WriteLine(instrument.GetInstrumentSummary());

            // Interface-based polymorphic behavior
            if (instrument is IRiskAssessable riskable)
            {
                Console.WriteLine($"Risk Category: {riskable.GetRiskCategory()}");
            }
        }
    }
}
```

---

## ⚡ Edge Cases & Validations

### Data Integrity Checks

| Scenario          | Validation                    | Handling                                |
| ----------------- | ----------------------------- | --------------------------------------- |
| Negative Quantity | `if (quantity < 0)`         | Throw `InvalidFinancialDataException` |
| Negative Price    | `if (price < 0)`            | Throw `InvalidFinancialDataException` |
| Invalid Currency  | `if (currency.Length != 3)` | Throw `InvalidFinancialDataException` |
| Duplicate ID      | Check dictionary              | Reject addition                         |
| Sell > Holdings   | Compare units                 | Throw `InvalidOperationException`     |
| File Permission   | Try-Catch                     | Display user-friendly error             |

### Sample Validation Code

**csharp**

```
private void ValidateInstrumentData(string currency, decimal price, int quantity)
{
    if (string.IsNullOrEmpty(currency) || currency.Length != 3)
        throw new InvalidFinancialDataException(
            $"Invalid currency code: {currency}. Must be 3 letters.");

    if (price < 0)
        throw new InvalidFinancialDataException(
            $"Price cannot be negative: {price}");

    if (quantity < 0)
        throw new InvalidFinancialDataException(
            $"Quantity cannot be negative: {quantity}");
}
```

---

## 🧪 Sample Test Scenarios

### Scenario 1: Complete Portfolio Lifecycle

**text**

```
1. Create instruments from CSV
2. Add to portfolio
3. Process buy/sell transactions
4. Generate console report
5. Export to file report
6. Query risk distribution
7. Calculate profit/loss
```

### Scenario 2: Error Handling Demonstration

**text**

```
1. Try adding instrument with negative price → Exception
2. Try selling more units than owned → Exception
3. Try invalid currency code → Exception
4. Try duplicate instrument ID → Rejection
5. Access file with no permission → Graceful handling
```

---

## 📚 Learning Outcomes

By completing this project, students will demonstrate proficiency in:

### Object-Oriented Programming

* ✅ Abstract class design and implementation
* ✅ Inheritance hierarchy creation
* ✅ Polymorphic method overriding
* ✅ Interface implementation
* ✅ Encapsulation with property validation

### C# Language Features

* ✅ LINQ queries and operations
* ✅ Generic collections (List, Dictionary)
* ✅ Exception handling with custom exceptions
* ✅ File I/O with StreamWriter
* ✅ String manipulation and parsing
* ✅ DateTime operations

### Software Design Patterns

* ✅ Repository pattern (Portfolio storage)
* ✅ Factory pattern (Instrument creation)
* ✅ Strategy pattern (Risk assessment)
* ✅ Template method (Report generation)

### Real-World Skills

* ✅ Financial data processing
* ✅ Portfolio valuation
* ✅ Risk analysis
* ✅ Report generation
* ✅ Data validation
* ✅ Error recovery

---

## 🚀 Extension Ideas

### Advanced Features

1. **Real-time Price Updates** : Connect to mock market data feed
2. **Performance Metrics** : Calculate returns, volatility, Sharpe ratio
3. **Tax Calculation** : Capital gains tax estimation
4. **Multi-Currency Support** : Currency conversion handling
5. **Historical Tracking** : Portfolio value over time
6. **Alert System** : Notify on significant price changes
7. **Diversification Analysis** : Sector/industry exposure
8. **What-If Scenarios** : Simulate market changes

### Technical Enhancements

1. **Database Integration** : Store data in SQL Server
2. **Web API** : Expose portfolio data via REST endpoints
3. **Authentication** : User login for multiple portfolios
4. **Caching** : Improve performance for large portfolios
5. **Async Operations** : Non-blocking file/network operations

---

## 📋 Evaluation Criteria

| Category        | Weight | Assessment Points                                     |
| --------------- | ------ | ----------------------------------------------------- |
| OOP Principles  | 25%    | Correct use of abstraction, inheritance, polymorphism |
| Data Management | 20%    | Proper collection usage, LINQ queries                 |
| Error Handling  | 15%    | Comprehensive validation, custom exceptions           |
| File Operations | 15%    | Correct file I/O, formatting                          |
| Code Quality    | 15%    | Naming, organization, comments                        |
| Requirements    | 10%    | All functional requirements met                       |
