# Utility Billing System – Tariff Calculation Engine

A C# billing system demonstrating abstract classes, method overriding, and runtime polymorphism for different utility types.

## 📋 Overview

This system calculates monthly bills for different utility connections (Electricity, Water, Gas) using a common abstract base class `UtilityBill`. Each utility type implements its own calculation logic while maintaining extensibility for future utility types.

## 🏗️ Class Hierarchy

```
┌─────────────────────────────────────────────────────────────┐
│                    UTILITYBILL (ABSTRACT)                    │
│                       (Base Class)                           │
├─────────────────────────────────────────────────────────────┤
│  + ConsumerId : int                                          │
│  + ConsumerName : string                                     │
│  + UnitsConsumed : decimal                                   │
│  + RatePerUnit : decimal                                     │
├─────────────────────────────────────────────────────────────┤
│  + abstract CalculateBillAmount() : decimal                  │
│  + virtual CalculateTax(decimal) : decimal  (5% default)    │
│  + concrete PrintBill() : void                               │
└─────────────────────────────────┬───────────────────────────┘
                                  │
        ┌─────────────────────────┼─────────────────────────┐
        │                         │                         │
        ▼                         ▼                         ▼
┌───────────────┐         ┌───────────────┐         ┌───────────────┐
│ ELECTRICITYBILL│         │   WATERBILL   │         │    GASBILL    │
├───────────────┤         ├───────────────┤         ├───────────────┤
│ Rules:        │         │ Rules:        │         │ Rules:        │
│ • 10% surcharge│         │ • Flat rate   │         │ • ₹150 fixed  │
│   if >300 units│         │ • No surcharge│         │   charge      │
│ • Default tax  │         │ • 2% tax      │         │ • 0% tax      │
│   (5%)         │         │   (overridden)│         │   (overridden)│
├───────────────┤         ├───────────────┤         ├───────────────┤
│ Override:     │         │ Override:     │         │ Override:     │
│ • BillAmount  │         │ • BillAmount  │         │ • BillAmount  │
│                │         │ • CalculateTax │         │ • CalculateTax │
└───────────────┘         └───────────────┘         └───────────────┘
```

## 💡 Billing Rules by Utility Type

| Utility | Bill Calculation | Tax Rule | Special Conditions |
|---------|-----------------|----------|-------------------|
| **Electricity** | Units × RatePerUnit | 5% (default) | +10% surcharge if >300 units |
| **Water** | Units × RatePerUnit | 2% (overridden) | No surcharge |
| **Gas** | (Units × RatePerUnit) + ₹150 | 0% (overridden) | Fixed monthly charge |

## 🔄 Polymorphic Execution Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    RUNTIME EXECUTION FLOW                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   START                                                          │
│     │                                                            │
│     ▼                                                            │
│   Create Utility Collection                                      │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │ List<UtilityBill> bills = new List<UtilityBill> {       │   │
│   │   new ElectricityBill(101, "John", 350, 8),            │   │
│   │   new WaterBill(201, "Emma", 45, 12),                   │   │
│   │   new GasBill(301, "Mike", 20, 25)                      │   │
│   │ };                                                        │   │
│   └─────────────────────────────────────────────────────────┘   │
│                                                                  │
│     ▼                                                            │
│   FOREACH LOOP (Polymorphic Calls)                              │
│     │                                                            │
│     └──► UtilityBill: Electricity (350 units)                   │
│           │                                                      │
│           ├──► CalculateBillAmount():                           │
│           │     • Base: 350 × 8 = ₹2800                         │
│           │     • Surcharge: 10% = ₹280 ( >300 units)          │
│           │     • Total Bill: ₹3080                             │
│           │                                                      │
│           ├──► CalculateTax(): 5% of ₹3080 = ₹154              │
│           │     ⚠ DEFAULT TAX (not overridden)                  │
│           │                                                      │
│           └──► PrintBill(): Displays ₹3234 total               │
│                                                                  │
│     └──► UtilityBill: Water (45 units)                          │
│           │                                                      │
│           ├──► CalculateBillAmount():                           │
│           │     • 45 × 12 = ₹540                                │
│           │                                                      │
│           ├──► CalculateTax(): 2% of ₹540 = ₹10.8              │
│           │     ✓ OVERRIDDEN TAX (2% instead of 5%)             │
│           │                                                      │
│           └──► PrintBill(): Displays ₹550.8 total              │
│                                                                  │
│     └──► UtilityBill: Gas (20 units)                            │
│           │                                                      │
│           ├──► CalculateBillAmount():                           │
│           │     • (20 × 25) + 150 = ₹500 + 150 = ₹650          │
│           │                                                      │
│           ├──► CalculateTax(): 0% of ₹650 = ₹0                 │
│           │     ✓ OVERRIDDEN TAX (0% instead of 5%)             │
│           │                                                      │
│           └──► PrintBill(): Displays ₹650 total                 │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📝 Sample Output

