# 🔐 ASCII String Transformer - Data Encoding Utility

## 📋 Project Overview

A C# console application that demonstrates string manipulation based on ASCII values. The program transforms input words by removing characters with even ASCII codes and reversing the remaining characters, with additional formatting rules - perfect for teaching data encoding concepts.

## 🎯 Purpose

This educational tool helps students understand:

* ASCII value calculations and properties
* String manipulation techniques
* Character validation and filtering
* Index-based transformations
* Data encoding concepts

## 🔧 How It Works

### Core Transformation Logic

**text**

```
Input String → Validate → Lowercase → Filter Even ASCII → Reverse → Position-Based Casing → Output
```

### Step-by-Step Process

1. **Validation Checks**
   * Input not null
   * Minimum 6 characters
   * No spaces, digits, or special characters (letters only)
2. **ASCII Filtering**
   * Convert to lowercase
   * Remove characters with even ASCII values
   * ASCII values: a=97 (odd), b=98 (even), c=99 (odd), etc.
3. **Reversal & Formatting**
   * Reverse the remaining characters
   * Convert even-indexed positions (0-based) to uppercase
   * Odd-indexed positions remain lowercase

## 📊 Examples Explained

### Sample 1: "Aeroplane" → "EaOeA"

| Step            | Operation                       | Result          |
| --------------- | ------------------------------- | --------------- |
| 1               | Input                           | Aeroplane       |
| 2               | Lowercase                       | aeroplane       |
| 3               | Filter even ASCII               | a e o a e       |
| 4               | Reverse                         | e a o e a       |
| 5               | Even index → Uppercase (0,2,4) | E a O e A       |
| **Final** |                                 | **EaOeA** |

**ASCII Analysis:**

* a (97 - odd) ✓ keep
* e (101 - odd) ✓ keep
* r (114 - even) ✗ remove
* o (111 - odd) ✓ keep
* p (112 - even) ✗ remove
* l (108 - even) ✗ remove
* a (97 - odd) ✓ keep
* n (110 - even) ✗ remove
* e (101 - odd) ✓ keep

### Sample 2: "Cowages" → "SeGaWoC"

| Step            | Operation               | Result            |
| --------------- | ----------------------- | ----------------- |
| 1               | Input                   | Cowages           |
| 2               | Lowercase               | cowages           |
| 3               | Filter even ASCII       | c o a e s         |
| 4               | Reverse                 | s e a o c         |
| 5               | Even index → Uppercase | S e A o C         |
| **Final** |                         | **SeGaWoC** |

**ASCII Analysis:**
All characters have odd ASCII values:

* c (99), o (111), w (119), a (97), g (103), e (101), s (115)

### Sample 3: "Magic" → Invalid Input

* Length = 5 (< 6 characters)

### Sample 4: "Kinder World" → Invalid Input

* Contains space character

### Sample 5: "B@rbie" → Invalid Input

* Contains special character '@'

## 🔍 ASCII Reference

| Character | ASCII Value | Parity  | Action                     |
| --------- | ----------- | ------- | -------------------------- |
| a-z       | 97-122      | Mixed   | Keep if odd                |
| A-Z       | 65-90       | Mixed   | Convert to lowercase first |
| Space     | 32          | Even    | Reject                     |
| 0-9       | 48-57       | Mixed   | Reject                     |
| @,!,#,$   | Various     | Various | Reject                     |

### Common Letters (Lowercase)

| Letter | ASCII | Parity  |
| ------ | ----- | ------- |
| a      | 97    | Odd ✓  |
| b      | 98    | Even ✗ |
| c      | 99    | Odd ✓  |
| d      | 100   | Even ✗ |
| e      | 101   | Odd ✓  |
| f      | 102   | Even ✗ |
| g      | 103   | Odd ✓  |
| h      | 104   | Even ✗ |
| i      | 105   | Odd ✓  |
| j      | 106   | Even ✗ |

## 📝 Algorithm Flowchart

**text**

