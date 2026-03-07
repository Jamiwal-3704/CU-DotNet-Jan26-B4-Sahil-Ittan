# SkyHigh Flight Aggregator

A C# flight search engine demonstrating sorting strategies using `IComparable<T>` and `IComparer<T>` interfaces.

## 📋 Overview

This flight aggregator system receives unsorted flight data from various airlines and provides three distinct sorting views: by price (default), by duration, and by departure time.

## 🏗️ Class Structure

```
┌─────────────────────────────────────────────────────────────┐
│                      FLIGHT CLASS                            │
│                    (IComparable<Flight>)                     │
├─────────────────────────────────────────────────────────────┤
│  + FlightNumber : string                                     │
│  + Price : decimal                                           │
│  + Duration : TimeSpan                                       │
│  + DepartureTime : DateTime                                  │
├─────────────────────────────────────────────────────────────┤
│  + CompareTo(Flight other) : int  ←─── DEFAULT SORT BY PRICE│
│  + ToString() : string                                       │
└─────────────────────────────────────────────────────────────┘
                            │
                            │ Uses
                            ▼
┌─────────────────────────────────────────────────────────────┐
│                    COMPARER CLASSES                          │
│                   (IComparer<Flight>)                        │
├─────────────────────────────────────────────────────────────┤
│  ┌─────────────────────┐    ┌─────────────────────────────┐ │
│  │ DURATIONCOMPARER    │    │ DEPARTURECOMPARER           │ │
│  ├─────────────────────┤    ├─────────────────────────────┤ │
│  │ + Compare(Flight x, │    │ + Compare(Flight x,         │ │
│  │   Flight y) : int   │    │   Flight y) : int           │ │
│  │   (by Duration)     │    │   (by DepartureTime)        │ │
│  └─────────────────────┘    └─────────────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
```

## ✈️ Flight Sorting Strategies

| View Type                      | Sort Criteria                   | Implementation        |
| ------------------------------ | ------------------------------- | --------------------- |
| **Economy View**         | Price (Ascending)               | Default (IComparable) |
| **Business Runner View** | Duration (Shortest first)       | DurationComparer      |
| **Early Bird View**      | Departure Time (Earliest first) | DepartureComparer     |

## 🔄 Sorting Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    SORTING EXECUTION FLOW                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   UNSORTED FLIGHT LIST                                           │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │ • Flight A: ₹12,500 | 2.5h | 08:30                      │   │
│   │ • Flight B: ₹8,900  | 4.0h | 06:15                      │   │
│   │ • Flight C: ₹15,200 | 1.8h | 10:45                      │   │
│   │ • Flight D: ₹8,900  | 3.2h | 07:30                      │   │
│   └─────────────────────────────────────────────────────────┘   │
│                           │                                       │
│           ┌───────────────┼───────────────┐                      │
│           │               │               │                      │
│           ▼               ▼               ▼                      │
│   ┌───────────────┐ ┌───────────────┐ ┌───────────────┐          │
│   │  DEFAULT      │ │  DURATION     │ │  DEPARTURE    │          │
│   │  SORT         │ │  COMPARER     │ │  COMPARER     │          │
│   │  (Price)      │ │               │ │               │          │
│   ├───────────────┤ ├───────────────┤ ├───────────────┤          │
│   │ flights.Sort()│ │ flights.Sort( │ │ flights.Sort( │          │
│   │               │ │  new Duration │ │  new Departure│          │
│   │               │ │  Comparer())  │ │  Comparer())  │          │
│   ├───────────────┤ ├───────────────┤ ├───────────────┤          │
│   │ 1. B: ₹8,900  │ │ 1. C: 1.8h    │ │ 1. B: 06:15   │          │
│   │ 2. D: ₹8,900  │ │ 2. A: 2.5h    │ │ 2. D: 07:30   │          │
│   │ 3. A: ₹12,500 │ │ 3. D: 3.2h    │ │ 3. A: 08:30   │          │
│   │ 4. C: ₹15,200 │ │ 4. B: 4.0h    │ │ 4. C: 10:45   │          │
│   └───────────────┘ └───────────────┘ └───────────────┘          │
│                           │                                       │
│                           ▼                                       │
│               THREE DISTINCT VIEWS GENERATED                      │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📊 Sample Flight Data

```
═══════════════════════════════════════════════════════════════
                 SKYHIGH FLIGHT AGGREGATOR
═══════════════════════════════════════════════════════════════

ORIGINAL UNSORTED FLIGHT LIST:
───────────────────────────────────────────────────────────────
AI-202   ₹12,500 | 2h 30m | Depart: 08:30 (15-May-2024)
AI-101   ₹8,900  | 4h 00m | Depart: 06:15 (15-May-2024)
AI-303   ₹15,200 | 1h 45m | Depart: 10:45 (15-May-2024)
AI-404   ₹8,900  | 3h 12m | Depart: 07:30 (15-May-2024)
AI-505   ₹21,000 | 1h 30m | Depart: 14:20 (15-May-2024)
AI-606   ₹10,500 | 2h 15m | Depart: 09:45 (15-May-2024)

═══════════════════════════════════════════════════════════════
```

