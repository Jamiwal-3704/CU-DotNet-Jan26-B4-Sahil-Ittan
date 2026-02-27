# Cricket Player Performance Tracker

A C# console application that processes cricket player statistics from a CSV file, calculates performance metrics, and displays sorted results.

## 📋 Overview

This tool reads player data from a CSV file, calculates strike rates and batting averages, handles data irregularities, and outputs a sorted list of players based on their strike rate.

## 🏗️ System Architecture

```
┌─────────────────────────────────────────────────────────────┐
│              CRICKET PERFORMANCE TRACKER                     │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  ┌─────────────┐                                            │
│  │   players.csv│                                           │
│  │  (Input File)│                                           │
│  └──────┬──────┘                                            │
│         │                                                    │
│         ▼                                                    │
│  ┌─────────────────────────────────────────────────────┐   │
│  │              FILE PROCESSING ENGINE                   │   │
│  ├─────────────────────────────────────────────────────┤   │
│  │  • Read line by line                                 │   │
│  │  • Split by commas                                    │   │
│  │  • Parse data types                                   │   │
│  │  • Handle exceptions                                  │   │
│  └────────────────────┬────────────────────────────────┘   │
│                       │                                      │
│                       ▼                                      │
│  ┌─────────────────────────────────────────────────────┐   │
│  │              PLAYER OBJECT CREATION                   │   │
│  ├─────────────────────────────────────────────────────┤   │
│  │  Name: string                                        │   │
│  │  RunsScored: int                                     │   │
│  │  BallsFaced: int                                     │   │
│  │  IsOut: bool                                         │   │
│  │  StrikeRate: double (calculated)                     │   │
│  │  Average: double (calculated)                        │   │
│  └────────────────────┬────────────────────────────────┘   │
│                       │                                      │
│                       ▼                                      │
│  ┌─────────────────────────────────────────────────────┐   │
│  │              DATA PROCESSING PIPELINE                 │   │
│  ├─────────────────────────────────────────────────────┤   │
│  │                                                      │   │
│  │  ┌──────────────┐    ┌──────────────┐              │   │
│  │  │  Filter      │───▶│    Sort      │              │   │
│  │  │  Balls ≥ 10  │    │  by SR Desc  │              │   │
│  │  └──────────────┘    └──────────────┘              │   │
│  │                           │                          │   │
│  │                           ▼                          │   │
│  │                    ┌──────────────┐                 │   │
│  │                    │   Display    │                 │   │
│  │                    │  Formatted   │                 │   │
│  │                    │    Table     │                 │   │
│  │                    └──────────────┘                 │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

## 📊 Player Class Structure

```csharp
public class Player
{
    public string Name { get; set; }
    public int RunsScored { get; set; }
    public int BallsFaced { get; set; }
    public bool IsOut { get; set; }
    public double StrikeRate { get; set; }
    public double Average { get; set; }
}
```

## 🧮 Calculation Formulas

| Metric                    | Formula                                                                         | Example                           |
| ------------------------- | ------------------------------------------------------------------------------- | --------------------------------- |
| **Strike Rate**     | `(RunsScored / BallsFaced) × 100`                                            | 84 runs / 90 balls × 100 = 93.33 |
| **Batting Average** | If `IsOut = true`: `RunsScored / 1<br>`If `IsOut = false`: `RunsScored` | 84 runs, Out → Avg = 84.00       |

## 🔄 Processing Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    DATA PROCESSING PIPELINE                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   RAW CSV DATA                                                   │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │ Steve Smith,84,90,True                                  │   │
│   │ Virat Kohli,29,35,False                                 │   │
│   │ Joe Root,110,120,True                                   │   │
│   │ David Warner,12,5,True   (❌ Balls < 10)               │   │
│   │ Ben Stokes,invalid,50,True (❌ Format error)            │   │
│   └─────────────────────────────────────────────────────────┘   │
│                           │                                       │
│                           ▼                                       │
│   ╔═══════════════════════════════════════════════════════════╗   │
│   ║                 EXCEPTION HANDLING                        ║   │
│   ╠═══════════════════════════════════════════════════════════╣   │
│   ║  • FileNotFoundException  → "File not found"             ║   │
│   ║  • FormatException        → "Invalid number format"      ║   │
│   ║  • DivideByZeroException  → Handle BallsFaced = 0        ║   │
│   ╚═══════════════════════════════════════════════════════════╝   │
│                           │                                       │
│                           ▼                                       │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │              PLAYER OBJECTS CREATED                       │   │
│   ├─────────────────────────────────────────────────────────┤   │
│   │ 1. Steve Smith | Runs:84 | Balls:90 | Out:Yes           │   │
│   │    → SR: 93.33 | Avg: 84.00                              │   │
│   │                                                          │   │
│   │ 2. Virat Kohli | Runs:29 | Balls:35 | Out:No            │   │
│   │    → SR: 82.86 | Avg: 29.00                              │   │
│   │                                                          │   │
│   │ 3. Joe Root | Runs:110 | Balls:120 | Out:Yes            │   │
│   │    → SR: 91.67 | Avg: 110.00                             │   │
│   └─────────────────────────────────────────────────────────┘   │
│                           │                                       │
│                           ▼                                       │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │              FILTER (Balls ≥ 10)                         │   │
│   ├─────────────────────────────────────────────────────────┤   │
│   │  ✓ Steve Smith (90 balls)                                │   │
│   │  ✓ Virat Kohli (35 balls)                                │   │
│   │  ✓ Joe Root (120 balls)                                  │   │
│   │  ✗ David Warner (5 balls) → REMOVED                      │   │
│   └─────────────────────────────────────────────────────────┘   │
│                           │                                       │
│                           ▼                                       │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │              SORT (by Strike Rate DESC)                  │   │
│   ├─────────────────────────────────────────────────────────┤   │
│   │  1. Steve Smith  (93.33)                                │   │
│   │  2. Joe Root     (91.67)                                │   │
│   │  3. Virat Kohli  (82.86)                                │   │
│   └─────────────────────────────────────────────────────────┘   │
│                           │                                       │
│                           ▼                                       │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │              FORMATTED TABLE OUTPUT                      │   │
│   └─────────────────────────────────────────────────────────┘   │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📝 Sample Output

```
═══════════════════════════════════════════════════════════════
         CRICKET PLAYER PERFORMANCE TRACKER
