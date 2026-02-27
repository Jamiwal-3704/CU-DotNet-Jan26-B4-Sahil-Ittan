# 📊 Northwind Database - SQL Query Practice Levels

## 📋 Project Overview

A comprehensive SQL query practice guide based on the classic Northwind database. This collection presents **20 progressively challenging queries** organized into four distinct skill levels, designed to build proficiency from basic joins to complex correlated subqueries.

## 🎯 Purpose

This practice guide helps learners master SQL querying by working with a realistic business database (Northwind), covering:

* Basic data retrieval across multiple tables
* Aggregation and grouping techniques
* Advanced filtering with subqueries
* Complex business logic implementation

## 🏢 Database Schema Context

The Northwind database models a wholesale trading company with these core tables:

| Table                        | Contents                                         |
| ---------------------------- | ------------------------------------------------ |
| **Products**           | Product details, prices, supplier/category links |
| **Categories**         | Product categories                               |
| **Suppliers**          | Vendor information                               |
| **Customers**          | Client company details                           |
| **Orders**             | Sales transactions header                        |
| **Order Details**      | Line items for each order                        |
| **Employees**          | Staff information and hierarchy                  |
| **Shippers**           | Shipping companies                               |
| **Territories/Region** | Sales territory data                             |

## 📈 Learning Path: 4 Progressive Levels

---

## 🟢 Level 1: The Join Foundation

 **Focus** : Inner Joins and basic multi-table connections

### Queries Included

| # | Query Objective                                       | Tables Involved        |
| - | ----------------------------------------------------- | ---------------------- |
| 1 | List all Product Names with their Category Names      | Products → Categories |
| 2 | Display Order IDs with Customer Company Names         | Orders → Customers    |
| 3 | Show Product Names and their Supplier Company Names   | Products → Suppliers  |
| 4 | List Orders (ID, Date) with processing Employee names | Orders → Employees    |
| 5 | Find Orders shipped to France with Shipper Company    | Orders → Shippers     |

### Key Concepts

* `INNER JOIN` syntax
* Foreign key relationships
* Table aliasing
* Column selection and formatting

### Sample Query Pattern

**sql**

```
SELECT p.ProductName, c.CategoryName
FROM Products p
INNER JOIN Categories c ON p.CategoryID = c.CategoryID
```

---

## 🟡 Level 2: Aggregations with Joins

 **Focus** : Using GROUP BY across multiple tables

### Queries Included

| # | Query Objective                           | Tables Involved                      |
| - | ----------------------------------------- | ------------------------------------ |
| 1 | Category Name and total units in stock    | Categories → Products               |
| 2 | Customer Company Name and total spending  | Customers → Orders → Order Details |
| 3 | Employee Last Name and total orders taken | Employees → Orders                  |
| 4 | Total Freight charges per Shipper company | Shippers → Orders                   |
| 5 | Top 5 Products by total quantity sold     | Products → Order Details            |

### Key Concepts

* `GROUP BY` with multiple tables
* Aggregate functions (`SUM`, `COUNT`)
* `ORDER BY` with aggregation
* `TOP` / `LIMIT` clauses
* Calculated fields (Price × Quantity)

### Sample Query Pattern

**sql**

```
SELECT c.CompanyName, SUM(od.Quantity * od.UnitPrice) AS TotalSpent
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY c.CompanyName
```

---

## 🟠 Level 3: Subqueries & Self-Joins

 **Focus** : Nested queries and tables referencing themselves

### Queries Included

| # | Query Objective                   | Technique Used              |
| - | --------------------------------- | --------------------------- |
| 1 | Products priced above average     | Scalar subquery in WHERE    |
| 2 | Employees with their managers     | Self-join on Employees      |
| 3 | Customers with no orders          | NOT IN / NOT EXISTS         |
| 4 | Orders above average order value  | Subquery in HAVING          |
| 5 | Products never ordered after 1997 | NOT EXISTS with date filter |

