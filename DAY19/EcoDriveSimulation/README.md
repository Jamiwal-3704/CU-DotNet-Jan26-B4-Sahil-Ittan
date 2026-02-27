# Eco-Drive Vehicle Simulation

A C# vehicle simulation demonstrating abstract classes, method overriding, and polymorphic behavior.

## 📋 Overview

This simulation implements a fleet management system where different vehicle types (ElectricCar, HeavyTruck, CargoPlane) inherit from an abstract Vehicle class, each with its own movement logic and fuel status reporting.

## 🏗️ Class Hierarchy

```
┌─────────────────────────────────────────────────────────────┐
│                     VEHICLE (ABSTRACT)                       │
│                       (Base Class)                           │
├─────────────────────────────────────────────────────────────┤
│  + ModelName : string                                        │
├─────────────────────────────────────────────────────────────┤
│  + abstract Move() : void                                    │
│  + virtual GetFuelStatus() : string                          │
│         "Fuel level is stable."                              │
└─────────────────────────┬───────────────────────────────────┘
                          │
        ┌─────────────────┼─────────────────┐
        │                 │                 │
        ▼                 ▼                 ▼
┌───────────────┐  ┌───────────────┐  ┌───────────────┐
│  ELECTRICCAR  │  │  HEAVYTRUCK   │  │  CARGOPLANE   │
├───────────────┤  ├───────────────┤  ├───────────────┤
│ Override Move │  │ Override Move │  │ Override Move │
│ Override Fuel │  │ Uses Base Fuel│  │ Override Fuel │
│   Status      │  │   Status      │  │  + base call  │
└───────────────┘  └───────────────┘  └───────────────┘
```

## 🚗 Vehicle Behaviors

| Vehicle Type | Move() Behavior | GetFuelStatus() Behavior |
|-------------|-----------------|-------------------------|
| **ElectricCar** | "gliding silently on battery power" | "battery is at 80%" (full override) |
| **HeavyTruck** | "hauling cargo with high-torque diesel" | Base: "Fuel level is stable" (no override) |
| **CargoPlane** | "ascending to 30,000 feet" | Base + "Checking jet fuel reserves..." |

## 🔄 Polymorphic Execution Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    RUNTIME EXECUTION FLOW                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   START                                                          │
│     │                                                            │
│     ▼                                                            │
│   Create Fleet Array                                             │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │ Vehicle[] fleet = {                                      │   │
│   │   new ElectricCar("Tesla Model 3"),                      │   │
│   │   new HeavyTruck("Volvo FH16"),                          │   │
│   │   new CargoPlane("Boeing 747")                           │   │
│   │ };                                                        │   │
│   └─────────────────────────────────────────────────────────┘   │
│                                                                  │
│     ▼                                                            │
│   FOREACH LOOP (Polymorphic Calls)                              │
│     │                                                            │
│     └──► Vehicle: Tesla Model 3                                 │
│           │                                                      │
│           ├──► Move(): "Tesla Model 3 is gliding silently..."   │
│           │        ╚══ ELECTRICCAR OVERRIDE                     │
│           │                                                      │
│           └──► Fuel(): "Tesla Model 3 battery is at 80%"        │
│                ╚══ ELECTRICCAR OVERRIDE                         │
│                                                                  │
│     └──► Vehicle: Volvo FH16                                    │
│           │                                                      │
│           ├──► Move(): "Volvo FH16 is hauling cargo..."         │
│           │        ╚══ HEAVYTRUCK OVERRIDE                      │
│           │                                                      │
│           └──► Fuel(): "Fuel level is stable."                  │
│                ╚══ BASE CLASS METHOD (NOT overridden)           │
│                                                                  │
│     └──► Vehicle: Boeing 747                                    │
│           │                                                      │
│           ├──► Move(): "Boeing 747 is ascending to 30,000 ft"   │
│           │        ╚══ CARGOPLANE OVERRIDE                      │
│           │                                                      │
│           └──► Fuel(): "Fuel level is stable. Checking jet..."  │
│                ╚══ OVERRIDE + BASE CALL                         │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📝 Sample Output

```
========== ECO-DRIVE FLEET SIMULATION ==========
            Polymorphism in Action
══════════════════════════════════════════════════

VEHICLE 1: Tesla Model 3
──────────────────────────────────────────
🚗 Tesla Model 3 is gliding silently on battery power.
🔋 Tesla Model 3 battery is at 80%.

══════════════════════════════════════════════════

VEHICLE 2: Volvo FH16
──────────────────────────────────────────
🚚 Volvo FH16 is hauling cargo with high-torque diesel power.
⛽ Fuel level is stable.

══════════════════════════════════════════════════

VEHICLE 3: Boeing 747
──────────────────────────────────────────
✈️ Boeing 747 is ascending to 30,000 feet.
⛽ Fuel level is stable. Checking jet fuel reserves...

══════════════════════════════════════════════════

FLEET MOVEMENT COMPLETE
All vehicles dispatched successfully!
══════════════════════════════════════════════════
```

