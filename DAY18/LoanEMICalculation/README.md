# Loan EMI Calculation System

A C# implementation demonstrating method hiding in inheritance for different loan types without using polymorphism.

## 📋 Overview

This case study implements a loan EMI calculation system for different loan products (Home Loan and Car Loan), showcasing how to handle different calculation rules using **method hiding** (`new` keyword) instead of virtual methods.

## 🏗️ Class Hierarchy

```
                    ┌─────────────────┐
                    │      LOAN       │
                    │   (Base Class)  │
                    ├─────────────────┤
                    │ LoanNumber      │
                    │ CustomerName    │
                    │ PrincipalAmount │
                    │ TenureInYears   │
                    ├─────────────────┤
                    │ CalculateEMI()  │
                    │   (10% simple)  │
                    └────────┬────────┘
                             │
            ┌────────────────┴────────────────┐
            │                                 │
            ▼                                 ▼
┌───────────────────────┐          ┌───────────────────────┐
│      HOMELOAN         │          │       CARLOAN         │
│    (Derived Class)    │          │    (Derived Class)    │
├───────────────────────┤          ├───────────────────────┤
│ Interest Rate: 8%     │          │ Interest Rate: 9%     │
│ Processing Fee: 1%    │          │ Insurance: ₹15,000    │
├───────────────────────┤          ├───────────────────────┤
│ new CalculateEMI()    │          │ new CalculateEMI()    │
│   (Hides base method) │          │   (Hides base method) │
└───────────────────────┘          └───────────────────────┘
```

## 💰 Loan Types & Calculation Rules

| Loan Type | Interest Rate | Additional Charges | EMI Formula |
|-----------|--------------|-------------------|-------------|
| **Base Loan** | 10% | None | (P + (P × 0.10 × T)) / (T × 12) |
| **Home Loan** | 8% | Processing Fee: 1% of P | (P + Fee + (P × 0.08 × T)) / (T × 12) |
| **Car Loan** | 9% | Insurance: ₹15,000 | (P + Insurance + (P × 0.09 × T)) / (T × 12) |

## 📊 Program Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                      EXECUTION SEQUENCE                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   START                                                          │
│     │                                                            │
│     ▼                                                            │
│   Create Loan Objects                                            │
│     ├──► HomeLoan hl1 = new HomeLoan(...)                       │
│     ├──► HomeLoan hl2 = new HomeLoan(...)                       │
│     ├──► CarLoan cl1 = new CarLoan(...)                         │
│     └──► CarLoan cl2 = new CarLoan(...)                         │
│                                                                  │
│     ▼                                                            │
│   Store in Loan Array                                            │
│     └──► Loan[] loans = { hl1, hl2, cl1, cl2 }                  │
│                                                                  │
│     ▼                                                            │
│   Loop Through Array                                             │
│     └──► foreach(Loan loan in loans)                            │
│            {                                                     │
│                 ▼                                                │
│           loan.CalculateEMI()                                    │
│                 │                                                │
│                 ▼                                                │
│           ╔════════════════════════════════════════════════╗    │
│           ║  BASE CLASS METHOD EXECUTES (ALWAYS!)         ║    │
│           ║  • Even for HomeLoan/CarLoan objects          ║    │
│           ║  • Because reference type is LOAN             ║    │
│           ║  • Method hiding = static binding             ║    │
│           ╚════════════════════════════════════════════════╝    │
│            }                                                     │
│                                                                  │
│     ▼                                                            │
│   END                                                            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 🔄 Method Hiding Behavior

```
┌─────────────────────────────────────────────────────────────────┐
│                    METHOD CALL RESOLUTION                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   REFERENCE TYPE = LOAN                                          │
│   ───────────────────────────────────────────────────────────   │
│                                                                  │
│   Loan[] loans = {                                               │
│       new HomeLoan("HL001", "John", 5000000, 15),               │
│       new CarLoan("CL001", "Mike", 800000, 5)                   │
│   };                                                             │
│                                                                  │
│   foreach(Loan loan in loans)                                    │
│   {                                                              │
│       // ⚠️ ALWAYS calls Loan.CalculateEMI()                    │
│       // ✅ Object type (HomeLoan/CarLoan) doesn't matter        │
│       // ❌ Derived class methods are HIDDEN, not called         │
│       loan.CalculateEMI();                                       │
│   }                                                              │
│                                                                  │
│   ╔══════════════════════════════════════════════════════════╗   │
│   ║  KEY INSIGHT: Without virtual/override, polymorphism    ║   │
│   ║  doesn't work. Reference type determines method called! ║   │
│   ╚══════════════════════════════════════════════════════════╝   │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📝 Sample Output

```
========== LOAN EMI CALCULATION SYSTEM ==========