═══════════════════════════════════════════════════════════════

Enter CSV file path: players.csv

───────────────────────────────────────────────────────────────
PROCESSING FILE: players.csv
───────────────────────────────────────────────────────────────
✓ Read 5 records from file
⚠ Warning: Skipped invalid data at line 4: David Warner,12,5,True (Balls < 10)
⚠ Warning: Skipped invalid data at line 5: Ben Stokes,invalid,50,True (Invalid runs)

✅ Successfully processed 3 players
───────────────────────────────────────────────────────────────

═══════════════════════════════════════════════════════════════
                  PERFORMANCE SUMMARY
═══════════════════════════════════════════════════════════════

Name            Runs     SR     Avg   
────────────────────────────────────────
Steve Smith     84      93.33   84.00
Joe Root        110     91.67   110.00
Virat Kohli     29      82.86   29.00
────────────────────────────────────────

═══════════════════════════════════════════════════════════════
              END OF REPORT
═══════════════════════════════════════════════════════════════
```

## ⚠️ Exception Handling Scenarios

| Exception                          | Cause                  | Handling                                                |
| ---------------------------------- | ---------------------- | ------------------------------------------------------- |
| **FileNotFoundException**    | CSV file missing       | Display "Error: File not found. Please check the path." |
| **FormatException**          | Non-numeric runs/balls | Skip line, log warning, continue processing             |
| **DivideByZeroException**    | BallsFaced = 0         | Set StrikeRate = 0, continue processing                 |
| **IndexOutOfRangeException** | Malformed CSV line     | Skip line, log warning                                  |

## 🎯 Key Features Demonstrated

| Feature                      | Implementation                            | Benefit                 |
| ---------------------------- | ----------------------------------------- | ----------------------- |
| **File I/O**           | `StreamReader` or `File.ReadAllLines` | Read external data      |
| **Exception Handling** | Try-catch blocks                          | Graceful error recovery |
| **Data Validation**    | Filter BallsFaced ≥ 10                   | Remove invalid entries  |
| **Calculated Fields**  | StrikeRate, Average                       | Derived metrics         |
| **Sorting**            | `List.Sort` with Comparison             | Custom ordering         |
| **Formatted Output**   | Console table with padding                | Readable display        |

## 🔧 Usage Instructions

1. **Create CSV file** (players.csv):

```
Steve Smith,84,90,True
Virat Kohli,29,35,False
Joe Root,110,120,True
David Warner,12,5,True
Ben Stokes,invalid,50,True
```

2. **Run application**
3. **Enter file path** when prompted
4. **View results** in formatted table

---

**Happy Cricket Analytics!** 🏏
