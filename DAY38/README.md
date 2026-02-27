# 🔐 String Transformation Utilities - ASCII & Vowel-Shift Cipher

## 📋 Project Overview

A comprehensive C# console application featuring two distinct string transformation utilities designed for educational purposes. This project helps students understand ASCII values, character manipulation, and cryptographic-style transformations through hands-on coding exercises.

## 🎯 Educational Purpose

This dual-utility tool demonstrates:

* **ASCII value analysis** and character properties
* **String validation** and manipulation techniques
* **Character mapping** and transformation logic
* **Algorithm design** for data encoding
* **Conditional rules** based on character types

---

# 🧹 Part 1: ASCII Cleanse and Invert

## Overview

A utility that removes characters with even ASCII codes from a word and reverses the remaining ones, demonstrating basic data transformation for security or encoding purposes.

## Transformation Rules

### Validation Requirements

| Rule             | Condition       | Example        | Result  |
| ---------------- | --------------- | -------------- | ------- |
| Not null         | input != null   | null           | Invalid |
| Min length       | Length ≥ 6     | "Magic" (5)    | Invalid |
| No spaces        | !Contains(' ')  | "Kinder World" | Invalid |
| No digits        | !Contains digit | "Test123"      | Invalid |
| No special chars | Only letters    | "B@rbie"       | Invalid |

### Password Generation Logic

1. **Convert** input to lowercase
2. **Remove** characters with even ASCII values
3. **Reverse** the remaining characters
4. **Transform** even-indexed positions (0-based) to uppercase
5. **Return** the generated key

## ASCII Reference Guide

### Letter Analysis (Lowercase)

| Letter | ASCII | Parity  | Action |
| ------ | ----- | ------- | ------ |
| a      | 97    | Odd ✓  | Keep   |
| b      | 98    | Even ✗ | Remove |
| c      | 99    | Odd ✓  | Keep   |
| d      | 100   | Even ✗ | Remove |
| e      | 101   | Odd ✓  | Keep   |
| f      | 102   | Even ✗ | Remove |
| g      | 103   | Odd ✓  | Keep   |
| h      | 104   | Even ✗ | Remove |
| i      | 105   | Odd ✓  | Keep   |
| j      | 106   | Even ✗ | Remove |

### Non-Letter Characters

| Character | ASCII   | Status |
| --------- | ------- | ------ |
| Space     | 32      | Reject |
| 0-9       | 48-57   | Reject |
| @,!,#,$   | Various | Reject |

## Detailed Examples

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

**ASCII Analysis:** All characters have odd ASCII values:

* c (99), o (111), w (119), a (97), g (103), e (101), s (115)

### Invalid Input Examples

| Input          | Reason                         |
| -------------- | ------------------------------ |
| "Magic"        | Length = 5 (< 6)               |
| "Kinder World" | Contains space                 |
| "B@rbie"       | Contains '@' special character |

---

# 🔄 Part 2: Vowel-Shift Cipher

## Overview

A cryptographic-style utility that transforms lowercase strings by shifting vowels and consonants according to specific rules, creating encoded versions of original text.

## Transformation Rules

### Two Parallel Encoding Systems

#### 1. Vowel Transformation (Circular Shift)

Vowels follow a circular progression:

**text**

```
a → e → i → o → u → a
```

| Original | Becomes |
| -------- | ------- |
| a        | e       |
| e        | i       |
| i        | o       |
| o        | u       |
| u        | a       |

#### 2. Consonant Transformation (Alphabet Shift with Vowel Skip)

Consonants shift to the next consonant in the alphabet:

* Move to the next alphabetical character
* If that character is a vowel, skip it and take the next
* Continue until a consonant is found

### Visual Consonant Progression

**text**

```
b → c (c is consonant)
c → d (d is consonant)
d → f (skip e because it's a vowel)
f → g (g is consonant)
g → h (h is consonant)
h → j (skip i because it's a vowel)
j → k (k is consonant)
k → l (l is consonant)
l → m (m is consonant)
m → n (n is consonant)
n → p (skip o because it's a vowel)
p → q (q is consonant)
q → r (r is consonant)
r → s (s is consonant)
s → t (t is consonant)
t → v (skip u because it's a vowel)
v → w (w is consonant)
w → x (x is consonant)
x → y (y is consonant)
y → z (z is consonant)
z → b (wrap around, skip a as vowel)
```

## Detailed Examples

### Example 1: "abcdu" → "ecdfa"

