Here is a **complete, clean, assessment-ready README.md** you can directly use for your repository.

It is written in a **professional + academic style**, suitable for **college assessment, interviews, and placements**.

---

# Weekly Sales Analysis System

**Console Application – .NET (Array-Based Processing)**

---

## 📌 Background

A retail outlet records **daily sales figures for a week (7 days)**.
Management requires a **console-based utility** to analyze weekly performance **using arrays only**, without advanced collections or LINQ.

This project focuses on **fundamental array logic**, manual calculations, and clean reporting.

---

## 🎯 Objective

Develop a **Weekly Sales Analysis System** that:

* Stores sales data for 7 consecutive days
* Performs statistical analysis
* Categorizes daily sales
* Displays a structured weekly report

⚠️ **Only array-based logic is allowed**

---

## ⚙ Functional Requirements

---

### 1️⃣ Data Capture

* Use a **single-dimensional array**:

```csharp
decimal[] dailySales = new decimal[7];
```

* Accept sales input for **Day 1 to Day 7**
* Input validation:

  * Sales value must be **≥ 0**
  * Invalid input must prompt **re-entry for the same day**

---

### 2️⃣ Weekly Sales Analysis

Using **array traversal (`for` loops only)**, compute:

#### ✅ Total Weekly Sales

* Sum of all sales values

#### ✅ Average Daily Sales

* Use:

```csharp
average = total / dailySales.Length;
```

#### ✅ Highest Sales Day

* Identify:

  * Highest sales amount
  * Corresponding day number (1–7)

#### ✅ Lowest Sales Day

* Identify:

  * Lowest sales amount
  * Corresponding day number (1–7)

#### ✅ Days Above Average

* Count how many days recorded sales **greater than weekly average**

---

### 3️⃣ Sales Categorization (Parallel Array)

* Create a second array:

```csharp
string[] salesCategory = new string[7];
```

* Categorize each day based on sales value:

| Sales Amount     | Category |
| ---------------- | -------- |
| `< 5,000`        | Low      |
| `5,000 – 15,000` | Medium   |
| `> 15,000`       | High     |

📌 **Index alignment must be maintained** between both arrays.

---

### 4️⃣ Output Report

Display a formatted weekly report:

```
Weekly Sales Report
-------------------
Total Sales        : 82,500.00
Average Daily Sale : 11,785.71

Highest Sale       : 18,200.00 (Day 6)
Lowest Sale        : 3,400.00  (Day 2)

Days Above Average : 3
```

#### Day-wise Sales Category Summary

```
Day 1 : Medium
Day 2 : Low
Day 3 : High
Day 4 : Medium
Day 5 : Medium
Day 6 : High
Day 7 : Medium
```

✔ Clean formatting
✔ Aligned labels
✔ Readable output

---

## 🚫 Technical Constraints (Strict)

| Rule                       | Allowed |
| -------------------------- | ------- |
| Arrays only                | ✔       |
| `for` loops                | ✔       |
| `foreach` (display only)   | ✔       |
| `List<T>`                  | ❌       |
| LINQ                       | ❌       |
| `Array.Sort()` for min/max | ❌       |

---

## 🧠 Learning Outcomes

After completing this exercise, learners will be able to:

* ✔ Use **array indexing** effectively
* ✔ Implement **parallel arrays**
* ✔ Perform **manual aggregation & comparison**
* ✔ Track indexes while finding min/max values
* ✔ Convert **real-world business requirements** into array logic
* ✔ Write clean and maintainable procedural code

---

## 🛠 Technologies Used

* C#
* .NET Console Application
* Visual Studio

---

## 📌 Key Takeaway

This project strengthens **core programming fundamentals**, especially:

* Data storage using arrays
* Manual computation logic
* Real-world problem solving without shortcuts

These skills are **critical for interviews, exams, and system-level programming**.

---

If you want next:

* ✅ **Complete C# implementation**
* ✅ **Flowchart / dry-run explanation**
* ✅ **Interview questions from this problem**
* ✅ **Optimized logic walkthrough**

Just tell me 👍
