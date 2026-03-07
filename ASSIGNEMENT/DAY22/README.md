# OLA Driver Ride Management System

A C# console application demonstrating object composition and collection management for tracking OLA drivers and their rides.

## 📋 Overview

This application manages multiple OLA drivers, each with multiple rides, and displays driver-wise ride details with total fare calculations.

## 🏗️ Class Structure

```
┌─────────────────────────────────────────────────────────────┐
│                       OLADRIVER CLASS                        │
├─────────────────────────────────────────────────────────────┤
│  + Id : int                                                 │
│  + Name : string                                            │
│  + VehicleNo : string                                       │
│  + Rides : List<Ride>                                       │
└────────────────────────────┬────────────────────────────────┘
                             │
                 ┌───────────┴───────────┐
                 │    contains many       │
                 ▼                        ▼
┌─────────────────────────────────────────────────────────────┐
│                         RIDE CLASS                           │
├─────────────────────────────────────────────────────────────┤
│  + RideId : int                                             │
│  + From : string                                            │
│  + To : string                                              │
│  + Fare : decimal                                           │
└─────────────────────────────────────────────────────────────┘
```

## 🔄 System Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    APPLICATION FLOW                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   START                                                          │
│     │                                                            │
│     ▼                                                            │
│   Create Drivers with Rides                                      │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │ Driver 1: Rahul (KA-01-1234)                            │   │
│   │   ├── Ride 1: Airport → City Center (₹450)             │   │
│   │   ├── Ride 2: City Center → Mall (₹180)                │   │
│   │   └── Ride 3: Mall → Railway Station (₹320)            │   │
│   │                                                         │   │
│   │ Driver 2: Priya (KA-02-5678)                            │   │
│   │   ├── Ride 1: Tech Park → Indiranagar (₹250)           │   │
│   │   ├── Ride 2: Indiranagar → Koramangala (₹220)         │   │
│   │   └── Ride 3: Koramangala → Airport (₹850)             │   │
│   │                                                         │   │
│   │ Driver 3: Amit (KA-03-9012)                             │   │
│   │   ├── Ride 1: Whitefield → MG Road (₹380)              │   │
│   │   └── Ride 2: MG Road → Electronic City (₹540)         │   │
│   └─────────────────────────────────────────────────────────┘   │
│                                                                  │
│     ▼                                                            │
│   Display Driver-wise Report                                     │
│     │                                                            │
│     └──► For each driver:                                        │
│           │                                                      │
│           ├──► Show Driver Details                               │
│           │     • ID, Name, Vehicle No                          │
│           │                                                      │
│           ├──► List All Rides                                    │
│           │     • Ride ID, From → To, Fare                      │
│           │                                                      │
│           └──► Calculate & Display Total Fare                   │
│                                                                  │
│     ▼                                                            │
│   END                                                            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📝 Sample Output

```
═══════════════════════════════════════════════════════════════
              OLA DRIVER RIDE MANAGEMENT SYSTEM
═══════════════════════════════════════════════════════════════

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
DRIVER #1: Rahul (KA-01-1234)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Ride Details:
───────────────────────────────────────────────────────────────
R101    Airport → City Center          ₹450.00
R102    City Center → Mall              ₹180.00
R103    Mall → Railway Station          ₹320.00
───────────────────────────────────────────────────────────────
Total Rides : 3
Total Fare  : ₹950.00
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
DRIVER #2: Priya (KA-02-5678)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Ride Details:
───────────────────────────────────────────────────────────────
R201    Tech Park → Indiranagar         ₹250.00
R202    Indiranagar → Koramangala       ₹220.00
R203    Koramangala → Airport           ₹850.00
───────────────────────────────────────────────────────────────
Total Rides : 3
Total Fare  : ₹1,320.00
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
DRIVER #3: Amit (KA-03-9012)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Ride Details:
───────────────────────────────────────────────────────────────
R301    Whitefield → MG Road             ₹380.00
R302    MG Road → Electronic City        ₹540.00
───────────────────────────────────────────────────────────────
Total Rides : 2
Total Fare  : ₹920.00
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

═══════════════════════════════════════════════════════════════
                    SYSTEM SUMMARY
═══════════════════════════════════════════════════════════════
Total Drivers      : 3
Total Rides        : 8
Total Revenue      : ₹3,190.00
Average Fare/Ride  : ₹398.75
═══════════════════════════════════════════════════════════════
```

## 💻 Class Definitions

### OLADriver Class

```csharp
public class OLADriver
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string VehicleNo { get; set; }
    public List<Ride> Rides { get; set; }
  
    public OLADriver()
    {
        Rides = new List<Ride>();
    }
}
```

### Ride Class

```csharp
public class Ride
{
    public int RideId { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal Fare { get; set; }
}
```

## 📊 Data Structure

```
┌─────────────────────────────────────────────────────────────────┐
│                    COLLECTION STRUCTURE                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   List<OLADriver> drivers = new List<OLADriver>();              │
│                                                                  │
│   drivers[0] ────┬─── OLADriver { Id=1, Name="Rahul", ... }    │
│                  │      │                                        │
│                  │      └─── List<Ride>                         │
│                  │            ├── Ride { RideId=101, ... }      │
│                  │            ├── Ride { RideId=102, ... }      │
│                  │            └── Ride { RideId=103, ... }      │
│                  │                                               │
│   drivers[1] ────┼─── OLADriver { Id=2, Name="Priya", ... }    │
│                  │      │                                        │
│                  │      └─── List<Ride>                         │
│                  │            ├── Ride { RideId=201, ... }      │
│                  │            ├── Ride { RideId=202, ... }      │
│                  │            └── Ride { RideId=203, ... }      │
│                  │                                               │
│   drivers[2] ────┴─── OLADriver { Id=3, Name="Amit", ... }     │
│                         │                                        │
│                         └─── List<Ride>                         │
│                               ├── Ride { RideId=301, ... }      │
│                               └── Ride { RideId=302, ... }      │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 🎯 Key Features Demonstrated

| Feature                         | Implementation                  | Purpose                      |
| ------------------------------- | ------------------------------- | ---------------------------- |
| **Object Composition**    | Driver contains List`<Ride>`  | One-to-many relationship     |
| **Nested Collections**    | List of Drivers with Ride lists | Hierarchical data management |
| **Aggregate Calculation** | Sum of fares per driver         | Revenue tracking             |
| **Formatted Display**     | Tabular output with separators  | Readable reporting           |

## 🚀 Usage Example

```csharp
// Calculate total fare for a driver
decimal CalculateTotalFare(OLADriver driver)
{
    decimal total = 0;
    foreach (var ride in driver.Rides)
    {
        total += ride.Fare;
    }
    return total;
}

// Display all drivers with their rides
foreach (var driver in drivers)
{
    Console.WriteLine($"Driver: {driver.Name} ({driver.VehicleNo})");
  
    foreach (var ride in driver.Rides)
    {
        Console.WriteLine($"  └─ {ride.From} → {ride.To}: ₹{ride.Fare}");
    }
  
    Console.WriteLine($"  Total Fare: ₹{CalculateTotalFare(driver)}");
    Console.WriteLine();
}
```

---

**Happy Riding!** 🚖
