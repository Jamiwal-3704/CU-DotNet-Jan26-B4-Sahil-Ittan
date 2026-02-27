# Employee Compensation Management System

A C# implementation demonstrating method hiding in inheritance without using virtual methods or abstract classes.

## 📋 Overview

This case study implements an employee compensation system with different employee types, showcasing how to handle polymorphic behavior using **method hiding** (`new` keyword) instead of traditional virtual/override approaches.

## 🏗️ Class Hierarchy

```
                    ┌─────────────────┐
                    │    EMPLOYEE     │
                    │   (Base Class)  │
                    ├─────────────────┤
                    │ EmployeeId      │
                    │ EmployeeName    │
                    │ BasicSalary     │
                    │ ExperienceYears │
                    ├─────────────────┤
                    │ CalculateAnnual │
                    │   Salary()       │
                    │ DisplayDetails() │
                    └────────┬────────┘
                             │
        ┌────────────────────┼────────────────────┐
        │                    │                    │
        ▼                    ▼                    ▼
┌───────────────┐    ┌───────────────┐    ┌───────────────┐
│  PERMANENT    │    │   CONTRACT    │    │    INTERN     │
│   EMPLOYEE    │    │   EMPLOYEE    │    │   EMPLOYEE    │
├───────────────┤    ├───────────────┤    ├───────────────┤
│ HRA: 20%      │    │ ContractDuration│  │ Fixed Stipend │
│ Special: 10%  │    │ Bonus: ₹30K   │    │ No Allowances │
│ Loyalty: ₹50K │    │ if ≥12 months │    │               │
│ (if exp ≥5yrs)│    │               │    │               │
├───────────────┤    ├───────────────┤    ├───────────────┤
│ new Calculate │    │ new Calculate │    │ new Calculate │
│   Salary()    │    │   Salary()    │    │   Salary()    │
└───────────────┘    └───────────────┘    └───────────────┘
```

## 💼 Employee Categories & Calculation Rules

| Employee Type | Base Salary | Allowances | Bonus Rules | Annual Salary Formula |
|--------------|-------------|------------|-------------|----------------------|
| **Base Employee** | BasicSalary × 12 | None | None | BasicSalary × 12 |
| **Permanent** | BasicSalary × 12 | HRA (20%) + Special (10%) | ₹50,000 if exp ≥ 5 yrs | (Basic × 12) + Allowances + Bonus |
| **Contract** | BasicSalary × 12 | None | ₹30,000 if duration ≥ 12 mos | (Basic × 12) + Bonus |
| **Intern** | BasicSalary × 12 | None | None | BasicSalary × 12 |

## 🔄 Method Hiding Behavior

```
┌─────────────────────────────────────────────────────────────────┐
│                    METHOD CALL RESOLUTION                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   Employee emp = new PermanentEmployee(...);                    │
│   emp.CalculateAnnualSalary()  →  Calls BASE class method       │
│                                                                  │
│   PermanentEmployee perm = new PermanentEmployee(...);          │
│   perm.CalculateAnnualSalary() →  Calls DERIVED class method    │
│                                                                  │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   WHY?                                                          │
│   ────────────────────────────────────────────────────────────  │
│   • No virtual methods = No polymorphism                        │
│   • Method hiding is STATIC (compile-time) not dynamic          │
│   • Reference type determines which method executes             │
│   • Object type doesn't matter for method resolution            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📊 Sample Output

```
========== EMPLOYEE COMPENSATION SYSTEM ==========

BASE EMPLOYEE (via Base Reference)
═══════════════════════════════════════════════
ID: 1001 | Name: John Doe
Basic Salary: ₹50,000 | Experience: 3 years
Annual Salary: ₹600,000 (Base Calculation)
───────────────────────────────────────────

PERMANENT EMPLOYEE (via Derived Reference)
═══════════════════════════════════════════════
ID: 2001 | Name: Jane Smith
Basic Salary: ₹60,000 | Experience: 5 years
Salary Breakdown:
├─ Base Annual: ₹720,000
├─ HRA (20%): ₹144,000
├─ Special (10%): ₹72,000
├─ Loyalty Bonus: ₹50,000 (5+ years exp)
└─ TOTAL: ₹986,000
───────────────────────────────────────────

CONTRACT EMPLOYEE (via Derived Reference)
═══════════════════════════════════════════════
ID: 3001 | Name: Mike Johnson
Basic Salary: ₹40,000 | Experience: 2 years
Contract Duration: 15 months
Salary Breakdown:
├─ Base Annual: ₹480,000
├─ Contract Bonus: ₹30,000 (≥12 months)
└─ TOTAL: ₹510,000
───────────────────────────────────────────

