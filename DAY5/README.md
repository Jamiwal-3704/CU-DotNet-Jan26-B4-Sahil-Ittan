Here’s a **clean, professional README.md** you can directly use in your repository.
It’s written in a way that recruiters, evaluators, and teammates will clearly understand the purpose and structure.

---

# Shared Greeting Library – .NET Framework Case Study

## 📌 Overview

This repository demonstrates how to create and use a **shared class library** in **.NET Framework** to avoid code duplication across multiple console applications.

The solution contains:

* A **Class Library** that generates greeting messages
* A **Console Application** that consumes the library

This case study focuses on **code reuse**, **project references**, and **clean separation of responsibilities**.

---

## 🏗 Solution Structure

```
GreetingSolution
│
├── GreetingLibrary   (Class Library – .NET Framework)
└── GreetingApp       (Console App – .NET Framework)
```

---

## 📦 Project 1: GreetingLibrary (Class Library)

### Purpose

Provides reusable greeting logic that can be shared across multiple applications.

### Class: `GreetingHelper`

```csharp
public static class GreetingHelper
{
    public static string GetGreeting(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Hello, Guest!";
        }

        return $"Hello, {name}!";
    }
}
```

### Method Rules

| Condition               | Output           |
| ----------------------- | ---------------- |
| Name is `null` or empty | `Hello, Guest!`  |
| Name is provided        | `Hello, <name>!` |

---

## 🖥 Project 2: GreetingApp (Console Application)

### Purpose

Consumes the shared library and displays greetings based on user input.

### Steps Performed

1. Takes user input from the console
2. Calls `GreetingHelper.GetGreeting()`
3. Displays the returned greeting

### Sample Code (Main Method)

```csharp
using System;
using GreetingLibrary;

namespace GreetingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            string greeting = GreetingHelper.GetGreeting(name);
            Console.WriteLine(greeting);
        }
    }
}
```

---

## 🔗 Project Reference

The console application references the class library using:

```
Right Click GreetingApp
→ Add
→ Project Reference
→ Select GreetingLibrary
```

---

## ▶ Expected Console Interaction

### Case 1: Name Provided

**Input**

```
Enter your name:
Sharad
```

**Output**

```
Hello, Sharad!
```

---

### Case 2: Name Not Provided

**Input**

```
Enter your name:
```

**Output**

```
Hello, Guest!
```

---

## 🎯 What This Project Demonstrates

| Concept                | Covered |
| ---------------------- | ------- |
| Class Library creation | ✔       |
| Project references     | ✔       |
| Code reusability       | ✔       |
| Static methods         | ✔       |
| Separation of concerns | ✔       |
| Clean architecture     | ✔       |

---

## 🚀 Key Takeaway

This project shows how to:

* Build reusable logic using **Class Libraries**
* Reference and consume shared code in **Console Applications**
* Keep applications modular and maintainable

---

## 🛠 Technologies Used

* C#
* .NET Framework
* Visual Studio

---

Feel free to clone, modify, and extend this solution for larger applications or enterprise-level architectures.

---

If you want, I can also:

* Add **screenshots section**
* Make it **resume/placement-ready**
* Convert this into a **step-by-step tutorial README**
* Add **UML / architecture explanation**

Just tell me 👍
