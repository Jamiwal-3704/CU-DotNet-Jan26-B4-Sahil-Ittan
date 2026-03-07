# SafeDrive Policy Optimizer

A C# insurance policy management system demonstrating dictionary operations, bulk updates, and safe data manipulation techniques.

## 📋 Overview

This PolicyTracker system manages auto insurance policies using a Dictionary collection, enabling efficient policy lookups, bulk premium adjustments, and compliant data retention through policy removal.

## 🏗️ System Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                   POLICYTRACKER SYSTEM                       │
│                     (Dictionary-Based)                       │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  ┌─────────────────────────────────────────────────────┐   │
│  │               POLICY DICTIONARY                       │   │
│  │  ┌─────────┐    ┌─────────────────────────────┐   │   │
│  │  │  Key    │    │           Value             │   │   │
│  │  │ (string)│    │        (Policy Object)      │   │   │
│  │  ├─────────┼────┼─────────────────────────────┤   │   │
│  │  │ "POL001"│───▶│ Holder: John Smith         │   │   │
│  │  │         │    │ Premium: $850.50           │   │   │
│  │  │         │    │ RiskScore: 82              │   │   │
│  │  │         │    │ Renewal: 2025-03-15        │   │   │
│  │  ├─────────┼────┼─────────────────────────────┤   │   │
│  │  │ "POL002"│───▶│ Holder: Emma Jones         │   │   │
│  │  │         │    │ Premium: $1,200.00         │   │   │
│  │  │         │    │ RiskScore: 68              │   │   │
│  │  │         │    │ Renewal: 2024-11-20        │   │   │
│  │  └─────────┘    └─────────────────────────────┘   │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                              │
│  ┌─────────────────────────────────────────────────────┐   │
│  │                SYSTEM OPERATIONS                      │   │
│  ├─────────────────────────────────────────────────────┤   │
│  │  • Bulk Adjustment: +5% premium if RiskScore > 75   │   │
│  │  • Clean-Up: Remove policies > 3 years old          │   │
│  │  • Security Check: Safe lookup with fallback        │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

## 📊 Policy Data Structure

| Property              | Type     | Description               | Example        |
| --------------------- | -------- | ------------------------- | -------------- |
| **HolderName**  | string   | Policy holder's full name | "John Smith"   |
| **Premium**     | decimal  | Annual premium amount     | $850.50        |
| **RiskScore**   | int      | Risk assessment (1-100)   | 82 (High risk) |
| **RenewalDate** | DateTime | Next renewal date         | 2025-03-15     |

## 🔄 Operational Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    SYSTEM OPERATIONS FLOW                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   INITIAL POLICY DICTIONARY                                      │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │ POL001: $850 | Risk:82 | Renew:2025-03-15              │   │
│   │ POL002: $1,200 | Risk:68 | Renew:2024-11-20            │   │
│   │ POL003: $650 | Risk:45 | Renew:2023-08-10              │   │
│   │ POL004: $2,100 | Risk:91 | Renew:2025-06-30            │   │
│   │ POL005: $950 | Risk:79 | Renew:2022-01-15 (OLD)        │   │
│   └─────────────────────────────────────────────────────────┘   │
│                           │                                       │
│           ┌───────────────┼───────────────┐                      │
│           │               │               │                      │
│           ▼               ▼               ▼                      │
│   ┌───────────────┐ ┌───────────────┐ ┌───────────────┐          │
│   │  BULK         │ │   CLEAN-UP    │ │   SECURITY    │          │
│   │  ADJUSTMENT   │ │               │ │    CHECK      │          │
│   ├───────────────┤ ├───────────────┤ ├───────────────┤          │
│   │ Find policies │ │ Find policies │ │ Lookup policy │          │
│   │ with RiskScore│ │ with Renewal  │ │ by ID         │          │
│   │ > 75          │ │ < Today-3yrs  │ │               │          │
│   │       │       │ │       │       │ │       │       │          │
│   │       ▼       │ │       ▼       │ │       ▼       │          │
│   │ Apply +5%     │ │ Store keys in │ │ If found:     │          │
│   │ premium       │ │ separate list │ │ return policy │          │
│   │ increase      │ │       │       │ │               │          │
│   │       │       │ │       ▼       │ │ If not found: │          │
│   │       │       │ │ Remove from   │ │ return custom │          │
│   │       │       │ │ dictionary    │ │ "Not Found"   │          │
│   └───────┴───────┘ └───────┴───────┘ └───────┴───────┘          │
│                           │                                       │
│                           ▼                                       │
│                 UPDATED POLICY DICTIONARY                         │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │ POL001: $892.50 | Risk:82 | Renew:2025-03-15 (+5%)     │   │
│   │ POL002: $1,200 | Risk:68 | Renew:2024-11-20 (No change)│   │
│   │ POL003: $650 | Risk:45 | Renew:2023-08-10 (No change)  │   │
│   │ POL004: $2,205 | Risk:91 | Renew:2025-06-30 (+5%)      │   │
│   │ POL005: REMOVED (Expired >3 years)                      │   │
│   └─────────────────────────────────────────────────────────┘   │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📝 Sample Output