INTERN EMPLOYEE (via Derived Reference)
═══════════════════════════════════════════════
ID: 4001 | Name: Sarah Williams
Basic Salary: ₹15,000 | Experience: 0 years
Annual Salary: ₹180,000 (Fixed Stipend)
───────────────────────────────────────────

========== POLYMORPHISM DEMO ==========
Using Base Class References:

emp1 (Permanent) via Base ref: ₹600,000
emp2 (Contract) via Base ref: ₹480,000
emp3 (Intern) via Base ref: ₹180,000

NOTE: All show BASE calculation!
Method hiding requires DERIVED references
==========================================
```

## 🎯 Key Concepts Demonstrated

### 1. **Method Hiding (`new` keyword)**
```csharp
public class Employee
{
    public decimal CalculateAnnualSalary()  // NOT virtual
    {
        return BasicSalary * 12;
    }
}

public class PermanentEmployee : Employee
{
    public new decimal CalculateAnnualSalary()  // Hides base method
    {
        decimal baseAnnual = base.CalculateAnnualSalary();
        decimal hra = BasicSalary * 0.2m * 12;
        decimal special = BasicSalary * 0.1m * 12;
        decimal bonus = ExperienceInYears >= 5 ? 50000 : 0;
        
        return baseAnnual + hra + special + bonus;
    }
}
```

### 2. **Reference Type vs Object Type**
```csharp
// Reference type = Employee, Object type = PermanentEmployee
Employee empRef = new PermanentEmployee(2001, "Jane", 60000, 5);

// Calls Employee.CalculateAnnualSalary() (reference type decides)
Console.WriteLine(empRef.CalculateAnnualSalary());  // Base: 720,000

// To call PermanentEmployee method, need correct reference
PermanentEmployee permRef = new PermanentEmployee(2001, "Jane", 60000, 5);
Console.WriteLine(permRef.CalculateAnnualSalary());  // Derived: 986,000
```

## 📈 Compensation Calculation Flow

```
─────────────────────────────────────────────────────────────
                    CALCULATION PIPELINE
─────────────────────────────────────────────────────────────

EMPLOYEE (Base)
    │
    └──► Annual = Basic × 12
         Return to caller

PERMANENT EMPLOYEE
    │
    ├──► Get Base Annual (Basic × 12)
    ├──► Add HRA (20% of Basic × 12)
    ├──► Add Special Allowance (10% of Basic × 12)
    ├──► Add Loyalty Bonus (₹50K if exp ≥ 5)
    │
    └──► Return Total

CONTRACT EMPLOYEE
    │
    ├──► Get Base Annual (Basic × 12)
    ├──► Check Contract Duration
    │    └──► Add ₹30K if ≥ 12 months
    │
    └──► Return Total

INTERN EMPLOYEE
    │
    └──► Return Base Annual (Basic × 12)
─────────────────────────────────────────────────────────────
```

## 🔍 Important Observations

### Method Hiding vs Overriding

| Aspect | Method Hiding (`new`) | Method Overriding (`virtual/override`) |
|--------|----------------------|----------------------------------------|
| **Polymorphism** | ❌ No | ✅ Yes |
| **Resolution Time** | Compile-time | Runtime |
| **Base Ref Behavior** | Calls base method | Calls derived method |
| **Keyword Required** | `new` in derived | `virtual` in base, `override` in derived |

### When to Use Each Reference Type

```csharp
// When you want BASE class behavior
Employee ref1 = new PermanentEmployee(...);
ref1.CalculateAnnualSalary();  // Gets BASE calculation

// When you want DERIVED class behavior
PermanentEmployee ref2 = new PermanentEmployee(...);
ref2.CalculateAnnualSalary();  // Gets FULL calculation with allowances
```

## ✅ Key Takeaways

1. **Method hiding** is static binding - compiler decides based on reference type
2. **Without virtual methods**, polymorphism doesn't work automatically
3. **Reference type** determines which method executes, not object type
4. **Use `base` keyword** to call base class methods from derived classes
5. **New keyword** explicitly hides base class members

## 📝 Usage Scenarios

```csharp
// Scenario 1: Processing payroll with correct references
List<PermanentEmployee> permanentStaff = GetPermanentEmployees();
foreach(var emp in permanentStaff)
{
    // CORRECT: Gets full salary with allowances
    decimal salary = emp.CalculateAnnualSalary();
}

// Scenario 2: Generic processing (only base salary)
List<Employee> allStaff = GetAllEmployees();
foreach(var emp in allStaff)
{
    // Base salary only (method hiding in effect)
    decimal salary = emp.CalculateAnnualSalary();
}
```

---

**Happy Coding!** 🚀
