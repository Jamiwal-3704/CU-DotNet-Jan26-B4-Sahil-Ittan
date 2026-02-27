Below is a **ready-to-use README.md** for your repository, written clearly for **assessment, placements, and reviewers**.
It matches **Day7-01 Assessment** expectations and keeps everything professional.

---

# Smart Access Control Log Processor

**Day7-01 Assessment – Console Application (C# / .NET)**

---

## 📌 Overview

This project simulates a **secure facility access log processor** where each access attempt is recorded in a **compact single-line format** to minimize storage usage.

The application:

* Reads **one line of input**
* Parses multiple values
* Performs **strict validation**
* Applies **real-world business rules**
* Produces **clean, formatted output**

This exercise focuses heavily on **data parsing, validation, and control flow logic**.

---

## 🧾 Input Format

Each access log entry is provided in the following format:

```
<GateCode>|<UserInitial>|<AccessLevel>|<IsActive>|<Attempts>
```

### Example Input

```
A3|S|5|true|120
```

---

## 📊 Field Description & Data Types

| Field       | Description                      | Data Type |
| ----------- | -------------------------------- | --------- |
| GateCode    | Gate identifier (letter + digit) | `string`  |
| UserInitial | First letter of user name        | `char`    |
| AccessLevel | Security level (0–9)             | `byte`    |
| IsActive    | User account active status       | `bool`    |
| Attempts    | Number of access attempts today  | `byte`    |

---

## ⚙ Functional Requirements

### 1️⃣ Input Handling (Mandatory)

* Read **all input using a single `Console.ReadLine()`**
* Split input using `|`
* No separate prompts for individual fields

---

### 2️⃣ Validation Rules (Critical)

If **any validation fails**, the program must print:

```
INVALID ACCESS LOG
```

and exit immediately.

#### Validation Conditions

##### GateCode

* Must be **exactly 2 characters**
* First character → **letter**
* Second character → **digit**

##### UserInitial

* Must be **one uppercase letter**
* Must use **char APIs** (no regex)

##### AccessLevel

* Must be between **1 and 7**
* Stored as `byte`

##### IsActive

* Accepts **true / false** only (case-insensitive)

##### Attempts

* Must be **≤ 200**
* Stored as `byte`

---

### 3️⃣ Business Logic

After successful validation:

| Condition           | Result                              |
| ------------------- | ----------------------------------- |
| `IsActive == false` | `ACCESS DENIED – INACTIVE USER`     |
| `Attempts > 100`    | `ACCESS DENIED – TOO MANY ATTEMPTS` |
| `AccessLevel >= 5`  | `ACCESS GRANTED – HIGH SECURITY`    |
| Otherwise           | `ACCESS GRANTED – STANDARD`         |

---

### 4️⃣ Output Formatting (Mandatory)

On successful validation, output must be **exactly formatted** as:

```
Gate      : A3
User      : S
Level     : 5
Attempts  : 120
Status    : ACCESS DENIED – TOO MANY ATTEMPTS
```

✔ Alignment must be clean
✔ Use `PadRight`, alignment, or string interpolation

---

## 🧪 Sample Test Cases

### ✅ Input

```
B7|R|6|true|45
```

**Output**

```
ACCESS GRANTED – HIGH SECURITY
```

---

### ❌ Input

```
C1|m|3|true|10
```

**Output**

```
INVALID ACCESS LOG
```

*Reason: lowercase UserInitial*

---

### ❌ Input

```
A9|K|2|false|20
```

**Output**

```
ACCESS DENIED – INACTIVE USER
```

---

## 🚫 Constraints (Assessment Rules)

| Rule                          | Status        |
| ----------------------------- | ------------- |
| Regex usage                   | ❌ Not allowed |
| Multiple ReadLine calls       | ❌ Not allowed |
| Exception swallowing          | ❌ Not allowed |
| Use of `byte`, `char`, `bool` | ✔ Mandatory   |
| String formatting & alignment | ✔ Mandatory   |

---

## 🎯 What This Exercise Evaluates

| Skill                     | Tested |
| ------------------------- | ------ |
| String splitting          | ✔      |
| Multi-value parsing       | ✔      |
| Byte range validation     | ✔      |
| Character validation      | ✔      |
| Boolean parsing           | ✔      |
| Output formatting         | ✔      |
| Real-world decision logic | ✔      |

---

## 🛠 Technologies Used

* C#
* .NET Console Application
* Visual Studio

---

## 📌 Key Takeaway

This assessment tests **attention to detail**, **data validation discipline**, and **real-world control flow logic**, which are critical for backend and system-level development.

---

If you want, I can also:

* Add **flow diagrams**
* Write **step-by-step solution logic**
* Create a **rubric-aligned explanation**
* Add **edge case reasoning**

Just say the word 👍