```
═══════════════════════════════════════════════════════════════
              SAFEDRIVE POLICY OPTIMIZER
═══════════════════════════════════════════════════════════════

INITIAL POLICY PORTFOLIO (5 policies loaded)
───────────────────────────────────────────────────────────────
POL001 | John Smith      | $850.00  | Risk:82 | Renews: 2025-03-15
POL002 | Emma Jones      | $1,200.00 | Risk:68 | Renews: 2024-11-20
POL003 | Robert Chen     | $650.00  | Risk:45 | Renews: 2023-08-10
POL004 | Maria Garcia    | $2,100.00 | Risk:91 | Renews: 2025-06-30
POL005 | David Kim       | $950.00  | Risk:79 | Renews: 2022-01-15

═══════════════════════════════════════════════════════════════
STEP 1: BULK ADJUSTMENT (RiskScore > 75 → +5% premium)
───────────────────────────────────────────────────────────────
✓ POL001: Risk 82 → Premium increased from $850.00 to $892.50
✓ POL004: Risk 91 → Premium increased from $2,100.00 to $2,205.00
✓ POL005: Risk 79 → Premium increased from $950.00 to $997.50
ℹ POL002: Risk 68 (below threshold) - No change
ℹ POL003: Risk 45 (below threshold) - No change

Total policies updated: 3 of 5
───────────────────────────────────────────────────────────────

═══════════════════════════════════════════════════════════════
STEP 2: CLEAN-UP (Removing policies with RenewalDate older than 3 years)
───────────────────────────────────────────────────────────────
Current date: 2025-05-15
3-year threshold: 2022-05-15

✗ POL005 | Renewal: 2022-01-15 → REMOVED (Expired)
✓ POL001 | Renewal: 2025-03-15 → Kept
✓ POL002 | Renewal: 2024-11-20 → Kept
✓ POL003 | Renewal: 2023-08-10 → Kept
✓ POL004 | Renewal: 2025-06-30 → Kept

Total policies removed: 1
Active policies remaining: 4
───────────────────────────────────────────────────────────────

═══════════════════════════════════════════════════════════════
STEP 3: SECURITY CHECK (Safe Lookup Operations)
───────────────────────────────────────────────────────────────

🔍 Looking up Policy ID: POL001
✅ FOUND:
   └─ Holder: John Smith | Premium: $892.50 | Risk:82 | Renews: 2025-03-15

🔍 Looking up Policy ID: POL999
❌ NOT FOUND: Policy ID 'POL999' does not exist in the system

🔍 Looking up Policy ID: POL005
❌ NOT FOUND: Policy ID 'POL005' does not exist in the system
   (This policy was removed during clean-up)

═══════════════════════════════════════════════════════════════
FINAL PORTFOLIO SUMMARY
═══════════════════════════════════════════════════════════════
Total Active Policies: 4
Total Premium Value: $4,945.00
Average Risk Score: 71.5
Oldest Renewal: 2023-08-10
Newest Renewal: 2025-06-30
═══════════════════════════════════════════════════════════════
```

## 🧠 Key Implementation Techniques

### 1. Safe Dictionary Modification

```csharp
// ❌ DANGEROUS - Modifying during enumeration
foreach(var kvp in policies)
{
    if(ShouldRemove(kvp.Value))
        policies.Remove(kvp.Key); // THROWS EXCEPTION!
}

// ✅ SAFE - Store keys first, then remove
List<string> keysToRemove = new List<string>();
foreach(var kvp in policies)
{
    if(kvp.Value.RenewalDate < cutoffDate)
        keysToRemove.Add(kvp.Key);
}

foreach(string key in keysToRemove)
{
    policies.Remove(key);
}
```

### 2. Safe Lookup with TryGetValue

```csharp
// ❌ DANGEROUS - May throw KeyNotFoundException
Policy policy = policies[policyId]; // CRASH if not found!

// ✅ SAFE - Uses TryGetValue
public string GetPolicyDetails(string policyId)
{
    if (policies.TryGetValue(policyId, out Policy policy))
    {
        return $"FOUND: {policy.HolderName} | Premium: {policy.Premium:C}";
    }
    return $"NOT FOUND: Policy ID '{policyId}' does not exist";
}
```

## 🔍 Operation Complexity Analysis

| Operation                 | Time Complexity | Space Complexity             | Technique Used        |
| ------------------------- | --------------- | ---------------------------- | --------------------- |
| **Bulk Adjustment** | O(n)            | O(1)                         | Iterate and update    |
| **Clean-Up**        | O(n)            | O(k) where k = removed items | Store keys separately |
| **Safe Lookup**     | O(1) avg        | O(1)                         | TryGetValue pattern   |

## ✅ Key Takeaways

| Concept                            | Implementation         | Benefit                              |
| ---------------------------------- | ---------------------- | ------------------------------------ |
| **Dictionary<TKey, TValue>** | Store policies by ID   | O(1) average lookup time             |
| **TryGetValue**              | Safe retrieval pattern | Prevents KeyNotFoundException        |
| **Separate keys list**       | Store before removing  | Avoids collection modification error |
| **decimal type**             | Premium calculations   | No floating-point rounding errors    |
| **DateTime comparison**      | Renewal date check     | Accurate policy expiration           |

---

**Safe Driving!** 🚗
