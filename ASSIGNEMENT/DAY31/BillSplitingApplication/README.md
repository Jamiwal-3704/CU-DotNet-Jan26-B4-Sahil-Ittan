# 💰 Bill Splitting Application - Expense Splitter

## 📋 Overview

A console-based expense splitting application that calculates fair share distribution among group members and determines the optimal settlement transactions. This application implements a queue-based algorithm to minimize the number of transactions needed to settle debts.

## 🎯 Purpose

The application solves the common problem of splitting expenses among friends, colleagues, or group members by:

* Calculating each person's fair share of total expenses
* Identifying who owes money (debtors) and who should receive money (creditors)
* Generating a minimal list of settlement transactions

## 🔧 How It Works

### Core Algorithm

1. **Calculate Total & Share** : Sums all expenses and divides by number of participants
2. **Categorize Participants** :

* **Receivers (Creditors)** : People who paid more than their share
* **Payers (Debtors)** : People who paid less than their share

1. **Generate Settlements** : Uses a queue-based matching algorithm to create optimal payment instructions

### Key Components

#### Data Structure

**csharp**

```
Dictionary<string, double> expenses
```

* **Key** : Person's name (string)
* **Value** : Amount paid (double)

#### Settlement Format

Each settlement transaction follows the format:

**text**

```
[Payer],[Receiver],[Amount]
```

Example: `"Sunil,Aman,300"` means Sunil pays Aman ₹300

### Algorithm Flow

**text**

```
┌─────────────────────────┐
│   Input: Dictionary     │
│   of expenses           │
└───────────┬─────────────┘
            ↓
┌─────────────────────────┐
│ Calculate total & share │
│ share = total / count   │
└───────────┬─────────────┘
            ↓
┌─────────────────────────┐
│ Split into two queues:  │
│ ┌─────────┐ ┌─────────┐│
│ │Payers   │ │Receivers││
│ │(owe)    │ │(get)    ││
│ └────┬────┘ └────┬────┘│
└──────┼───────────┼──────┘
       ↓           ↓
┌─────────────────────────┐
│ While both queues have  │
│        items:           │
└───────────┬─────────────┘
            ↓
┌─────────────────────────┐
│ Dequeue one payer and   │
│ one receiver            │
└───────────┬─────────────┘
            ↓
┌─────────────────────────┐
│ Calculate payment =     │
│ Min(payer.amount,       │
│     receiver.amount)    │
└───────────┬─────────────┘
            ↓
┌─────────────────────────┐
│ Add settlement record   │
│ "Payer,Receiver,amount" │
└───────────┬─────────────┘
            ↓
┌─────────────────────────┐
│ If amounts remain:      │
│ Re-enqueue with balance │
└───────────┬─────────────┘
            ↓
            ↺
            ↓
┌─────────────────────────┐
│   Return settlements    │
│        list             │
└─────────────────────────┘
```

## 📊 Example Walkthrough

### Input

**csharp**

```
Dictionary<string, double> expenses = new Dictionary<string, double>()
{
    {"Aman", 900},
    {"Sunil", 0},
    {"Kartik", 1290}
};
```

### Calculation Steps

| Person | Paid   | Share (730) | Difference | Status   |
| ------ | ------ | ----------- | ---------- | -------- |
| Aman   | ₹900  | ₹730       | +₹170     | Receiver |
| Sunil  | ₹0    | ₹730       | -₹730     | Payer    |
| Kartik | ₹1290 | ₹730       | +₹560     | Receiver |

### Settlement Process

1. **First Transaction** : Sunil pays Aman ₹170

* Sunil balance: ₹730 → ₹560 remaining
* Aman balance: ₹170 → ₹0 settled

1. **Second Transaction** : Sunil pays Kartik ₹560

* Sunil balance: ₹560 → ₹0 settled
* Kartik balance: ₹560 → ₹0 settled

### Output

**text**

```
Sunil,Aman,170
Sunil,Kartik,560
```

## 🚀 Features

### Current Features

* ✅ Calculates fair share distribution
* ✅ Identifies creditors and debtors automatically
* ✅ Generates minimal transaction settlements
* ✅ Handles any number of participants
* ✅ Works with zero or negative contributions

### Potential Enhancements

* 🔲 Input validation and error handling
* 🔲 Support for unequal sharing (weights/percentages)
* 🔲 Transaction rounding (to nearest rupee/dollar)
* 🔲 Export settlements to CSV
* 🔲 GUI interface
* 🔲 Payment method suggestions (UPI, cash, etc.)

## 💻 Technical Implementation

### Class Structure

* **Class** : `ExpenceSplitter`
* **Main Method** : `SettleExpenceShare()`
* **Return Type** : `List<string>` containing settlement instructions

### Key Methods Used

* `Sum()` - LINQ extension for total calculation
* `Enqueue()`/`Dequeue()` - Queue operations
* `Math.Abs()` - Absolute value for balance calculations
* `Math.Min()` - Determine optimal payment amount

### Time Complexity

* **O(n)** for initial categorization
* **O(m)** for settlement generation where m ≤ n-1
* **Overall** : Linear time complexity O(n)

## 📝 Usage Instructions

### Running the Application

1. Compile the C# program
2. Modify the expenses dictionary in Main()
3. Run the executable
4. View settlement transactions in console

### Customizing Input

**csharp**

```
Dictionary<string, double> expenses = new Dictionary<string, double>()
{
    {"Person1", amount1},
    {"Person2", amount2},
    {"Person3", amount3}
    // Add more as needed
};
```

### Reading Output

Each line represents a payment:

* **First value** : Who pays
* **Second value** : Who receives
* **Third value** : Amount to transfer

## 🔍 Edge Cases Handled

* ✅ Zero payments (person paid nothing)
* ✅ Equal shares (no settlements needed)
* ✅ Single person (trivial case)
* ✅ Decimal amounts
* ✅ Any number of participants

## 🎓 Learning Outcomes

This project demonstrates:

* Queue data structure implementation
* Dictionary manipulation
* LINQ operations (Sum)
* Algorithm design for optimization
* Real-world problem solving
* Object-oriented programming concepts

## 📁 File Structure

**text**

```
BillSplittingApplication/
│
├── ExpenceSplitter.cs      # Main application logic
├── README.md                # Documentation
└── bin/                     # Compiled output
    └── Debug/
        └── BillSplittingApplication.exe
```

## 🤝 Use Cases

* 🍽️ Restaurant bills with friends
* 🏠 Roommate expense sharing
* 🚗 Road trip cost splitting
* 🎉 Party expense management
* 🏢 Team lunch settlements
