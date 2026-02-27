Below is a **complete, professional README.md** you can directly use for your repository.
It is **methods-focused**, **assessment-ready**, and suitable for **placements, viva, and interviews**.

---

# Sales Order Processing System (Methods-Focused)

**Console Application – .NET**

---

## 📌 Problem Statement

A retail company requires a **console-based Sales Order Processing System** built in .NET.
The main goal is **not UI**, but to design a **clean, maintainable, and method-driven solution** that processes weekly sales data.

This exercise emphasizes:

* Proper **method design**
* Clear **separation of responsibilities**
* Reusable and readable code structure

---

## 🏢 Business Scenario

* Daily sales are recorded for **7 days**
* Discounts are applied based on **weekly total**
* Tax is applied **after discount**
* All logic must be implemented using **well-defined methods**

---

## 🎯 Objective

Develop a system that:

* Accepts weekly sales data
* Calculates totals, averages, discounts, tax, and final payable amount
* Identifies highest and lowest sales days
* Categorizes sales using parallel arrays
* Uses **methods for every logical operation**

---

## ⚙ Functional Requirements

---

### 1️⃣ Sales Data Input

* Store sales using:

```csharp
decimal[] sales = new decimal[7];
```

* Input validation must be handled **inside a method**, not in `Main`

#### Method to Design

```csharp
static void ReadWeeklySales(decimal[] sales)
```

**Responsibilities**

* Read daily sales values
* Validate non-negative input
* Store values in the array

---

### 2️⃣ Total & Average Calculation

#### Methods to Design

```csharp
static decimal CalculateTotal(decimal[] sales)
static decimal CalculateAverage(decimal total, int days)
```

* Total weekly sales
* Average daily sales using `array.Length`

---

### 3️⃣ Highest & Lowest Sales Day

Determine:

* Highest sale amount and its day
* Lowest sale amount and its day

#### Methods to Design (Using `out`)

```csharp
static decimal FindHighestSale(decimal[] sales, out int day)
static decimal FindLowestSale(decimal[] sales, out int day)
```

📌 **Day numbering must be 1–7**

---

### 4️⃣ Discount Calculation (Method Overloading)

#### Discount Rules

* Total ≥ 50,000 → **10% discount**
* Otherwise → **5% discount**

#### Methods to Design

```csharp
static decimal CalculateDiscount(decimal total)
static decimal CalculateDiscount(decimal total, bool isFestivalWeek)
```

**Festival Week Rule**

* Extra **5% discount** if `isFestivalWeek == true`

---

### 5️⃣ Tax Calculation

* Apply **18% tax** on the discounted amount

#### Method to Design

```csharp
static decimal CalculateTax(decimal amount)
```

---

### 6️⃣ Final Payable Amount

Combine:

* Total sales
* Discount
* Tax

#### Method to Design

```csharp
static decimal CalculateFinalAmount(decimal total, decimal discount, decimal tax)
```

---

### 7️⃣ Sales Categorization (Parallel Array)

* Create a category array:

```csharp
string[] categories = new string[7];
```

#### Categorization Rules

| Sales Amount     | Category |
| ---------------- | -------- |
| `< 5,000`        | Low      |
| `5,000 – 15,000` | Medium   |
| `> 15,000`       | High     |

#### Method to Design

```csharp
static void GenerateSalesCategory(decimal[] sales, string[] categories)
```

📌 Index alignment between arrays must be maintained.

---

## 📄 Output Requirements

Display a clean, formatted report:

```
Weekly Sales Summary
--------------------
Total Sales        : 78,500.00
Average Daily Sale : 11,214.29

Highest Sale       : 18,000.00 (Day 6)
Lowest Sale        : 3,200.00  (Day 2)

Discount Applied   : 7,850.00
Tax Amount         : 12,168.30
Final Payable      : 82,818.30

Day-wise Category:
Day 1 : Medium
Day 2 : Low
...
Day 7 : High
```

✔ Aligned labels
✔ Clean formatting
✔ Readable summary

---

## 🚫 Technical Constraints (Strict)

Learners must:

| Rule                              | Status |
| --------------------------------- | ------ |
| Use methods for all logic         | ✔      |
| Arrays only                       | ✔      |
| Method overloading                | ✔      |
| `out` parameters                  | ✔      |
| `List<T>` / LINQ                  | ❌      |
| Logic inside `Main`               | ❌      |
| Duplicate logic                   | ❌      |
| Hard-coded calculations in `Main` | ❌      |

---

## 🎓 Learning Objectives (What This Tests)

This single exercise validates:

| Concept                         | Tested |
| ------------------------------- | ------ |
| Method definition vs invocation | ✔      |
| Parameters vs arguments         | ✔      |
| Return types vs `void`          | ✔      |
| `out` parameters                | ✔      |
| Method overloading              | ✔      |
| Separation of concerns          | ✔      |
| Maintainable architecture       | ✔      |

---

## 🛠 Technologies Used

* C#
* .NET Console Application
* Visual Studio

---

## 📌 Key Takeaway

This project reinforces **real-world coding discipline** by forcing:

* Modular thinking
* Clear method responsibilities
* Readable, maintainable code

These skills are **critical for backend roles, interviews, and enterprise development**.

---

If you want next:

* ✅ **Complete implementation code**
* ✅ **Method-by-method explanation**
* ✅ **Interview questions from this problem**
* ✅ **Refactoring tips for best practices**

Just tell me 👍