## 🧠 Polymorphism Deep Dive

### How Runtime Selection Works

```
┌─────────────────────────────────────────────────────────────────┐
│                    METHOD RESOLUTION TABLE                       │
├──────────────┬─────────────────┬─────────────────┬─────────────┤
│  OBJECT      │  Move() Called  │  Source        │  Fuel() Called│
│  TYPE        │                 │                │               │
├──────────────┼─────────────────┼─────────────────┼─────────────┤
│ ElectricCar  │ ElectricCar     │ Override       │ ElectricCar  │
│ HeavyTruck   │ HeavyTruck      │ Override       │ Vehicle      │
│ CargoPlane   │ CargoPlane      │ Override       │ CargoPlane   │
└──────────────┴─────────────────┴─────────────────┴─────────────┘

KEY INSIGHTS:
═══════════════
1. ALL Move() calls use derived class implementations
   → Because Move() is ABSTRACT, MUST be overridden

2. Fuel() calls vary based on override status:
   → ElectricCar: FULL OVERRIDE (replaces base)
   → HeavyTruck: NO OVERRIDE (uses base)
   → CargoPlane: OVERRIDE + base (extends base)

3. NO TYPE CHECKING NEEDED - Polymorphism handles it!
   → No if/else or switch statements required
   → Runtime determines correct method automatically
```

## 🎯 Key Concepts Demonstrated

### 1. **Abstract Classes**
```csharp
public abstract class Vehicle
{
    public string ModelName { get; set; }
    
    // Must be overridden by derived classes
    public abstract void Move();
    
    // Can be optionally overridden
    public virtual string GetFuelStatus()
    {
        return "Fuel level is stable.";
    }
}
```

### 2. **Method Overriding Types**

| Override Type | Implementation | When Used |
|--------------|----------------|-----------|
| **Abstract Override** | `public override void Move()` | Required for all derived classes |
| **Virtual Override** | `public override string GetFuelStatus()` | Optional, provides custom behavior |
| **Base Call + Override** | `base.GetFuelStatus() + custom` | Extends rather than replaces base |

### 3. **Polymorphic Collection**
```csharp
// Single array can hold ANY vehicle type
Vehicle[] fleet = new Vehicle[]
{
    new ElectricCar("Tesla"),
    new HeavyTruck("Volvo"),
    new CargoPlane("Boeing")
};

// Single loop handles ALL types automatically
foreach (Vehicle v in fleet)
{
    v.Move();              // Correct version called automatically
    Console.WriteLine(v.GetFuelStatus());  // Correct version called automatically
}
```

## 📊 Method Call Hierarchy

```
Vehicle Reference (Abstract)
        │
        ├──► Move() [ABSTRACT]
        │       │
        │       ├──► ElectricCar.Move()  ✓
        │       ├──► HeavyTruck.Move()   ✓
        │       └──► CargoPlane.Move()   ✓
        │
        └──► GetFuelStatus() [VIRTUAL]
                │
                ├──► ElectricCar.GetFuelStatus()  ✓ (Override)
                │       └── Replaces base completely
                │
                ├──► HeavyTruck.GetFuelStatus()   ⚠ (No override)
                │       └── Uses Vehicle version
                │
                └──► CargoPlane.GetFuelStatus()   ✓ (Override)
                        └── Calls base THEN adds custom
```

## ✅ Success Criteria Checklist

| Criteria | Achieved | How |
|----------|----------|-----|
| **No Interfaces** | ✅ | Pure class inheritance |
| **Runtime Selection** | ✅ | Polymorphism determines correct method |
| **Abstraction** | ✅ | Cannot instantiate Vehicle directly |
| **No Type Checking** | ✅ | Loop uses Vehicle references only |
| **Base Call Support** | ✅ | CargoPlane uses base.GetFuelStatus() |

## 🔍 Key Takeaways

1. **Abstract methods** FORCE derived classes to provide implementation
2. **Virtual methods** provide OPTIONAL override capability
3. **base keyword** allows calling parent implementation
4. **Polymorphism** enables treating all vehicles uniformly
5. **Runtime binding** ensures correct method execution

---

**Happy Driving!** 🚀