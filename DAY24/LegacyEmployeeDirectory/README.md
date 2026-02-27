# Legacy Employee Directory

A C# console application demonstrating legacy collection handling using `System.Collections.Hashtable` with boxing/unboxing operations.

## 📋 Overview

This legacy system manages employee records using the older `Hashtable` collection (pre-generics era), demonstrating key concepts like boxing, unboxing, and DictionaryEntry iteration.

## 🏗️ Collection Structure

```
┌─────────────────────────────────────────────────────────────┐
│                   HASHTABLE STRUCTURE                        │
│                    (Legacy Collection)                       │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  ┌─────────────────────────────────────────────────────┐   │
│  │   employeeTable                                      │   │
│  │   ┌─────────┬─────────────┬────────────────────┐   │   │
│  │   │  Key    │   Value     │  Type Information  │   │   │
│  │   │ (object)│  (object)   │                    │   │   │
│  │   ├─────────┼─────────────┼────────────────────┤   │   │
│  │   │   101   │   "Alice"   │  int → string      │   │   │
│  │   ├─────────┼─────────────┼────────────────────┤   │   │
│  │   │   102   │   "Bob"     │  int → string      │   │   │
│  │   ├─────────┼─────────────┼────────────────────┤   │   │
│  │   │   103   │   "Charlie" │  int → string      │   │   │
│  │   ├─────────┼─────────────┼────────────────────┤   │   │
│  │   │   104   │   "Diana"   │  int → string      │   │   │
│  │   └─────────┴─────────────┴────────────────────┘   │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                              │
│  ⚠️ NOTE: All keys and values stored as object type         │
│           Requires casting/unboxing when retrieving         │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

## 🔄 Program Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    EXECUTION SEQUENCE                            │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   START                                                          │
│     │                                                            │
│     ▼                                                            │
│   STEP 1: Create Hashtable & Add Initial Data                   │
│     ├──► employeeTable.Add(101, "Alice")   │
│     ├──► employeeTable.Add(102, "Bob")     │
│     ├──► employeeTable.Add(103, "Charlie") │
│     └──► employeeTable.Add(104, "Diana")   │
│                                                                  │
│     ▼                                                            │
│   STEP 2: Check for ID 105                                      │
│     ├──► Does 105 exist? → NO                                   │
│     └──► Add "Edward" with ID 105                               │
│                                                                  │
│     ▼                                                            │
│   STEP 3: Retrieve Employee 102 (Demonstrate Unboxing)         │
│     ├──► Get value for key 102 → returns object                 │
│     └──► Cast object to string → "Bob"                         │
│                                                                  │
│     ▼                                                            │
│   STEP 4: Iterate Through All Records                          │
│     └──► foreach(DictionaryEntry entry in employeeTable)       │
│           └──► Display ID: [key], Name: [value]                │
│                                                                  │
│     ▼                                                            │
│   STEP 5: Remove Employee 103                                   │
│     ├──► employeeTable.Remove(103)                              │
│     └──► Display remaining count                                │
│                                                                  │
│     ▼                                                            │
│   END                                                            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📝 Sample Output

```
═══════════════════════════════════════════════════════════════
              LEGACY EMPLOYEE DIRECTORY
         (System.Collections.Hashtable Demo)
═══════════════════════════════════════════════════════════════

STEP 1: INITIAL DATA LOAD
───────────────────────────────────────────────────────────────
✓ Added: ID 101 → Alice
✓ Added: ID 102 → Bob
✓ Added: ID 103 → Charlie
✓ Added: ID 104 → Diana

Initial employee count: 4
───────────────────────────────────────────────────────────────

STEP 2: UNIQUE KEY CHECK (ID: 105)
───────────────────────────────────────────────────────────────
🔍 Checking if ID 105 exists... NOT FOUND
➕ Adding Edward with ID 105
✅ Employee added successfully
───────────────────────────────────────────────────────────────

STEP 3: DATA RETRIEVAL WITH UNBOXING
───────────────────────────────────────────────────────────────
🔍 Retrieving employee with ID 102...

Raw object from Hashtable: System.Object
Casting to string... ✅ SUCCESS

Result: Employee Name = "Bob"
───────────────────────────────────────────────────────────────

STEP 4: ITERATING ALL RECORDS
───────────────────────────────────────────────────────────────
📋 CURRENT EMPLOYEE DIRECTORY:

    ID: 101, Name: Alice
    ID: 102, Name: Bob
    ID: 103, Name: Charlie
    ID: 104, Name: Diana
    ID: 105, Name: Edward

Total records displayed: 5
───────────────────────────────────────────────────────────────

STEP 5: DELETION OPERATION
───────────────────────────────────────────────────────────────
🗑️ Removing employee with ID 103 (Charlie)...

✓ Removal successful
📊 Remaining employees count: 4

Final employee list:
    ID: 101, Name: Alice
    ID: 102, Name: Bob
    ID: 104, Name: Diana
    ID: 105, Name: Edward
───────────────────────────────────────────────────────────────

═══════════════════════════════════════════════════════════════
            OPERATIONS COMPLETED SUCCESSFULLY
═══════════════════════════════════════════════════════════════
```

## 🧠 Key Concepts Demonstrated

### 1. Hashtable vs Dictionary<TKey,TValue>

| Feature                   | Hashtable (Legacy)        | Dictionary<TKey,TValue> (Modern) |
| ------------------------- | ------------------------- | -------------------------------- |
| **Type Safety**     | ❌ No (stores object)     | ✅ Yes (type-safe)               |
| **Boxing/Unboxing** | ✅ Required               | ❌ Not needed                    |
| **Performance**     | Slower (casting overhead) | Faster (type-specific)           |
| **Namespace**       | System.Collections        | System.Collections.Generic       |

### 2. Boxing/Unboxing Demonstration

```csharp
// BOXING: int value automatically boxed to object when stored
employeeTable.Add(102, "Bob");  // 102 (int) → object

// UNBOXING: object must be cast back to original type
object objValue = employeeTable[102];     // Returns object
string name = (string)objValue;            // Explicit cast required
```

### 3. DictionaryEntry Iteration

```csharp
// Hashtable requires DictionaryEntry for enumeration
foreach (DictionaryEntry entry in employeeTable)
{
    int id = (int)entry.Key;        // Unbox key
    string name = (string)entry.Value; // Unbox value
    Console.WriteLine($"ID: {id}, Name: {name}");
}
```

## 🔍 Operation Summary

| Task                      | Method Used                   | Return Type     | Notes                          |
| ------------------------- | ----------------------------- | --------------- | ------------------------------ |
| **Add Data**        | `Add(key, value)`           | void            | Both key/value boxed to object |
| **Check Existence** | `ContainsKey(key)`          | bool            | Type-safe check                |
| **Retrieve Data**   | `[key]` indexer             | object          | Requires unboxing              |
| **Iterate**         | `foreach` + DictionaryEntry | DictionaryEntry | Legacy enumeration pattern     |
| **Delete**          | `Remove(key)`               | void            | Removes entry if exists        |
| **Get Count**       | `Count` property            | int             | Number of entries              |

## ✅ Key Takeaways

1. **Hashtable** stores everything as `object` → requires casting
2. **Boxing** happens automatically when adding value types
3. **Unboxing** must be explicit when retrieving
4. **DictionaryEntry** is required for enumeration
5. **Legacy collections** are less type-safe than generic counterparts

---

**Happy Coding!** 👨‍💻