CREATING LOAN OBJECTS...
═══════════════════════════════════════════════════

Home Loan 1: HL001 | John | ₹50,00,000 | 15 years
Home Loan 2: HL002 | Sarah | ₹75,00,000 | 20 years
Car Loan 1: CL001 | Mike | ₹8,00,000 | 5 years
Car Loan 2: CL002 | Emma | ₹12,00,000 | 7 years

═══════════════════════════════════════════════════

PROCESSING ALL LOANS (via Base Class Reference)...
═══════════════════════════════════════════════════

Loan #: HL001 | Customer: John
Base Loan EMI (10%): ₹47,222.22
───────────────────────────────────────────

Loan #: HL002 | Customer: Sarah
Base Loan EMI (10%): ₹68,750.00
───────────────────────────────────────────

Loan #: CL001 | Customer: Mike
Base Loan EMI (10%): ₹16,000.00
───────────────────────────────────────────

Loan #: CL002 | Customer: Emma
Base Loan EMI (10%): ₹21,142.86
───────────────────────────────────────────

═══════════════════════════════════════════════════

FOR COMPARISON - Using Derived Class References:
═══════════════════════════════════════════════════

HOME LOAN (Actual with 8% + 1% fee):
├─ Principal: ₹50,00,000
├─ Processing Fee (1%): ₹50,000
├─ Total with Interest (8%): ₹1,10,00,000
└─ Actual EMI: ₹61,111.11

CAR LOAN (Actual with 9% + ₹15,000 insurance):
├─ Principal: ₹8,00,000
├─ Insurance: ₹15,000
├─ Total with Interest (9%): ₹12,35,000
└─ Actual EMI: ₹20,583.33

═══════════════════════════════════════════════════

⚠️ NOTE: Array loop used BASE class references
   ∴ All EMIs calculated with 10% simple interest
   Derived class logic only works with derived references!
═══════════════════════════════════════════════════
```

## 🧮 EMI Calculation Formulas

### Base Loan EMI
```
EMI = [P + (P × R × T)] / (T × 12)
Where:
P = Principal Amount
R = Interest Rate (10% = 0.10)
T = Tenure in Years

Example: ₹10,00,000 for 10 years
= [10,00,000 + (10,00,000 × 0.10 × 10)] / (10 × 12)
= [10,00,000 + 10,00,000] / 120
= 20,00,000 / 120
= ₹16,666.67
```

### Home Loan EMI (with Processing Fee)
```
Effective Principal = P + (P × 0.01)  // Add 1% fee
EMI = [Effective Principal + (P × 0.08 × T)] / (T × 12)
```

### Car Loan EMI (with Insurance)
```
Effective Principal = P + 15,000  // Add insurance
EMI = [Effective Principal + (P × 0.09 × T)] / (T × 12)
```

## 🔍 Key Observations

### What Happens in the Array Loop?
```csharp
Loan[] loans = { homeLoan1, homeLoan2, carLoan1, carLoan2 };

foreach(Loan loan in loans)
{
    // ⚠️ THIS CALLS LOAN.CALCULATEEMI() - ALWAYS!
    // Even though objects are HomeLoan/CarLoan
    // Reference type = Loan → Base method executes
    decimal emi = loan.CalculateEMI();
}
```

### Why This Happens
| Factor | Explanation |
|--------|------------|
| **Method Hiding** | `new` keyword hides, doesn't override |
| **Static Binding** | Method call resolved at compile-time |
| **Reference Type** | Compiler looks at declared type (Loan) |
| **No Polymorphism** | Without virtual/override, no dynamic dispatch |

## ✅ Key Takeaways

1. **Method hiding is NOT polymorphism** - It's static binding
2. **Reference type determines method** - Not the actual object type
3. **Base class references** always call base class methods
4. **To use derived logic** - Must use derived class references
5. **Compiler warning** - CS0108: Hides inherited member

## 📝 Usage Scenarios

```csharp
// Scenario 1: Processing with base logic (as in array)
Loan[] allLoans = GetAllLoans();
foreach(Loan loan in allLoans)
{
    // Always gets base calculation (10% interest)
    decimal baseEMI = loan.CalculateEMI();
}

// Scenario 2: Getting actual loan-specific EMI
HomeLoan homeLoan = new HomeLoan("HL001", "John", 5000000, 15);
// Gets HomeLoan calculation (8% + fee)
decimal actualEMI = homeLoan.CalculateEMI();
```

---

**Happy Coding!** 🚀