| Index | Char | Type      | Logic Applied               | Result |
| ----- | ---- | --------- | --------------------------- | ------ |
| 0     | a    | Vowel     | a → next vowel (e)         | e      |
| 1     | b    | Consonant | b → c (c is consonant)     | c      |
| 2     | c    | Consonant | c → d (d is consonant)     | d      |
| 3     | d    | Consonant | d → e (vowel) → skip to f | f      |
| 4     | u    | Vowel     | u → wrap to a              | a      |

**Final Result:** "ecdfa"

### Example 2: "hello" → "jimmu"

| Char | Type      | Logic                       | Result |
| ---- | --------- | --------------------------- | ------ |
| h    | Consonant | h → i (vowel) → skip to j | j      |
| e    | Vowel     | e → i                      | i      |
| l    | Consonant | l → m                      | m      |
| l    | Consonant | l → m                      | m      |
| o    | Vowel     | o → u                      | u      |

**Final Result:** "jimmu"

### Example 3: "world" → "xusmf"

| Char | Type      | Logic               | Result |
| ---- | --------- | ------------------- | ------ |
| w    | Consonant | w → x              | x      |
| o    | Vowel     | o → u              | u      |
| r    | Consonant | r → s              | s      |
| l    | Consonant | l → m              | m      |
| d    | Consonant | d → e (vowel) → f | f      |

**Final Result:** "xusmf"

## Complete Transformation Tables

### Vowel Mappings

| Input | Output |
| ----- | ------ |
| a     | e      |
| e     | i      |
| i     | o      |
| o     | u      |
| u     | a      |

### Consonant Mappings

| Input | Skip Logic      | Output |
| ----- | --------------- | ------ |
| b     | b→c (c ok)     | c      |
| c     | c→d (d ok)     | d      |
| d     | d→e (vowel)→f | f      |
| f     | f→g (g ok)     | g      |
| g     | g→h (h ok)     | h      |
| h     | h→i (vowel)→j | j      |
| j     | j→k (k ok)     | k      |
| k     | k→l (l ok)     | l      |
| l     | l→m (m ok)     | m      |
| m     | m→n (n ok)     | n      |
| n     | n→o (vowel)→p | p      |
| p     | p→q (q ok)     | q      |
| q     | q→r (r ok)     | r      |
| r     | r→s (s ok)     | s      |
| s     | s→t (t ok)     | t      |
| t     | t→u (vowel)→v | v      |
| v     | v→w (w ok)     | w      |
| w     | w→x (x ok)     | x      |
| x     | x→y (y ok)     | y      |
| y     | y→z (z ok)     | z      |
| z     | z→a (vowel)→b | b      |

---

# 🔧 Core Algorithm Implementations

## ASCII Cleanse Algorithm Flow

**text**

```
┌─────────────────────┐
│        START        │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Input string       │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Validate input:    │
│  - Not null         │
│  - Length ≥ 6       │
│  - Only letters     │
└──────────┬──────────┘
           ↓
    ┌──────┴──────┐
    │ Valid?       │
    │  ┌───┴───┐  │
    │ Yes      No  │
    └──┬───────┬┘  │
       ↓       └───┘
┌─────────────────────┐
│ Convert to lowercase│
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Filter characters   │
│ with odd ASCII only │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Reverse string      │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Uppercase even      │
│ indices (0-based)   │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Return result       │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Display output or   │
│ "Invalid Input"     │
└─────────────────────┘
```

## Vowel-Shift Algorithm Flow

**text**

```
┌─────────────────────┐
│        START        │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Input lowercase    │
│     string s        │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Initialize empty    │
│  result string      │
└──────────┬──────────┘
           ↓
    ┌──────┴──────┐
    │ For each char│
    │  in string   │
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ Is vowel?    │
    │  ┌───┴───┐  │
    │ Yes      No  │
    └──┬───────┬┘  │
       ↓       ↓   │
    Vowel      Consonant
    Path       Path
       ↓       ↓
┌─────────┐ ┌─────────────────┐
│ Find    │ │ Find next       │
│ next    │ │ alphabetical    │
│ vowel   │ │ character       │
│ in      │ └────────┬────────┘
│ circle  │          ↓
└────┬────┘    ┌─────┴─────┐
     │         │ Is vowel? │
     │         │ ┌──┴──┐   │
     │         │ Yes   No   │
     │         └──┬────┬───┘
     │            ↓    ↓
     │       Skip to  Keep
     │       next     this
     │       char     char
     └───────────┬───┘
                 ↓
        ┌────────┴────────┐
        │ Append char to  │
        │ result string   │
        └────────┬────────┘
                 ↓
        ┌────────┴────────┐
        │ More chars?      │
        │   ┌──┴──┐       │
        │  Yes    No       │
        └──┬───────┬──────┘
           ↓       ↓
           └───────┘
                 ↓
        ┌────────┴────────┐
        │ Return result   │
        └─────────────────┘
```

