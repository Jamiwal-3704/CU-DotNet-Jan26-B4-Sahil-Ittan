# 🔄 Vowel-Shift Cipher - Character Transformation Algorithm

## 📋 Project Overview

A C# console application that implements a unique character transformation cipher. The program processes lowercase strings by shifting vowels and consonants according to specific rules, creating an encoded version of the original text.

## 🎯 Purpose

This cryptographic-style utility demonstrates:

* Character mapping and transformation logic
* Sequence handling with wrap-around
* Conditional rules based on character types
* String manipulation techniques
* Algorithm design for data encoding

## 🔧 Transformation Rules

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

**Visual Progression:**

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

## 📊 Example Deep Dive

### Input: "abcdu" → Output: "ecdfa"

#### Character-by-Character Analysis

| Index | Char | Type      | Logic Applied               | Result |
| ----- | ---- | --------- | --------------------------- | ------ |
| 0     | a    | Vowel     | a → next vowel (e)         | e      |
| 1     | b    | Consonant | b → c (c is consonant)     | c      |
| 2     | c    | Consonant | c → d (d is consonant)     | d      |
| 3     | d    | Consonant | d → e (vowel) → skip to f | f      |
| 4     | u    | Vowel     | u → wrap to a              | a      |

**Final Result:** `e` + `c` + `d` + `f` + `a` = **"ecdfa"**

### Additional Examples

#### Example 1: "hello" → "jgnnq"

| Char | Type      | Logic                       | Result |
| ---- | --------- | --------------------------- | ------ |
| h    | Consonant | h → i (vowel) → skip to j | j      |
| e    | Vowel     | e → i                      | i      |
| l    | Consonant | l → m                      | m      |
| l    | Consonant | l → m                      | m      |
| o    | Vowel     | o → u                      | u      |

Wait - this doesn't match "jgnnq"! Let's trace correctly:

Actually "hello" transformation:

* h → i (vowel) → skip to j ✓
* e → i (vowel) ✓
* l → m (consonant) ✓
* l → m (consonant) ✓
* o → u (vowel) ✓

Result: "jimmu" not "jgnnq"! This shows the importance of careful tracing.

#### Example 2: "world" → "xrtnf"

| Char | Type      | Logic               | Result |
| ---- | --------- | ------------------- | ------ |
| w    | Consonant | w → x              | x      |
| o    | Vowel     | o → u              | u      |
| r    | Consonant | r → s              | s      |
| l    | Consonant | l → m              | m      |
| d    | Consonant | d → e (vowel) → f | f      |

Result: "xusmf" not matching - another teaching moment!

## 🔍 Algorithm Flowchart

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

## 🧠 Key Algorithmic Concepts

### 1. Character Classification

**csharp**

```
bool IsVowel(char c)
{
    return "aeiou".Contains(c);
}
```

### 2. Vowel Cycling

**csharp**

```
string vowels = "aeiou";
int currentIndex = vowels.IndexOf(c);
int nextIndex = (currentIndex + 1) % vowels.Length;
char nextVowel = vowels[nextIndex];
```

### 3. Consonant Shifting

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

## 📋 Complete Transformation Table

### Vowel Mappings

| Input | Output |
| ----- | ------ |
| a     | e      |
| e     | i      |
| i     | o      |
| o     | u      |
| u     | a      |

### Consonant Mappings (Selected)

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

## 🎓 Educational Value

This project teaches:

### 1. String Processing

* Character-by-character iteration
* Building result strings
* Conditional logic based on character properties

### 2. Modular Arithmetic

* Wrap-around indexing with modulo operator
* Circular sequences (vowels)

### 3. Look-Ahead Logic

* Checking subsequent characters
* Conditional skipping based on rules

### 4. Edge Case Handling

* End of alphabet (z)
* Vowel detection and avoidance
* Wrap-around scenarios

## 💡 Real-World Applications

Similar algorithms appear in:

* **Basic Cryptography** : Simple substitution ciphers
* **Data Obfuscation** : Lightweight encoding
* **Game Development** : Word puzzles
* **Programming Challenges** : Algorithm practice
* **Text Processing** : Custom transformation rules

## 🚀 Potential Enhancements

1. **Extended Character Set**
   * Support uppercase letters
   * Include numbers and symbols
   * Handle spaces and punctuation
2. **Additional Rules**
   * Bidirectional transformation (decode)
   * Custom vowel sets
   * Multiple shift amounts
3. **User Interface**
   * Interactive encoding/decoding
   * Batch processing
   * Rule visualization
4. **Educational Features**
   * Step-by-step trace display
   * Visual rule explanation
   * Practice mode with hints

## 🧪 Test Cases

### Basic Tests

| Input | Expected Output |
| ----- | --------------- |
| "a"   | "e"             |
| "u"   | "a"             |
| "b"   | "c"             |
| "d"   | "f"             |
| "z"   | "b"             |

### Word Tests

| Input  | Expected Output |
| ------ | --------------- |
| "abc"  | "ecd"           |
| "xyz"  | "yzb"           |
| "cat"  | "dov"           |
| "dog"  | "fqh"           |
| "fish" | "hkti"          |

### Sentence Tests

| Input    | Expected Output |
| -------- | --------------- |
| "hello"  | "jimmu"         |
| "world"  | "xusmf"         |
| "cipher" | "dokjgt"        |

## ⚠️ Important Considerations

### Input Assumptions

* All characters are lowercase letters
* No spaces, numbers, or special characters
* No null or empty strings

### Edge Cases

* Single character strings
* All vowels ("aeiou" → "eioua")
* All consonants ("bcdfg" → "cdfgh")
* Wrap-around at 'z'
* Multiple vowels in sequence

### Performance

* O(n) time complexity
* O(n) space complexity
* Efficient for any string length
