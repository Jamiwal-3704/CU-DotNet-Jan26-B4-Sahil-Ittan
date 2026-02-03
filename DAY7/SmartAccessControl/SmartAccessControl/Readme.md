Below is a **clean, professional, assessment-ready `README.md`** you can directly paste into your repository.
It is written exactly how **trainers, reviewers, and interviewers** expect.

---

# 🔐 Smart Access Control Log Processor

A **.NET Console Application** that parses, validates, and processes compact access control logs used by secure facilities.
The application reads a single-line log entry, validates each field strictly, applies business rules, and outputs a clean, formatted access decision.

---

## 📌 Problem Statement

Secure facilities record access attempts in a **compact single-line format** to reduce storage usage.

Each access attempt is logged as:

```
<GateCode>|<UserInitial>|<AccessLevel>|<IsActive>|<Attempts>
```

### Example

```
A3|S|5|true|120
```

---

## 🛠 Tech Stack

* **Language:** C#
* **Framework:** .NET 6 / .NET 7 / .NET 8
* **Project Type:** Console Application
* **Version Control:** Git

---

## 📂 Project Structure

```
SmartAccessControl
│
├── Program.cs
├── SmartAccessControl.csproj
└── README.md
```

---

## 📥 Input Format

| Field       | Description                           | Type   |
| ----------- | ------------------------------------- | ------ |
| GateCode    | Gate identifier (letter + digit)      | string |
| UserInitial | First letter of user name (uppercase) | char   |
| AccessLevel | Security level (1–7)                  | byte   |
| IsActive    | Account active status                 | bool   |
| Attempts    | Number of access attempts             | byte   |

---

## ✅ Validation Rules

The application enforces **strict validation**:

### GateCode

* Exactly **2 characters**
* First character must be a **letter**
* Second character must be a **digit**

### UserInitial

* Must be a **single uppercase letter**
* Validated using **char APIs** (no regex)

### AccessLevel

* Must be between **1 and 7**
* Stored as **byte**

### IsActive

* Accepts only **true / false** (case-insensitive)

### Attempts

* Must be **≤ 200**
* Stored as **byte**

🚫 If any validation fails, the program outputs:

```
INVALID ACCESS LOG
```

---

## 🧠 Business Logic

After successful validation, the access decision is determined using priority-based rules:

1. **Inactive user**

   ```
   ACCESS DENIED – INACTIVE USER
   ```
2. **Too many attempts (>100)**

   ```
   ACCESS DENIED – TOO MANY ATTEMPTS
   ```
3. **High security access (AccessLevel ≥ 5)**

   ```
   ACCESS GRANTED – HIGH SECURITY
   ```
4. **Standard access**

   ```
   ACCESS GRANTED – STANDARD
   ```

---

## 🖨 Output Format

On success, output is displayed in a clean, aligned format:

```
Gate      : A3
User      : S
Level     : 5
Attempts  : 120
Status    : ACCESS DENIED – TOO MANY ATTEMPTS
```

---

## 🧪 Sample Test Cases

### ✔ Valid Input

**Input**

```
B7|R|6|true|45
```

**Output**

```
ACCESS GRANTED – HIGH SECURITY
```

---

### ❌ Invalid User Initial

**Input**

```
C1|m|3|true|10
```

**Output**

```
INVALID ACCESS LOG
```

---

### ❌ Inactive User

**Input**

```
A9|K|2|false|20
```

**Output**

```
ACCESS DENIED – INACTIVE USER
```

---

## ⚙️ How to Run the Project

### Using Visual Studio

1. Open the solution
2. Press **Ctrl + F5**
3. Enter input in the console

### Using .NET CLI

```bash
dotnet run
```

---

## 🚦 Constraints Followed

* ✔ Single `Console.ReadLine()`
* ✔ No regular expressions
* ✔ No exception swallowing
* ✔ Uses `byte`, `char`, and `bool`
* ✔ Uses safe parsing (`TryParse`)
* ✔ Clean output formatting with alignment

---

## 🎯 Learning Outcomes

This project demonstrates:

* Safe multi-input parsing
* Strict data validation
* Proper use of primitive data types
* Priority-based business logic
* Professional console output formatting
* Real-world system design thinking

---

## 👨‍💻 Author

**Sahil Ittan**
.NET Developer | Full-Stack Enthusiast

---

## 📜 License

This project is for **educational and training purposes**.

---

If you want, I can:

* Add **badges** (build, .NET version)
* Create a **professional GitHub profile README**
* Rewrite this README for **company assignment submission**
* Shorten it for **interview portfolio**

Just tell me 👍
