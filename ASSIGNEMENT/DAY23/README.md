Below is a **GitHub README.md content** written **only as explanation** of the problem — **no code, no solution** — with **clear descriptions, structure, and conceptual diagrams** suitable for a Day 23 assignment.

---

# 📅 Day 23 – Exception Handling in .NET

**(Built-in & Custom Exceptions)**

---

## 🎯 Objective

This exercise focuses on building a  **strong conceptual understanding of exception handling in .NET** .
By completing this task, students will learn how runtime errors occur, how they are handled gracefully, and how custom business rules can be enforced using user-defined exceptions.

The goal is  **not just to catch errors** , but to:

* Prevent application crashes
* Provide meaningful feedback to users
* Maintain clean and predictable program flow

---

## 🧠 What You Will Learn

* Common **built-in exceptions** in .NET
* The structure and purpose of `try`, `catch`, and `finally`
* How to design and use **custom exceptions**
* How **exception propagation** works
* Understanding and using **InnerException**
* Inspecting exception details for debugging and logging

---

## 🧩 Problem Overview

You are developing a  **Student Enrollment System** .
This system accepts user input and performs runtime operations that are **prone to failure** if invalid data is entered.

Instead of letting the application crash, the system must:

* Detect errors
* Handle them safely
* Inform the user clearly
* Continue execution wherever possible

---

## 📌 Part 1 – Built-in Exception Handling

### 🔹 Purpose

This part demonstrates how .NET provides **predefined exception classes** to handle common runtime problems.

Each scenario represents a  **real-world mistake a user might make** .

---

### 🔹 Scenarios Covered

1. **Division Operation**
   * User enters two numbers
   * Division is attempted
   * Error occurs if divisor is `0`
2. **Input Conversion**
   * User enters text instead of a number
   * Conversion to integer fails
3. **Array Access**
   * User provides an index
   * Index may exceed array bounds

---

### 🔹 Key Learning Points

* Each scenario must be handled **independently**
* Each operation has its **own try–catch block**
* Specific exception types must be handled explicitly
* The program must **never terminate abruptly**

---

### 🔹 `finally` Block Requirement

Regardless of success or failure:

* The `finally` block must execute
* It confirms that the operation lifecycle has ended

**Message printed:**

```
Operation Completed
```

---

### 🔄 Exception Flow Concept

![Image](https://www.scaler.com/topics/images/flowchart-illustrates-flow-of-try-catch-statement.webp)

![Image](https://www.researchgate.net/publication/326881132/figure/fig2/AS%3A659560876498944%401534262975484/Exception-handling-software-flow-chart.png)

![Image](https://textbooks.cs.ksu.edu/cc310/images/1/1.3.x.11.exception3.png)

**Conceptual Flow:**

```
User Input
   ↓
Try Block
   ↓
Error Occurs?
 ┌───────────────┐
 │ Yes           │ No
 ↓               ↓
Catch Block   Normal Execution
   ↓               ↓
Finally Block (Always Executes)
```

---

## 📌 Part 2 – Custom Exceptions

### 🔹 Why Custom Exceptions?

Built-in exceptions handle  **technical errors** , but **business rules** require custom validation.

In a Student Enrollment System:

* Not all invalid data causes system errors
* Some data violates **domain rules**

Custom exceptions help express  **meaningful business logic errors** .

---

### 🔹 Custom Exceptions Required

#### 1️⃣ InvalidStudentAgeException

* Age must be between **18 and 60**
* User is repeatedly prompted until valid age is provided
* Ensures compliance with enrollment rules

#### 2️⃣ InvalidStudentNameException

* Ensures student name follows defined validity rules
* Prevents invalid or malformed names

---

### 🔹 Key Learning Points

* Custom exceptions inherit from base exception types
* They represent **domain-specific problems**
* They make error handling **more readable and meaningful**

---

## 📌 Part 3 – InnerException Demonstration

### 🔹 What is InnerException?

An `InnerException` allows one exception to  **wrap another exception** .

This is useful when:

* A low-level error occurs
* A higher-level exception provides context

---

### 🔹 Requirement

* A custom exception must be wrapped inside another exception
* Both messages must be printed:
  * Outer Exception Message
  * Inner Exception Message

---

### 🔄 Exception Wrapping Concept

![Image](https://miro.medium.com/v2/resize%3Afit%3A1200/1%2AMEiXXYzFLtJRlGdRK9gSMA.png)

![Image](https://ik.imagekit.io/upgrad1/abroad-images/imageCompo/images/ExceptionHierarchy_in_JavaVOEECE.png?pr-true=)

![Image](https://i.sstatic.net/53LMK.png)

**Conceptual View:**

```
Low-Level Exception
        ↓
Wrapped as InnerException
        ↓
Higher-Level Custom Exception
        ↓
Handled by Caller
```

---

## 📌 Part 4 – Logging Exception Details

### 🔹 Why Log Exceptions?

When applications grow:

* Errors may not be reproducible easily
* Logs help diagnose issues without rerunning the program

---

### 🔹 Exception Properties to Inspect

The following exception details must be printed:

* **Message**
  → Human-readable description of the error
* **StackTrace**
  → Shows where the error occurred
* **InnerException**
  → Displays the original underlying exception (if any)

---

### 🔍 Debugging Insight

![Image](https://miro.medium.com/0%2AzikaSxcRoLEA7Qzp.png)

![Image](https://i.sstatic.net/IoeLG.png)

![Image](https://www.researchgate.net/publication/326881132/figure/fig2/AS%3A659560876498944%401534262975484/Exception-handling-software-flow-chart.png)

These properties are essential for:

* Debugging production issues
* Understanding execution flow
* Improving application stability

---

## ✅ Expected Outcome

By completing this assignment, students will be able to:

* Handle runtime errors safely
* Apply structured exception handling
* Create and use custom exceptions
* Understand exception propagation
* Analyze detailed exception information

This forms a  **core foundation for robust .NET application development** .

---

📌 **Note:**
This README intentionally focuses on  **conceptual understanding only** .
No implementation or code has been included, as the goal is clarity of design and behavior.

---

If you want, next I can:

* Make this **more beginner-friendly**
* Convert it into **college-assignment format**
* Add **real-world analogies**
* Add **UML-style exception diagrams**

Just tell me 👍
