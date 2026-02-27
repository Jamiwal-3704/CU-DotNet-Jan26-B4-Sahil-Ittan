# Height Calculator

A C# program that demonstrates object-oriented programming concepts by representing and adding heights of two people.

## 📋 Overview

This program implements a `Height` class that models a person's height in feet and inches, showcasing fundamental OOP principles including encapsulation, constructor overloading, and method overriding.

## ✨ Features

- **Height Representation**: Stores height in feet (integer) and inches (double)
- **Multiple Constructors**: 
  - Default constructor (0 feet 0.0 inches)
  - Parameterized constructor for custom values
- **Height Addition**: Adds two height objects with automatic normalization (12 inches = 1 foot)
- **Custom Formatting**: Displays heights in "Height - X feet Y inches" format

## 🎯 Learning Objectives

This project demonstrates:
- Class design with properties and methods
- Constructor overloading
- Encapsulation with private fields
- Method overriding (ToString())
- Object interaction and manipulation
- Input validation and data normalization

## 📊 Program Flow

```
┌─────────────────────────────────────────────────────────────┐
│                    PROGRAM EXECUTION FLOW                    │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│   START                                                      │
│     │                                                        │
│     ▼                                                        │
│   Create Person 1                     ┌─────────────────┐   │
│   (5 feet 6.5 inches) ──────────────> │  Height Object  │   │
│                                        │  - Feet: 5      │   │
│     ▼                                  │  - Inches: 6.5  │   │
│   Create Person 2                      └─────────────────┘   │
│   (5 feet 7.5 inches) ──────────────> ┌─────────────────┐   │
│                                        │  Height Object  │   │
│                                        │  - Feet: 5      │   │
│     ▼                                  │  - Inches: 7.5  │   │
│   Display Individual Heights           └─────────────────┘   │
│     │                                                        │
│     ▼                                                        │
│   Add Heights (Person1 + Person2)                           │
│     │                                                        │
│     ▼                                                        │
│   Normalize Result (if inches ≥ 12)                         │
│     │                                                        │
│     ▼                                                        │
│   Display Combined Height                                    │
│     │                                                        │
│     ▼                                                        │
│   END                                                        │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

## 🔧 How It Works

### Class Structure

The `Height` class encapsulates height data with:
- **Private fields**: `_feet` (int) and `_inches` (double)
- **Public properties**: Controlled access with validation
- **Constructors**: Multiple ways to initialize objects
- **AddHeights() method**: Combines two height objects
- **Normalize() method**: Converts excess inches to feet
- **ToString() override**: Custom string output

### Height Addition Logic

```
┌─────────────────────────────────────────────────────────┐
│                   ADDITION PROCESS                       │
├─────────────────────────────────────────────────────────┤
│                                                          │
│   Person 1: 5 feet 6.5 inches                            │
│   Person 2: 5 feet 7.5 inches                            │
│              │          │                                 │
│              ▼          ▼                                 │
│         Feet: 5+5 = 10                                    │
│         Inches: 6.5+7.5 = 14.0                            │
│              │                                            │
│              ▼                                            │
│   Raw Result: 10 feet 14.0 inches                         │
│              │                                            │
│              ▼                                            │
│   NORMALIZATION:                                          │
│   14.0 inches ÷ 12 = 1 foot remainder 2.0 inches         │
│              │                                            │
│              ▼                                            │
│   FINAL RESULT: 11 feet 2.0 inches                        │
│                                                          │
└─────────────────────────────────────────────────────────┘
```

## 📝 Sample Output

```
=== Height Calculator ===

Individual Heights:
Height - 5 feet 6.5 inches
Height - 5 feet 7.5 inches

Combined Height:
Height - 11 feet 2.0 inches

=== Program Completed ===
```

## 🧩 Key Concepts Demonstrated

| Concept | Implementation | Purpose |
|---------|---------------|---------|
| **Encapsulation** | Private fields with public properties | Data hiding and validation |
| **Constructor Overloading** | Default and parameterized constructors | Flexible object creation |
| **Method Overriding** | ToString() override | Custom string representation |
| **Object Interaction** | AddHeights(Height h2) | Object collaboration |
| **Data Normalization** | Convert inches ≥ 12 to feet | Maintains data integrity |
| **Property Validation** | Non-negative value checks | Prevents invalid states |

## 🎓 Educational Value

This program is ideal for learning:
- **Object-Oriented Programming fundamentals**
- **Class design best practices**
- **C# syntax and features**
- **Real-world data representation**
- **Mathematical operations on objects**

## 💡 Real-World Applications

- Health and fitness applications
- Medical record systems
- Anthropometric data collection
- Growth tracking applications
- Sports and athletics programs

## 🚀 Getting Started

### Prerequisites
- .NET SDK (any recent version)
- Any C# IDE (Visual Studio, VS Code, Rider)

### Running the Program
1. Create a new Console Application
2. Copy the Height class and Program class
3. Build and run the application
4. View the output showing individual and combined heights

## 📊 Class Diagram

```
┌─────────────────────────────┐
│           Height            │
├─────────────────────────────┤
│ - feet : int                │
│ - inches : double           │
├─────────────────────────────┤
│ + Feet : int                │
│ + Inches : double           │
├─────────────────────────────┤
│ + Height()                  │
│ + Height(int, double)       │
│ + AddHeights(Height)        │
│ + ToString() : string       │
│ - Normalize() : void        │
└─────────────────────────────┘
```

## ✅ Validation Rules

- **Feet**: Cannot be negative
- **Inches**: Cannot be negative
- **Normalization**: Automatically converts 12+ inches to feet
- **Format**: Always displays as "X feet Y.Y inches"

## 🔍 Common Use Cases

1. **Medical Records**: Track patient heights
2. **Fitness Apps**: Log user measurements
3. **Sports Analytics**: Compare athlete heights
4. **Growth Charts**: Monitor child development
5. **Statistical Analysis**: Height data aggregation

---

## 📝 Note

This program is designed for educational purposes to demonstrate C# and OOP concepts. The height addition feature is particularly useful in scenarios where you need to combine measurements, such as calculating total height when stacking objects or combining measurements in construction.

**Happy Coding!** 🚀
