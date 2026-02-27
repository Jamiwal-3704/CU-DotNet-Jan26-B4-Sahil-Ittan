# ☁️ The SaaS Architect - Subscription Management System

## 📋 Project Overview

A comprehensive console-based Subscription Management System designed for a Software-as-a-Service (SaaS) platform. This project demonstrates advanced object-oriented programming concepts through a real-world billing and subscriber management scenario.

## 🎯 Educational Objective

This capstone project reinforces mastery of:

* **Abstraction & Inheritance** - Building class hierarchies
* **Polymorphism** - Runtime behavior differentiation
* **Equality & Comparison** - Object identity and sorting
* **Collection Management** - Dictionaries and ordered collections
* **LINQ Operations** - Data transformation and sorting
* **Reporting** - Formatted output generation

---

## 🏗️ System Architecture

**text**

```
┌─────────────────────────────────────────────────────────────┐
│                    SAAS SUBSCRIPTION                        │
│                    MANAGEMENT SYSTEM                         │
└─────────────────────────────────────────────────────────────┘
                              │
            ┌─────────────────┼─────────────────┐
            │                 │                 │
            ▼                 ▼                 ▼
    ┌─────────────┐   ┌─────────────┐   ┌─────────────┐
    │  Subscriber │   │  Dictionary │   │   Report    │
    │  Hierarchy  │   │  Management │   │  Generator  │
    └─────────────┘   └─────────────┘   └─────────────┘
            │                 │                 │
            └─────────────────┼─────────────────┘
                              │
                              ▼
              ┌─────────────────────────────┐
              │    IComparable & Equals     │
              │      Implementation         │
              └─────────────────────────────┘
```

---

## 📊 Component Breakdown

### 1️⃣ The Foundation: Abstraction & Inheritance

#### Abstract Base Class: `Subscriber`

| Component                 | Purpose                                                                                                              |
| ------------------------- | -------------------------------------------------------------------------------------------------------------------- |
| **Properties**      | `Guid ID` - Unique identifier`string Name` - Subscriber name`DateTime JoinDate` - Subscription start |
| **Abstract Method** | `decimal CalculateMonthlyBill()` - Must be implemented by derived classes                                          |

#### Derived Subscriber Types

| Subclass                     | Properties                                        | Billing Formula                |
| ---------------------------- | ------------------------------------------------- | ------------------------------ |
| **BusinessSubscriber** | `decimal FixedRate``decimal TaxRate`     | `FixedRate × (1 + TaxRate)` |
| **ConsumerSubscriber** | `double DataUsageGB``decimal PricePerGB` | `DataUsageGB × PricePerGB`  |

### 2️⃣ Identity & Comparison Logic

#### Equality Override (`Equals` & `GetHashCode`)

* **Rule** : Two subscribers are equal if they have the same `Guid ID`
* **Purpose** : Enables dictionary lookups by ID rather than reference
* **Impact** : `myDict.ContainsKey(subscriber)` works based on business identity

#### Comparable Implementation (`IComparable<Subscriber>`)

| Sort Priority       | Field    | Direction                     |
| ------------------- | -------- | ----------------------------- |
| **Primary**   | JoinDate | Ascending (earliest first)    |
| **Secondary** | Name     | Alphabetical (if dates equal) |

### 3️⃣ Data Structure: Dictionary with Email Key

#### Primary Storage

**plaintext**

```
Dictionary<string, Subscriber>
├── Key: Email address (string)
└── Value: Subscriber object
```

#### Sample Data Requirements

* Minimum **5 mixed subscribers** (combination of Business and Consumer)
* Each with unique Email (key) and GUID (identifier)

#### The Sorting Challenge

| Challenge                             | Solution                             |
| ------------------------------------- | ------------------------------------ |
| Dictionary is unordered by default    | Cannot rely on inherent ordering     |
| Need to sort by monthly bill          | Must transform to ordered collection |
| Report requires descending bill order | Sort highest to lowest               |

**Solution Approaches:**

* Convert to `List<KeyValuePair<string, Subscriber>>`
* Use LINQ's `OrderByDescending()`
* Or implement custom `SortedDictionary` comparer

### 4️⃣ Polymorphic Reporting

#### `ReportGenerator` Class

* **Static Method** : `PrintRevenueReport(IEnumerable<Subscriber> subscribers)`
* **Key Feature** : Polymorphic method calls
* **Input** : Any enumerable collection of subscribers