```
┌─────────────────────┐
│        START        │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Prompt user for    │
│  input string       │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Call CleanseAnd-   │
│  Invert(input)      │
└──────────┬──────────┘
           ↓
    ┌──────┴──────┐
    │ Is null?     │
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ Length >= 6? │
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ Contains    │
    │ only letters?│
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ Any invalid? │
    │  ┌───┴───┐  │
    │ Yes      No  │
    └──┬───────┬┘  │
       ↓       │   │
┌──────────────┘   │
↓                  ↓
┌─────────────────────┐
│ Return empty string │
└─────────────────────┘
                      ↓
              ┌─────────────────────┐
              │ Convert to lowercase│
              └──────────┬──────────┘
                         ↓
              ┌─────────────────────┐
              │ Filter: Keep only   │
              │ odd ASCII characters│
              └──────────┬──────────┘
                         ↓
              ┌─────────────────────┐
              │ Reverse the string  │
              └──────────┬──────────┘
                         ↓
              ┌─────────────────────┐
              │ For i = 0 to length │
              │ if i is even:       │
              │   char.ToUpper      │
              └──────────┬──────────┘
                         ↓
              ┌─────────────────────┐
              │ Return transformed  │
              │     string          │
              └──────────┬──────────┘
                         ↓
┌─────────────────────┐
│ Check return value  │
└──────────┬──────────┘
           ↓
    ┌──────┴──────┐
    │ Is empty?    │
    │  ┌───┴───┐  │
    │ Yes      No  │
    └──┬───────┬┘  │
       ↓       │   │
┌──────────────┘   │
↓                  ↓
┌─────────────────────┐
│ Display:           │
│ "Invalid Input"    │
└─────────────────────┘
                    ↓
              ┌─────────────────────┐
              │ Display:            │
              │ "The generated key  │
              │ is - <result>"      │
              └──────────┬──────────┘
                         ↓
              ┌─────────────────────┐
              │         END         │
              └─────────────────────┘
```

## 🎓 Educational Concepts

### 1. ASCII Value Analysis

* Understanding character-to-number mapping
* Parity (even/odd) of character codes
* Relationship between uppercase and lowercase (difference of 32)

### 2. String Validation

* Null checking
* Length validation
* Character classification (char.IsLetter)

### 3. String Manipulation

* Conversion to lowercase
* Filtering based on conditions
* String reversal
* Character case modification

### 4. Index-Based Operations

* 0-based indexing
* Even/odd position identification
* Conditional transformation

## 💡 Real-World Applications

This transformation technique demonstrates concepts used in:

* **Basic Data Encoding** : Simple obfuscation techniques
* **Hash Functions** : Foundation of more complex hashing
* **Data Validation** : Input sanitization practices
* **Character Encoding** : Understanding data representation
* **Cryptography Basics** : Building blocks for encryption

## 🚀 Potential Enhancements

1. **Additional Validation Rules**
   * Allow specific special characters
   * Handle multiple words
   * Support for numbers in specific contexts
2. **Extended Transformations**
   * Odd ASCII removal instead of even
   * Different index-based rules
   * Multiple transformation rounds
3. **Educational Features**
   * Show intermediate steps
   * Display ASCII values
   * Highlight removed characters
4. **Security Applications**
   * Simple password generation
   * Basic key derivation
   * Data masking techniques

## 📋 Validation Rules Summary

| Rule             | Condition       | Example        | Result  |
| ---------------- | --------------- | -------------- | ------- |
| Not null         | input != null   | null           | Invalid |
| Min length       | Length >= 6     | "Magic"        | Invalid |
| No spaces        | !Contains(' ')  | "Kinder World" | Invalid |
| No digits        | !Contains digit | "Test123"      | Invalid |
| No special chars | Only letters    | "B@rbie"       | Invalid |
| All valid        | Pass all checks | "Aeroplane"    | Process |

## 🧪 Testing Scenarios

### Valid Inputs

* "aeroplane" → Transformation applies
* "cowages" → All odd ASCII
* "abcdef" → Mix of even/odd
* "zyxwvuts" → Various patterns

### Invalid Inputs

* Short: "cat" (length 3)
* With space: "hello world"
* With numbers: "test123"
* With symbols: "pa$$word"
* Mixed case with invalid: "Hello@123"
