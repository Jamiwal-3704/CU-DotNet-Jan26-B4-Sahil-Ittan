Below is a **clean, assessment-ready README.md** you can directly add to your repository for **Case Study: Simple User Login Message Processor**.
It is written for **beginners**, **evaluators**, and **placement reviewers**.

---

# Simple User Login Message Processor

**Console Application – C# / .NET**

---

## 📌 Overview

This project demonstrates **basic string processing and decision-making** in a console application.

Users manually enter login messages, which may include:

* Extra spaces
* Mixed casing
* Custom wording

The application cleans the input, analyzes its meaning, and displays a **well-formatted login status message**.

---

## 🧾 Input Specification

The program reads **one single line of input** in the following format:

```
<UserName>|<LoginMessage>
```

### Example

```
Sharad|  LOGIN Successful  
```

⚠️ Only **one `Console.ReadLine()`** is allowed.

---

## ⚙ Functional Requirements

---

### 1️⃣ String Cleaning (Entry Level)

The `LoginMessage` must be cleaned using:

* `Trim()` → remove leading & trailing spaces
* `ToLower()` → normalize casing

#### Example

```
"  LOGIN Successful  "
→ "login successful"
```

---

### 2️⃣ String Search

Check whether the cleaned message contains:

```
"successful"
```

* Use `Contains()` **or** `IndexOf()`
* Case-insensitive (handled by `ToLower()`)

---

### 3️⃣ String Comparison

The system defines a **standard success message**:

```
"login successful"
```

Compare the cleaned message with the standard message using:

```
Equals()
```

Determine whether the message is:

* Standard
* Custom

---

### 4️⃣ Business Rules

| Condition                               | Status                           |
| --------------------------------------- | -------------------------------- |
| Keyword **not found**                   | `LOGIN FAILED`                   |
| Keyword found & message equals standard | `LOGIN SUCCESS`                  |
| Keyword found but message differs       | `LOGIN SUCCESS (CUSTOM MESSAGE)` |

---

### 5️⃣ Formatted Output (Mandatory)

The output must be displayed **exactly** in this format:

```
User     : Sharad
Message  : login successful
Status   : LOGIN SUCCESS
```

✔ Labels must be aligned
✔ Clean spacing
✔ No extra blank lines

---

## 🚫 Constraints (Beginner Friendly)

| Rule                       | Allowed |
| -------------------------- | ------- |
| One `Console.ReadLine()`   | ✔       |
| `Trim()`                   | ✔       |
| `ToLower()`                | ✔       |
| `Contains()` / `IndexOf()` | ✔       |
| `Equals()`                 | ✔       |
| Regex                      | ❌       |
| Culture APIs               | ❌       |

---

## 🧪 Sample Test Cases

### ✅ Input

```
Amit| Login successful 
```

**Output Status**

```
LOGIN SUCCESS
```

---

### ✅ Input

```
Neha|login successful today
```

**Output Status**

```
LOGIN SUCCESS (CUSTOM MESSAGE)
```

---

### ❌ Input

```
Ravi|invalid password
```

**Output Status**

```
LOGIN FAILED
```

---

## 🎯 What This Exercise Helps Learners Practice

| Concept            | Level        |
| ------------------ | ------------ |
| Trimming strings   | Basic        |
| Case normalization | Basic        |
| String search      | Basic        |
| String comparison  | Basic        |
| Console formatting | Basic        |
| Real-world logic   | Introductory |

---

## 🛠 Technologies Used

* C#
* .NET Console Application
* Visual Studio

---

## 📌 Key Takeaway

This project builds a strong foundation in:

* **String manipulation**
* **Clean input handling**
* **Clear output formatting**
* **Business-driven decision logic**

These are essential skills for **backend**, **automation**, and **system-level development**.

---

If you want next:

* ✅ **Full solution code**
* ✅ **Step-by-step explanation**
* ✅ **Common beginner mistakes**
* ✅ **Interview questions from this problem**

Just tell me 👍
