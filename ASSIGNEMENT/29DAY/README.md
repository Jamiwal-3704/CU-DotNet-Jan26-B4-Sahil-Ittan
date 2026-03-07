# 🏠 Smart Kitchen Architect - Object-Oriented Design System

## Project Overview

A sophisticated backend system for AeroCook's home automation startup that models smart kitchen appliances. This project demonstrates advanced object-oriented design principles including inheritance, interfaces, and polymorphism to create a modular and extensible architecture.

## The Challenge

Design a software system for three distinct kitchen devices that share common traits but have unique capabilities, ensuring no code duplication while maintaining device-specific features.

## Device Profiles

### 1. 📱 Microwave

* Basic cooking appliance
* Requires timer functionality
* Has its own specific cooking process
* No smart connectivity

### 2. 🔥 Electric Oven (Flagship Product)

* Advanced cooking appliance
* Requires timer functionality
* WiFi-enabled for remote monitoring
* Needs preheating stage before cooking
* Smart connectivity features

### 3. 💨 Air Fryer

* Simple mechanical device
* Quick cooking mechanism
* No digital timer
* No smart connectivity
* Direct cooking without preheating

## Learning Objectives

* Abstract class design and implementation
* Interface segregation principle
* Polymorphic behavior
* Code reuse through inheritance
* Contract enforcement via interfaces
* Extensible architecture design

## Solution Architecture

### Base Class: Appliance

**csharp**

```
public abstract class Appliance
{
    public string ModelName { get; set; }
    public double PowerConsumption { get; set; } // Watts

    // Common constructor
    public Appliance(string modelName, double powerConsumption)
    {
        ModelName = modelName;
        PowerConsumption = powerConsumption;
    }

    // Abstract method - every appliance must implement its own cooking logic
    public abstract void Cook();

    // Virtual method - default behavior that can be overridden
    public virtual void Preheat()
    {
        Console.WriteLine("No preheating required. Starting immediately.");
    }

    // Common method for all appliances
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Model: {ModelName}");
        Console.WriteLine($"Power: {PowerConsumption}W");
    }
}
```

### Interface Contracts

**csharp**

```
// Timer capability contract
public interface ITimerEnabled
{
    void SetTimer(int minutes);
    void StartTimer();
    void CancelTimer();
}

// WiFi capability contract
public interface IWiFiEnabled
{
    void ConnectToWiFi(string networkName);
    void DisconnectWiFi();
    bool IsConnected { get; }
}
```

### Device Implementations

#### Microwave Class

**csharp**

```
public class Microwave : Appliance, ITimerEnabled
{
    private int cookingTime;

    public Microwave(string modelName, double powerConsumption) 
        : base(modelName, powerConsumption) { }

    // ITimerEnabled implementation
    public void SetTimer(int minutes)
    {
        cookingTime = minutes;
        Console.WriteLine($"Microwave timer set to {minutes} minutes");
    }

    public void StartTimer()
    {
        Console.WriteLine($"Microwave timer started: {cookingTime} minutes remaining");
    }

    public void CancelTimer()
    {
        Console.WriteLine("Microwave timer cancelled");
    }

    // Specific cooking implementation
    public override void Cook()
    {
        Console.WriteLine("🔵 Microwave: Cooking with electromagnetic waves");
        Console.WriteLine($"Cooking for {cookingTime} minutes at full power");
    }
}
```

#### ElectricOven Class

**csharp**

```
public class ElectricOven : Appliance, ITimerEnabled, IWiFiEnabled
{
    private int cookingTime;
    public bool IsConnected { get; private set; }

    public ElectricOven(string modelName, double powerConsumption) 
        : base(modelName, powerConsumption) { }

    // ITimerEnabled implementation
    public void SetTimer(int minutes)
    {
        cookingTime = minutes;
        Console.WriteLine($"Oven timer set to {minutes} minutes");
    }

    public void StartTimer()
    {
        Console.WriteLine($"Oven timer started: {cookingTime} minutes remaining");
    }

    public void CancelTimer()
    {
        Console.WriteLine("Oven timer cancelled");
    }

    // IWiFiEnabled implementation
    public void ConnectToWiFi(string networkName)
    {
        IsConnected = true;
        Console.WriteLine($"🔗 Oven connected to WiFi network: {networkName}");
    }

    public void DisconnectWiFi()
    {
        IsConnected = false;
        Console.WriteLine("Oven disconnected from WiFi");
    }

    // Override preheat for oven
    public override void Preheat()
    {
        Console.WriteLine("🔥 Oven preheating to 350°F... Please wait 10 minutes");
    }

    // Specific cooking implementation
    public override void Cook()
    {
        Console.WriteLine("🔥 Electric Oven: Cooking with radiant heat");
        if (IsConnected)
        {
            Console.WriteLine("📱 Remote monitoring active - check your phone!");
        }
        Console.WriteLine($"Baking for {cookingTime} minutes at optimal temperature");
    }
}
```

#### AirFryer Class

**csharp**

```
public class AirFryer : Appliance
{
    public AirFryer(string modelName, double powerConsumption) 
        : base(modelName, powerConsumption) { }

    // Specific cooking implementation
    public override void Cook()
    {
        Console.WriteLine("💨 Air Fryer: Rapid air circulation cooking");
        Console.WriteLine("Cooking complete in 15 minutes - no timer needed!");
    }

    // Note: No timer or WiFi interfaces implemented
}
```

## Class Diagram

**text**