#### Reporting Behavior

**plaintext**

```
For each subscriber in collection:
    └── Call CalculateMonthlyBill() (runtime decides which version)
        ├── Business: Uses FixedRate + TaxRate formula
        └── Consumer: Uses DataUsageGB × PricePerGB formula
```

---

## 🔄 System Workflow

**text**

```
┌─────────────────────────────────────────────────────────────┐
│                           START                              │
└───────────────────────────┬─────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                    Initialize System                         │
│    Create Dictionary<string, Subscriber>                     │
└───────────────────────────┬─────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                  Create Subscriber Objects                   │
│    ├── BusinessSubscriber (3+ examples)                     │
│    │   ├─ FixedRate: Various amounts                        │
│    │   └─ TaxRate: Various percentages                      │
│    └── ConsumerSubscriber (2+ examples)                     │
│        ├─ DataUsageGB: Various amounts                      │
│        └─ PricePerGB: Various rates                         │
└───────────────────────────┬─────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                  Add to Dictionary                           │
│    Key: Email Address                                        │
│    Value: Subscriber object                                  │
└───────────────────────────┬─────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│              Sort Subscribers by Monthly Bill                │
│    └── Extract values from Dictionary                       │
│    └── Calculate monthly bill for each                      │
│    └── Order by bill amount (Descending)                    │
│    └── Store in ordered collection                          │
└───────────────────────────┬─────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                  Generate Revenue Report                     │
│    └── Pass ordered collection to ReportGenerator           │
│    └── Iterate through each subscriber                      │
│    └── Call CalculateMonthlyBill() polymorphically          │
│    └── Format table-style output                            │
│    └── Include subscriber type identification               │
└───────────────────────────┬─────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                    Display Formatted Report                  │
│    └── Show sorted list with:                               │
│        ├─ Email / Name                                       │
│        ├─ Subscriber Type                                    │
│        ├─ Calculated Monthly Bill                            │
│        ├─ Join Date                                          │
│        └─ Type-specific parameters                           │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                             END                               │
└─────────────────────────────────────────────────────────────┘
```

---

## 📊 Sample Report Output Format

**text**

```
================================================================================
                    SAAS REVENUE REPORT - By Monthly Bill
                          Generated: 2024-01-15
================================================================================

RANK | SUBSCRIBER           | TYPE     | JOIN DATE  | MONTHLY BILL | DETAILS
================================================================================
#1   | techcorp@email.com   | Business | 2023-01-10 |   $2,750.00  | Rate: $2500, Tax: 10%
#2   | netflix@email.com    | Business | 2023-03-15 |   $1,650.00  | Rate: $1500, Tax: 10%
#3   | heavyuser@email.com  | Consumer | 2023-02-20 |     $499.50  | Usage: 999GB @ $0.50/GB
#4   | smallbiz@email.com   | Business | 2023-04-01 |     $412.50  | Rate: $375, Tax: 10%
#5   | casual@email.com     | Consumer | 2023-05-10 |      $49.95  | Usage: 333GB @ $0.15/GB

================================================================================
TOTAL MONTHLY REVENUE: $5,361.95
AVERAGE BILL: $1,072.39
================================================================================

SUBSCRIBER BREAKDOWN:
┌──────────────────────────────────────────────────────────────────────────────┐
│ Business Subscribers: 3 | Avg Business Bill: $1,604.17                       │
│ Consumer Subscribers: 2 | Avg Consumer Bill: $274.73                         │
└──────────────────────────────────────────────────────────────────────────────┘
```

---

## 🧩 Key Implementation Details

### Abstraction Rules

* ❌ Cannot directly instantiate `Subscriber` class
* ✅ All concrete types must inherit from `Subscriber`
* ✅ Must override `CalculateMonthlyBill()` in derived classes

### Polymorphism Behavior

| Subscriber Type    | `CalculateMonthlyBill()` Implementation     |
| ------------------ | --------------------------------------------- |
| **Business** | `return FixedRate * (1 + TaxRate);`         |
| **Consumer** | `return (decimal)DataUsageGB * PricePerGB;` |

### Equality Logic

**plaintext**

```
Subscriber A equals Subscriber B if:
    └── A.ID == B.ID
    (regardless of Name, JoinDate, or other properties)
```

### Sorting Logic

**plaintext**

```
Compare(Subscriber x, Subscriber y):
    ├── If x.JoinDate != y.JoinDate:
    │   └── Return based on date (earlier first)
    └── Else (dates equal):
        └── Compare by Name alphabetically
```