```
═══════════════════════════════════════════════════════════════
            UTILITY BILLING SYSTEM - TARIFF CALCULATOR
═══════════════════════════════════════════════════════════════

ELECTRICITY BILL
───────────────────────────────────────────────────────────────
Consumer ID    : 101
Consumer Name  : John
Units Consumed : 350
Rate Per Unit  : ₹8.00

Bill Breakdown:
├─ Base Amount : ₹2,800.00 (350 units × ₹8.00)
├─ Surcharge   : ₹280.00 (10% on >300 units)
├─ Bill Amount : ₹3,080.00
├─ Tax (5%)    : ₹154.00
└─ TOTAL PAYABLE: ₹3,234.00
───────────────────────────────────────────────────────────────

WATER BILL
───────────────────────────────────────────────────────────────
Consumer ID    : 201
Consumer Name  : Emma
Units Consumed : 45
Rate Per Unit  : ₹12.00

Bill Breakdown:
├─ Bill Amount : ₹540.00 (45 units × ₹12.00)
├─ Tax (2%)    : ₹10.80
└─ TOTAL PAYABLE: ₹550.80
───────────────────────────────────────────────────────────────

GAS BILL
───────────────────────────────────────────────────────────────
Consumer ID    : 301
Consumer Name  : Mike
Units Consumed : 20
Rate Per Unit  : ₹25.00

Bill Breakdown:
├─ Usage Charge : ₹500.00 (20 units × ₹25.00)
├─ Fixed Charge : ₹150.00 (monthly)
├─ Bill Amount  : ₹650.00
├─ Tax (0%)     : ₹0.00
└─ TOTAL PAYABLE: ₹650.00
───────────────────────────────────────────────────────────────

═══════════════════════════════════════════════════════════════
              END OF BILLING CYCLE
═══════════════════════════════════════════════════════════════
```

## 🧠 Polymorphism Deep Dive

### Method Resolution at Runtime

```
┌─────────────────────────────────────────────────────────────────┐
│                    METHOD CALL RESOLUTION TABLE                   │
├──────────────┬─────────────────┬─────────────────┬─────────────┤
│  OBJECT      │ CalculateBill   │ CalculateTax    │ PrintBill   │
│  TYPE        │   Amount()      │                 │  (concrete) │
├──────────────┼─────────────────┼─────────────────┼─────────────┤
│ Electricity  │ Electricity     │ UtilityBill     │ UtilityBill │
│              │ (with surcharge) │ (5% default)    │             │
├──────────────┼─────────────────┼─────────────────┼─────────────┤
│ Water        │ Water           │ Water           │ UtilityBill │
│              │ (flat rate)      │ (2% override)   │             │
├──────────────┼─────────────────┼─────────────────┼─────────────┤
│ Gas          │ Gas             │ Gas             │ UtilityBill │
│              │ (+₹150 fixed)    │ (0% override)   │             │
└──────────────┴─────────────────┴─────────────────┴─────────────┘

KEY INSIGHTS:
═══════════════
1. Abstract method (CalculateBillAmount) → MUST be overridden by ALL
2. Virtual method (CalculateTax) → OPTIONALLY overridden
3. Concrete method (PrintBill) → SAME for ALL, but uses overridden methods
4. Template pattern: PrintBill defines algorithm, derived classes provide specifics
```

## 🎯 Design Pattern: Template Method

```
┌─────────────────────────────────────────────────────────────────┐
│                    TEMPLATE METHOD PATTERN                       │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   public void PrintBill()  ←─── CONCRETE TEMPLATE METHOD        │
│   {                                                              │
│       // Step 1: Calculate base amount                          │
│       decimal billAmount = CalculateBillAmount();  ←─┐         │
│                                                       │         │
│       // Step 2: Calculate tax                        │         │
│       decimal tax = CalculateTax(billAmount);    ←──┼─┤         │
│                                                       │         │
│       // Step 3: Display results                      │ VARIABLE │
│       Console.WriteLine(...);                         │ PARTS   │
│   }                                                    │         │
│                                                       │         │
│   // Abstract/Virtual methods ────────────────────────┘         │
│   public abstract decimal CalculateBillAmount();                 │
│   public virtual decimal CalculateTax(decimal amount)            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## ✅ Open/Closed Principle Demonstrated

```
                    ┌─────────────────────┐
                    │     EXISTING        │
                    │   UTILITY TYPES     │
                    │  (Closed for mod)   │
                    └──────────┬──────────┘
                               │
    ┌──────────────────────────┼──────────────────────────┐
    │                          │                          │
    ▼                          ▼                          ▼
┌────────┐                ┌────────┐                ┌────────┐
│Electric│                │ Water  │                │  Gas   │
│  Bill  │                │  Bill  │                │  Bill  │
└────────┘                └────────┘                └────────┘

                    ┌─────────────────────┐
                    │     NEW TYPES       │
                    │  (Open for extension)│
                    └──────────┬──────────┘
                               │
                    ┌──────────┴──────────┐
                    │                      │
                    ▼                      ▼
                ┌────────┐             ┌────────┐
                │ Solar  │             │Steam   │
                │  Bill  │             │ Bill   │
                └────────┘             └────────┘
```

## 🔍 Key Design Decisions

| Design Element | Why It's Used |
|----------------|---------------|
| **Abstract Class** | Provides common properties + partial implementation |
| **Abstract Method** | Forces each utility to define its own calculation |
| **Virtual Method** | Allows optional tax rule customization |
| **Concrete Method** | Ensures consistent billing process (Template Pattern) |
| **List<UtilityBill>** | Enables polymorphic processing of all bill types |

## 🎓 Learning Objectives Achieved

✅ **Abstract class usage** - UtilityBill cannot be instantiated directly  
✅ **Abstract method implementation** - Each utility implements CalculateBillAmount()  
✅ **Virtual method with optional override** - Tax calculation customized per utility  
✅ **Method overriding** - All derived classes override base methods  
✅ **Runtime polymorphism** - Correct method called based on actual object type  
✅ **Template method pattern** - PrintBill defines algorithm, derived classes fill details  
✅ **Open/Closed Principle** - New utilities can be added without modifying existing code  

---

**Happy Billing!** 🧾