### Key Concepts

* Subqueries in WHERE clause
* Self-joins for hierarchical data
* `NOT EXISTS` vs `NOT IN`
* Correlated vs non-correlated subqueries
* Date filtering in subqueries

### Sample Query Pattern (Self-Join)

**sql**

```
SELECT e.FirstName + ' ' + e.LastName AS Employee,
       m.FirstName + ' ' + m.LastName AS Manager
FROM Employees e
LEFT JOIN Employees m ON e.ReportsTo = m.EmployeeID
```

---

## 🔴 Level 4: Complex Logic & Advanced Joins

 **Focus** : Multiple joins, HAVING clauses, and correlated subqueries

### Queries Included

| # | Query Objective                         | Complexity Level                  |
| - | --------------------------------------- | --------------------------------- |
| 1 | Employees and their covered regions     | Multi-table join (4 tables)       |
| 2 | Customers and Suppliers in same city    | Self-join across different tables |
| 3 | Customers purchasing from >3 categories | HAVING with COUNT(DISTINCT)       |
| 4 | Revenue from discontinued products      | Complex filtering with joins      |
| 5 | Most expensive product per category     | Correlated subquery               |

### Key Concepts

* Multi-table join chains
* `HAVING` with complex conditions
* Correlated subqueries
* `DISTINCT` in aggregations
* Cross-table business logic

### Sample Query Pattern (Correlated Subquery)

**sql**

```
SELECT c.CategoryName, p.ProductName, p.UnitPrice
FROM Products p
JOIN Categories c ON p.CategoryID = c.CategoryID
WHERE p.UnitPrice = (
    SELECT MAX(UnitPrice)
    FROM Products
    WHERE CategoryID = c.CategoryID
)
```

## 📊 Skill Progression Map

**text**

```
Level 1: Foundation
    ├── Basic joins
    ├── Foreign key understanding
    └── Simple SELECT
          ↓
Level 2: Aggregation
    ├── GROUP BY mastery
    ├── Multiple join patterns
    └── Calculated fields
          ↓
Level 3: Advanced Filtering
    ├── Subquery techniques
    ├── Self-join patterns
    └── Existence checks
          ↓
Level 4: Complex Analytics
    ├── Correlated subqueries
    ├── Multi-table strategies
    └── Business intelligence
```

## 🎓 Learning Outcomes

By completing all 20 queries, learners will be able to:

### Technical Skills

* Write efficient JOIN operations across multiple tables
* Perform complex aggregations with proper grouping
* Implement subqueries for advanced filtering
* Use self-joins for hierarchical data analysis
* Apply HAVING clauses for group filtering
* Create correlated subqueries for row-by-row comparisons

### Analytical Skills

* Translate business questions into SQL queries
* Understand database normalization in practice
* Identify optimal join paths through schemas
* Recognize when to use different query techniques
* Debug and optimize complex queries

## 💼 Real-World Applications

These query patterns mirror actual business intelligence tasks:

| Level | Business Scenario                           |
| ----- | ------------------------------------------- |
| 1     | Basic reporting and data extraction         |
| 2     | Sales analysis and performance metrics      |
| 3     | Customer behavior and exception reporting   |
| 4     | Strategic analytics and market intelligence |

## 🔍 Database Relationships Summary

**text**

```
Customers ──── Orders ──── Order Details ──── Products ──── Suppliers
                 │                              │
                 │                              │
            Employees                       Categories
                 │
                 │
            Territories ──── Region
```

## 📝 Prerequisites

* Understanding of basic SQL SELECT statements
* Familiarity with WHERE clause filtering
* Basic knowledge of aggregate functions
* Access to Northwind database instance

## 🚀 Getting Started

1. Install Northwind database on your SQL Server instance
2. Start with Level 1 queries and progress sequentially
3. Test each query and verify results
4. Experiment with variations to deepen understanding
5. Use the provided patterns as templates for custom queries