### Dictionary Sorting Challenge

**The Problem:**

**text**

```
Dictionary<string, Subscriber> myDict = {...};
// Dictionary has no inherent order
// Cannot simply sort in place
```

**The Solution Flow:**

**text**

```
1. Access dictionary values: myDict.Values
2. Convert to List: .ToList()
3. Calculate bill for each subscriber
4. Sort by bill: .OrderByDescending(s => s.CalculateMonthlyBill())
5. Store in ordered collection (List or SortedDictionary)
```

---

## 💼 Business Logic Formulas

### Business Subscriber Calculation

| Component              | Description                    | Example             |
| ---------------------- | ------------------------------ | ------------------- |
| FixedRate              | Base subscription price        | $1,000.00           |
| TaxRate                | Business tax (as decimal)      | 0.10 (10%)          |
| **Monthly Bill** | `FixedRate × (1 + TaxRate)` | **$1,100.00** |

### Consumer Subscriber Calculation

| Component              | Description                   | Example          |
| ---------------------- | ----------------------------- | ---------------- |
| DataUsageGB            | Gigabytes used                | 500 GB           |
| PricePerGB             | Cost per gigabyte             | $0.15            |
| **Monthly Bill** | `DataUsageGB × PricePerGB` | **$75.00** |

---

## 🔍 Test Scenarios

### Scenario 1: Mixed Portfolio

**text**

```
Input:
├── Business: Fixed $2000, Tax 8% → Bill: $2,160
├── Business: Fixed $1500, Tax 10% → Bill: $1,650
├── Consumer: Usage 1000GB @ $0.50 → Bill: $500
├── Consumer: Usage 250GB @ $0.25 → Bill: $62.50
├── Business: Fixed $500, Tax 5% → Bill: $525

Sorted Order (Descending Bill):
1. Business: $2,160
2. Business: $1,650
3. Consumer: $500
4. Business: $525
5. Consumer: $62.50
```

### Scenario 2: Date-Based Sorting

**text**

```
If bills are equal, sort by JoinDate:
├── Sub A: Bill $100, JoinDate: Jan 1
├── Sub B: Bill $100, JoinDate: Feb 1
├── Sub C: Bill $100, JoinDate: Mar 1

Sorted by Date (Ascending):
1. Sub A (Jan 1)
2. Sub B (Feb 1)
3. Sub C (Mar 1)
```

---

## 📝 Report Formatting Requirements

### Table Structure

**text**

```
┌──────────┬──────────────┬──────────┬────────────┬──────────────┐
│   Email  │     Type     │Join Date │Monthly Bill│    Details   │
├──────────┼──────────────┼──────────┼────────────┼──────────────┤
│ user@e.co│ Business     │2023-01-01│  $1,200.00 │Rate: $1000   │
│         │              │          │            │Tax: 20%      │
└──────────┴──────────────┴──────────┴────────────┴──────────────┘
```

### Formatting Requirements

* Currency using `ToString("C")`
* Dates in ISO format (YYYY-MM-DD)
* Proper column alignment
* Type-specific details in last column

---

## 🎓 Learning Outcomes

By completing this project, students demonstrate:

| Concept                 | Implementation                                    |
| ----------------------- | ------------------------------------------------- |
| **Abstraction**   | Abstract Subscriber class with core properties    |
| **Inheritance**   | Business and Consumer derived classes             |
| **Polymorphism**  | Different billing calculations per type           |
| **Encapsulation** | Protected/private member access                   |
| **Equality**      | Override Equals/GetHashCode for business identity |
| **Comparable**    | IComparable implementation for sorting            |
| **Collections**   | Dictionary with custom objects                    |
| **LINQ**          | Sorting and filtering collections                 |
| **Reporting**     | Formatted output with StringBuilder               |

---

## 🚀 Extension Ideas

1. **Tiered Pricing** : Add volume discounts for high-usage consumers
2. **Annual Plans** : Support prepaid annual subscriptions with discount
3. **Referral Tracking** : Add referral credits to monthly bills
4. **Multi-Currency** : Support different currencies per subscriber
5. **Invoice Generation** : Create PDF invoices for each subscriber
6. **Payment Processing** : Simulate payment collection
7. **Cancellation Logic** : Handle subscriber churn
8. **Promotional Codes** : Apply discounts based on promo codes