## 📝 Sample Output - Three Views

```
═══════════════════════════════════════════════════════════════
ECONOMY VIEW (Default Sort by Price - Ascending)
═══════════════════════════════════════════════════════════════
Rank  Flight    Price     Duration   Departure
───────────────────────────────────────────────────────────────
#1    AI-101    ₹8,900    4h 00m     06:15
#2    AI-404    ₹8,900    3h 12m     07:30
#3    AI-202    ₹12,500   2h 30m     08:30
#4    AI-606    ₹10,500   2h 15m     09:45
#5    AI-303    ₹15,200   1h 45m     10:45
#6    AI-505    ₹21,000   1h 30m     14:20
───────────────────────────────────────────────────────────────
Note: Flights with same price (₹8,900) maintain original order
═══════════════════════════════════════════════════════════════

BUSINESS RUNNER VIEW (Sort by Duration - Shortest First)
═══════════════════════════════════════════════════════════════
Rank  Flight    Duration   Price     Departure
───────────────────────────────────────────────────────────────
#1    AI-505    1h 30m     ₹21,000   14:20
#2    AI-303    1h 45m     ₹15,200   10:45
#3    AI-606    2h 15m     ₹10,500   09:45
#4    AI-202    2h 30m     ₹12,500   08:30
#5    AI-404    3h 12m     ₹8,900    07:30
#6    AI-101    4h 00m     ₹8,900    06:15
───────────────────────────────────────────────────────────────
═══════════════════════════════════════════════════════════════

EARLY BIRD VIEW (Sort by Departure Time - Earliest First)
═══════════════════════════════════════════════════════════════
Rank  Flight    Departure   Price     Duration
───────────────────────────────────────────────────────────────
#1    AI-101    06:15       ₹8,900    4h 00m
#2    AI-404    07:30       ₹8,900    3h 12m
#3    AI-202    08:30       ₹12,500   2h 30m
#4    AI-606    09:45       ₹10,500   2h 15m
#5    AI-303    10:45       ₹15,200   1h 45m
#6    AI-505    14:20       ₹21,000   1h 30m
───────────────────────────────────────────────────────────────
═══════════════════════════════════════════════════════════════
```

## 🧠 Interface Implementation Details

### 1. IComparable`<T>` - Default Sorting by Price

```csharp
public class Flight : IComparable<Flight>
{
    // Properties...
  
    public int CompareTo(Flight other)
    {
        // Null handling
        if (other == null) return 1;
      
        // Default sort by Price (ascending)
        return this.Price.CompareTo(other.Price);
    }
}
```

### 2. IComparer`<T>` - Custom Sorting Strategies

```csharp
// Sort by Duration (shortest first)
public class DurationComparer : IComparer<Flight>
{
    public int Compare(Flight x, Flight y)
    {
        // Null handling
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
      
        // Compare by duration
        return x.Duration.CompareTo(y.Duration);
    }
}

// Sort by Departure Time (earliest first)
public class DepartureComparer : IComparer<Flight>
{
    public int Compare(Flight x, Flight y)
    {
        // Null handling
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
      
        // Compare by departure time
        return x.DepartureTime.CompareTo(y.DepartureTime);
    }
}
```

## 🔍 Comparison Logic Matrix

| Scenario            | IComparable.CompareTo() | IComparer.Compare() |
| ------------------- | ----------------------- | ------------------- |
| **x < y**     | Returns negative        | Returns negative    |
| **x == y**    | Returns 0               | Returns 0           |
| **x > y**     | Returns positive        | Returns positive    |
| **x is null** | N/A (x is 'this')       | Returns -1          |
| **y is null** | Returns 1               | Returns 1           |
| **Both null** | N/A                     | Returns 0           |

## 🛠️ Usage Examples

```csharp
// Create flight list
List<Flight> flights = GetFlights();

// Default sort by Price (IComparable)
flights.Sort();
DisplayFlights("ECONOMY VIEW", flights);

// Sort by Duration (custom comparer)
flights.Sort(new DurationComparer());
DisplayFlights("BUSINESS RUNNER VIEW", flights);

// Sort by Departure Time (custom comparer)
flights.Sort(new DepartureComparer());
DisplayFlights("EARLY BIRD VIEW", flights);
```

## ✅ Key Takeaways

| Concept                      | Implementation                      | Benefit                       |
| ---------------------------- | ----------------------------------- | ----------------------------- |
| **IComparable`<T>`** | Flight class implements CompareTo() | Enables default sort by price |
| **IComparer`<T>`**   | Separate comparer classes           | Multiple sorting strategies   |
| **Null handling**      | Check for null parameters           | Prevents runtime crashes      |
| **List.Sort()**        | Overloaded method                   | Flexible sorting options      |

---

**Happy Flying!** ✈️
