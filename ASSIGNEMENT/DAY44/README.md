# ☁️ The SaaS Architect - Subscription Management System

## 📋 Project Overview
A console-based Subscription Management System for a SaaS platform that demonstrates advanced OOP concepts. The system manages different subscriber types, calculates monthly bills, and generates revenue reports.

---

## 🎯 Objective
Build a system that:
- Manages Business and Consumer subscribers
- Calculates monthly bills using type-specific formulas
- Sorts subscribers by bill amount for reporting
- Demonstrates abstraction, polymorphism, equality, and sorting

---

## 🏗️ Core Components

### 1️⃣ Subscriber Hierarchy

```
┌─────────────────────────────────────┐
│      Subscriber (Abstract)          │
│  └── Guid ID                         │
│  └── string Name                      │
│  └── DateTime JoinDate                │
│  └── CalculateMonthlyBill() (abs)    │
└───────────────┬─────────────────────┘
                │
        ┌───────┴───────┐
        │               │
        ▼               ▼
┌───────────────┐ ┌───────────────┐
│  Business     │ │  Consumer     │
│  Subscriber   │ │  Subscriber   │
├───────────────┤ ├───────────────┤
│ FixedRate     │ │ DataUsageGB   │
│ TaxRate       │ │ PricePerGB    │
└───────────────┘ └───────────────┘
```

---

### 2️⃣ Billing Formulas

| Subscriber Type | Formula | Example |
|-----------------|---------|---------|
| **Business** | `FixedRate × (1 + TaxRate)` | ₹1000 × 1.10 = ₹1100 |
| **Consumer** | `DataUsageGB × PricePerGB` | 500GB × ₹2 = ₹1000 |

---

### 3️⃣ Identity & Comparison

| Feature | Implementation | Purpose |
|---------|---------------|---------|
| **Equals()** | Compare by GUID ID | Business identity, not reference |
| **GetHashCode()** | Hash based on ID | Dictionary lookup efficiency |
| **IComparable** | Sort by JoinDate → Name | Default ordering |

---

### 4️⃣ Data Structure

**Primary Storage:**
```csharp
Dictionary<string, Subscriber> subscribers
├── Key: Email address
└── Value: Subscriber object
```

**The Sorting Challenge:**
| Issue | Solution |
|-------|----------|
| Dictionary is unordered | Convert to ordered collection |
| Need sort by monthly bill | Calculate → OrderBy → Store |

**Approach:**
```
1. Access dictionary values: subscribers.Values
2. Calculate monthly bill for each
3. Sort descending by bill amount
4. Store in List<KeyValuePair<string, Subscriber>>
```

---

### 5️⃣ Polymorphic Reporting

**ReportGenerator.PrintRevenueReport()**
- Takes `IEnumerable<Subscriber>` as input
- Iterates through collection
- Calls `CalculateMonthlyBill()` polymorphically
- Formats table-style output with StringBuilder

---

## 📊 Sample Report Output

```
================================================================================
                    SAAS REVENUE REPORT - By Monthly Bill
================================================================================
Email               | Type     | Join Date  | Monthly Bill | Details
────────────────────┼──────────┼────────────┼──────────────┼──────────────────
techcorp@email.com  | Business | 2023-01-10 | ₹2,750.00    | Rate: ₹2500, Tax:10%
netflix@email.com   | Business | 2023-03-15 | ₹1,650.00    | Rate: ₹1500, Tax:10%
heavyuser@email.com | Consumer | 2023-02-20 | ₹999.00      | Usage: 999GB @ ₹1/GB
smallbiz@email.com  | Business | 2023-04-01 | ₹412.50      | Rate: ₹375, Tax:10%
casual@email.com    | Consumer | 2023-05-10 | ₹49.95       | Usage: 333GB @ ₹0.15/GB
================================================================================
TOTAL MONTHLY REVENUE: ₹5,861.45
================================================================================
```

---

## 🔧 Key Requirements Checklist

| Feature | Implementation |
|---------|---------------|
| **Abstraction** | Subscriber class cannot be instantiated directly |
| **Inheritance** | Business and Consumer derive from Subscriber |
| **Polymorphism** | Different billing calculations per type |
| **Equality** | Override Equals/GetHashCode based on GUID |
| **Comparable** | IComparable for JoinDate + Name sorting |
| **Dictionary** | Email as key, Subscriber as value |
| **Sorting** | Order by monthly bill descending |
| **Reporting** | Static method with IEnumerable input |

---

## 💼 Sample Subscriber Data

| Email | Type | JoinDate | Business Data | Consumer Data |
|-------|------|----------|---------------|---------------|
| tech@co.com | Business | 2023-01-10 | Fixed: 2500, Tax: 10% | - |
| user@home.com | Consumer | 2023-02-20 | - | Usage: 500GB, Price: ₹2 |
| startup@io.com | Business | 2023-03-15 | Fixed: 1500, Tax: 10% | - |
| family@mail.com | Consumer | 2023-04-01 | - | Usage: 200GB, Price: ₹1.5 |
| corp@ltd.com | Business | 2023-05-10 | Fixed: 5000, Tax: 8% | - |

---

## 📝 Quick Summary

This project demonstrates:
- **OOP Fundamentals**: Abstraction, Inheritance, Polymorphism
- **Identity Management**: Proper Equals/GetHashCode overrides
- **Sorting Logic**: IComparable implementation
- **Collection Mastery**: Dictionary + LINQ sorting
- **Polymorphic Reporting**: Type-specific behavior at runtime

Perfect for understanding real-world subscription billing systems!