```
┌─────────────────────────┐
│    ABSTRACT            │
│     Appliance          │
├─────────────────────────┤
│ # ModelName: string    │
│ # PowerConsumption: double│
├─────────────────────────┤
│ + Cook() (abstract)    │
│ + Preheat() (virtual)  │
│ + DisplayInfo()        │
└──────────┬──────────────┘
           │
    ┌──────┴──────┬──────────────┐
    │             │              │
    ▼             ▼              ▼
┌─────────┐ ┌───────────┐ ┌──────────┐
│Microwave│ │ElectricOven│ │ AirFryer │
├─────────┤ ├───────────┤ ├──────────┤
│         │ │           │ │          │
└────┬────┘ └─────┬─────┘ └────┬─────┘
     │            │            │
     │            │            │
┌────▼────┐ ┌─────▼─────┐
│interface│ │ interface │
│ITimer-  │ │IWiFi-     │
│Enabled  │ │Enabled    │
└─────────┘ └───────────┘
     ▲            ▲
     └──────┬─────┘
            │
      ┌─────┴─────┐
      │Implements │
      └───────────┘
```

## Project Flowchart

**text**

```
┌─────────────────────┐
│        START        │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│   Create Device     │
│    Instances        │
│ - Microwave         │
│ - ElectricOven      │
│ - AirFryer          │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Store in List      │
│  List<Appliance>    │
│  (Polymorphism)     │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Loop Through       │
│  Each Appliance     │
└──────────┬──────────┘
           ↓
    ┌──────┴──────┐
    │ Display      │
    │ Device Info  │
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ Call Preheat │
    │ (Virtual)    │
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ Check for    │
    │ Interfaces   │
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ is ITimer-   │
    │ Enabled?     │
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │  Yes ──► Set &  │
    │       Start Timer│
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ is IWiFi-    │
    │ Enabled?     │
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │  Yes ──► Connect │
    │       to WiFi    │
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ Call Cook()  │
    │ (Polymorphic)│
    └──────┬──────┘
           ↓
    ┌──────┴──────┐
    │ More devices?│
    │   ┌──┴──┐   │
    │  Yes    No  │
    └──┬───────┬┘ │
       ↓       └──┘
    ┌──────┴──────┐
    │   End Loop  │
    └──────────┬──┘
               ↓
┌─────────────────────┐
│   Display Summary   │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│         END         │
└─────────────────────┘
```

## Test Program Output Example

**text**

```
=== SMART KITCHEN TEST RUN ===

🔹 MICROWAVE - Model: MW-1000
Power: 1200W
No preheating required. Starting immediately.
⏰ Setting timer: 5 minutes
⏰ Timer started
🔵 Microwave: Cooking with electromagnetic waves
Cooking for 5 minutes at full power
----------------------------------------

🔸 ELECTRIC OVEN - Model: EO-3500
Power: 3500W
🔥 Oven preheating to 350°F... Please wait 10 minutes
⏰ Setting timer: 45 minutes
⏰ Timer started
🔗 Connected to WiFi network: AeroCook_Home
📱 Remote monitoring active - check your phone!
🔥 Electric Oven: Cooking with radiant heat
Baking for 45 minutes at optimal temperature
----------------------------------------

🔹 AIR FRYER - Model: AF-1500
Power: 1500W
No preheating required. Starting immediately.
💨 Air Fryer: Rapid air circulation cooking
Cooking complete in 15 minutes - no timer needed!
----------------------------------------
```

## Key Design Decisions

### 1. Abstract Base Class (Appliance)

* **Why?** Provides common properties while preventing instantiation
* **What?** ModelName, PowerConsumption, common methods
* **Benefit** Code reuse and polymorphic collection support

### 2. Interface Segregation

* **ITimerEnabled** : Only for devices with timers
* **IWiFiEnabled** : Only for smart devices
* **Principle** : Don't force classes to implement unused methods

### 3. Virtual Methods (Preheat)

* Default implementation in base class
* Override only when needed (Oven)
* Follows Open/Closed Principle

### 4. Abstract Methods (Cook)

* Forces every device to implement
* Ensures polymorphic behavior
* Device-specific cooking logic

## Design Patterns Used

* **Template Method** : Base class defines skeleton
* **Strategy** : Different cooking algorithms
* **Interface Segregation** : Specific interfaces
* **Polymorphism** : Runtime method resolution

## Testing Scenarios

### Scenario 1: Polymorphic Collection

**csharp**

```
List<Appliance> kitchen = new List<Appliance>
{
    new Microwave("MW-1000", 1200),
    new ElectricOven("EO-3500", 3500),
    new AirFryer("AF-1500", 1500)
};

foreach(var appliance in kitchen)
{
    appliance.Cook(); // Different for each!
}
```

### Scenario 2: Interface Checking

**csharp**

```
if (appliance is ITimerEnabled timer)
{
    timer.SetTimer(10);
    timer.StartTimer();
}

if (appliance is IWiFiEnabled wifi)
{
    wifi.ConnectToWiFi("HomeNetwork");
}
```

## Extensions/Ideas

* ✅ Add Bluetooth-enabled devices
* ✅ Implement voice control interfaces
* ✅ Add energy efficiency ratings
* ✅ Create recipe management system
* ✅ Implement scheduling capabilities
* ✅ Add diagnostic interfaces
* ✅ Create mobile app integration

## Prerequisites

* Advanced C# concepts
* Interface design
* Abstract classes
* Polymorphism
* Type checking and casting
* Object-oriented principles

This project demonstrates professional-grade object-oriented design that's modular, maintainable, and extensible - exactly what modern IoT and home automation systems require.