---

# 🧠 Key Programming Concepts

## 1. Character Classification

**csharp**

```
// For ASCII Cleanse
bool IsLetter(char c) => char.IsLetter(c);

// For Vowel-Shift
bool IsVowel(char c) => "aeiou".Contains(c);
```

## 2. ASCII Value Checking

**csharp**

```
// Check if ASCII value is even
bool IsEvenAscii(char c) => (int)c % 2 == 0;
```

## 3. Vowel Cycling

**csharp**

```
string vowels = "aeiou";
int currentIndex = vowels.IndexOf(c);
int nextIndex = (currentIndex + 1) % vowels.Length;
char nextVowel = vowels[nextIndex];
```

## 4. Consonant Shifting with Skip

**csharp**

```
char nextChar = (char)(c + 1);
if (nextChar > 'z') nextChar = 'a';
while (IsVowel(nextChar))
{
    nextChar = (char)(nextChar + 1);
    if (nextChar > 'z') nextChar = 'a';
}
```

---

# 📊 Test Cases Comparison

## ASCII Cleanse Tests

| Input          | Expected Output | Reason               |
| -------------- | --------------- | -------------------- |
| "Aeroplane"    | "EaOeA"         | Mixed even/odd ASCII |
| "Cowages"      | "SeGaWoC"       | All odd ASCII        |
| "Magic"        | Invalid         | Length < 6           |
| "Kinder World" | Invalid         | Contains space       |
| "B@rbie"       | Invalid         | Special character    |

## Vowel-Shift Tests

| Input   | Expected Output |
| ------- | --------------- |
| "a"     | "e"             |
| "u"     | "a"             |
| "b"     | "c"             |
| "d"     | "f"             |
| "z"     | "b"             |
| "abc"   | "ecd"           |
| "hello" | "jimmu"         |
| "world" | "xusmf"         |
| "aeiou" | "eioua"         |
| "bcdfg" | "cdfgh"         |

---

# 🎓 Educational Value Summary

## ASCII Cleanse Teaches

* **Character-to-Number Mapping** : Understanding ASCII values
* **Parity Concepts** : Even/odd number properties
* **String Validation** : Input sanitization techniques
* **Index-Based Transformation** : Position-dependent rules
* **Data Encoding** : Basic obfuscation concepts

## Vowel-Shift Teaches

* **Modular Arithmetic** : Wrap-around with modulo operator
* **Look-Ahead Logic** : Checking subsequent characters
* **Conditional Branching** : Different paths for different character types
* **Edge Case Handling** : End of alphabet, wrap scenarios
* **Cipher Concepts** : Basic cryptographic thinking

---

# 💡 Real-World Applications

| Concept           | Application Area                        |
| ----------------- | --------------------------------------- |
| ASCII filtering   | Data sanitization, input validation     |
| Character mapping | Basic cryptography, encoding            |
| Circular shifts   | Password generation, hashing algorithms |
| Skip logic        | Pattern matching, parsing algorithms    |
| String reversal   | Data transformation, obfuscation        |

---

# 🚀 Potential Enhancements for Both Projects

## Common Enhancements

* **Bidirectional transformation** (encode/decode)
* **Support for uppercase** letters
* **Extended character sets** (numbers, symbols)
* **Batch processing** capabilities
* **GUI interface** for visualization

## ASCII Cleanse Specific

* Configurable ASCII parity (even/odd selection)
* Minimum length configuration
* Step-by-step transformation display

## Vowel-Shift Specific

* Custom vowel sets
* Multiple shift amounts
* Interactive encoding/decoding

---

# 📝 Usage Instructions

## Running the Programs

1. Compile the C# application
2. Run the executable
3. Follow the prompts to enter input strings
4. View transformed output or error messages

## Input Requirements Summary

### ASCII Cleanse

* Minimum 6 characters
* Letters only (a-z, A-Z)
* No spaces, digits, or special characters

### Vowel-Shift

* Lowercase letters only (assumed)
* Can be any length
* No validation (pure transformation)
