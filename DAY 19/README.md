🚀 C# Object-Oriented Programming Projects

This repository contains two independent C# console applications built to demonstrate core Object-Oriented Programming (OOP) concepts, real-world problem modeling, and clean extensible design.

Both projects focus on abstraction, inheritance, method overriding, runtime polymorphism, and SOLID principles.

📌 Project 1: Eco-Drive Vehicle Simulation
🧠 Problem Overview

A logistics company operates multiple vehicle types, each with unique movement and fuel-handling behavior.
The goal is to design a vehicle simulation engine where a central controller can operate all vehicles without knowing their concrete types.

🎯 Key Objectives

Enforce a common vehicle structure

Allow each vehicle to define its own movement logic

Demonstrate runtime method selection

Avoid if / switch checks for vehicle type

🏗️ Design Approach

Abstract Base Class: Vehicle

Derived Classes:

ElectricCar

HeavyTruck

CargoPlane

Uses abstract methods, virtual methods, and the base keyword

🧩 Core Features

Move() → Abstract method (mandatory override)

GetFuelStatus() → Virtual method (optional override)

Runtime polymorphism via Vehicle collection

Extensible design (new vehicle types can be added easily)

🧪 Sample Output
Tesla Model X is gliding silently on battery power.
Tesla Model X battery is at 80%.

Volvo FH16 is hauling cargo with high-torque diesel power.
Fuel level is stable.

Boeing 747 Freighter is ascending to 30,000 feet.
Fuel level is stable. Checking jet fuel reserves...

🛠️ Concepts Used

Abstract classes

Method overriding

Virtual methods

Runtime polymorphism

Open/Closed Principle

📌 Project 2: Utility Billing System – Tariff Calculation Engine
🧠 Problem Overview

A city corporation needs a utility billing system to calculate monthly bills for different utility connections such as electricity, water, and gas.

Each utility follows:

Common billing structure

Different calculation and tax rules

The system must be extensible without modifying existing code.

🎯 Key Objectives

Centralize billing flow

Allow utility-specific calculations

Implement a template-style billing algorithm

Support future utility types

🏗️ Design Approach

Abstract Base Class: UtilityBill

Derived Classes:

ElectricityBill

WaterBill

GasBill

Uses a Template Method Pattern

🧩 Billing Rules Implemented
Utility	Special Rule	Tax Rule
Electricity	10% surcharge if units > 300	Default 5%
Water	No surcharge	2% tax
Gas	₹150 fixed monthly charge	No tax
🔄 Billing Flow

Calculate base bill amount

Apply utility-specific tax

Display final payable amount

All steps are handled via one method call (PrintBill()).

🧪 Sample Output
Consumer ID     : 101
Consumer Name   : Amit Sharma
Units Consumed  : 350
Bill Amount     : ₹2502.50
Tax             : ₹125.12
Total Payable   : ₹2627.62

🛠️ Concepts Used

Abstract classes

Abstract methods

Virtual methods

Method overriding

Runtime polymorphism

Template Method Pattern

Open/Closed Principle (OCP)

🧠 Learning Outcomes

✔ Strong understanding of OOP fundamentals
✔ Real-world problem modeling
✔ Clean and extensible code design
✔ Interview-ready architecture patterns

🚀 How to Run

Open the project in Visual Studio

Build the solution

Run the console application

📌 Future Enhancements

Add Factory Pattern

Introduce unit testing

Convert to .NET Web API

Add configuration-based billing rates

👨‍💻 Author

Built as part of hands-on C# OOP learning and practice.