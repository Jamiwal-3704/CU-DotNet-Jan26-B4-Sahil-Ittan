# Order Processing Domain

A C# console application demonstrating proper non-static class design with instance members, encapsulation, and business logic implementation.

## 📋 Overview

This project implements an `Order` class that models a real-world order processing system, showcasing fundamental object-oriented programming concepts through instance fields, properties, methods, and constructors.

## 🎯 Learning Objectives

- Understand non-static (instance) class design
- Implement proper encapsulation with private fields
- Create and use multiple constructors
- Design read-only and read/write properties with validation
- Implement instance methods with business logic
- Enforce validation rules through instance members

## 📊 Class Design

```
┌─────────────────────────────────────────────────────────────┐
│                        ORDER CLASS                           │
│                     (Non-Static Design)                      │
├─────────────────────────────────────────────────────────────┤
│                      PRIVATE FIELDS                          │
│  ┌─────────────────────────────────────────────────────┐    │
│  │  - _orderId : int                                   │    │
│  │  - _customerName : string                           │    │
│  │  - _totalAmount : decimal                           │    │
│  │  - _discountApplied : bool                          │    │
│  │  - _orderDate : DateTime                             │    │
│  │  - _status : string                                  │    │
│  └─────────────────────────────────────────────────────┘    │
│                                                              │
│                      PUBLIC PROPERTIES                       │
│  ┌─────────────────────────────────────────────────────┐    │
│  │  + OrderId : int          (Read-only)               │    │
│  │  + CustomerName : string   (Read/Write with validation)│    │
│  │  + TotalAmount : decimal   (Read-only)               │    │
│  │  + Status : string         (Read-only)               │    │
│  │  + OrderDate : DateTime    (Read-only)               │    │
│  └─────────────────────────────────────────────────────┘    │
│                                                              │
│                      CONSTRUCTORS                            │
│  ┌─────────────────────────────────────────────────────┐    │
│  │  + Order()                     (Default)            │    │
│  │  + Order(int orderId, string customerName)          │    │
│  └─────────────────────────────────────────────────────┘    │
│                                                              │
│                      INSTANCE METHODS                        │
│  ┌─────────────────────────────────────────────────────┐    │
│  │  + AddItem(decimal price) : void                    │    │
│  │  + ApplyDiscount(decimal percentage) : bool         │    │
│  │  + GetOrderSummary() : string                       │    │
│  │  - ValidateCustomerName() : void    (private)       │    │
│  └─────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────┘
```

## 🔧 Key Features

### 1. **Encapsulated Fields**
```csharp
private int _orderId;
private string _customerName;
private decimal _totalAmount;
private bool _discountApplied;
private DateTime _orderDate;
private string _status;
```

### 2. **Controlled Property Access**
- **Read-only Properties**: OrderId, TotalAmount, Status, OrderDate
- **Validated Property**: CustomerName (rejects null/empty)
- **Internal Updates**: TotalAmount modified only through methods

### 3. **Multiple Constructors**
- **Default Constructor**: Sets current date and "NEW" status
- **Parameterized Constructor**: Initializes with order ID and customer name

### 4. **Business Logic Methods**
- **AddItem()**: Accumulates order total
- **ApplyDiscount()**: Applies percentage discount (1-30%) only once
- **GetOrderSummary()**: Returns formatted order information

## 📈 Program Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                      PROGRAM EXECUTION FLOW                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│   START                                                          │
│     │                                                            │
│     ▼                                                            │
│   Create Order Objects                                          │
│     ├──► Order default1 = new Order();                          │
│     │       (Date = Today, Status = "NEW", Total = 0)           │
│     │                                                            │
│     └──► Order order1 = new Order(101, "Rahul");                │
│             (OrderId = 101, Customer = "Rahul", Status = "NEW") │
│                                                                  │
│     ▼                                                            │
│   Add Items to Order                                            │
│     └──► order1.AddItem(500);                                   │
│          order1.AddItem(300);                                    │
│          (Total = 800)                                           │
│                                                                  │
│     ▼                                                            │
│   Apply Discount                                                │
│     └──► order1.ApplyDiscount(10);                              │
│          (Total = 720, Discount Applied = true)                  │
│                                                                  │
│     ▼                                                            │
│   Display Order Summary                                         │
│     └──► Console.WriteLine(order1.GetOrderSummary());           │
│                                                                  │
│     ▼                                                            │
│   END                                                            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## 📝 Sample Output

```
========== ORDER PROCESSING SYSTEM ==========

Order #101 Details:
──────────────────────────────
Order Id: 101
Customer: Rahul
Total Amount: ₹720
Status: NEW
Order Date: 15-Mar-2024

========== ADDITIONAL ORDERS ==========

Default Order:
──────────────────────────────
Order Id: 0
Customer: Guest
Total Amount: ₹0
Status: NEW
Order Date: 15-Mar-2024

Order #102 Details:
──────────────────────────────
Order Id: 102
Customer: Priya
Total Amount: ₹1250
Status: NEW

=========================================
```

## 🧪 Validation Rules

| Rule | Enforcement |
|------|------------|
| **Customer Name** | Cannot be null, empty, or whitespace |
| **Total Amount** | Cannot be negative (enforced through AddItem) |
| **Discount** | Only one discount per order (1-30% range) |
| **Order ID** | Read-only, set through constructor only |
| **Order Status** | Initialized to "NEW", can be extended |

## 💡 Instance Members Explained

### Constructors
```csharp
// Default Constructor - Sets default values
public Order()
{
    _orderDate = DateTime.Now;
    _status = "NEW";
    _totalAmount = 0;
    _discountApplied = false;
}

// Parameterized Constructor - Custom initialization
public Order(int orderId, string customerName)
{
    _orderId = orderId;
    _customerName = customerName;
    _orderDate = DateTime.Now;
    _status = "NEW";
    _totalAmount = 0;
    _discountApplied = false;
}
```

### Properties with Logic
```csharp
public string CustomerName
{
    get { return _customerName; }
    set 
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Customer name cannot be empty");
        _customerName = value;
    }
}

public decimal TotalAmount
{
    get { return _totalAmount; }
    // No set - read-only externally
}
```

### Business Methods
```csharp
public void AddItem(decimal price)
{
    if (price > 0)
        _totalAmount += price;
}

public bool ApplyDiscount(decimal percentage)
{
    if (_discountApplied) return false;
    if (percentage < 1 || percentage > 30) return false;
    
    _totalAmount -= (_totalAmount * percentage / 100);
    _discountApplied = true;
    return true;
}
```

## 🎓 Educational Value

This project demonstrates:

1. **Non-Static Class Design**
   - Instance members belong to objects, not the class
   - Each order object maintains its own state

2. **Proper Encapsulation**
   - Private fields hide internal data
   - Properties control access with validation

3. **Constructor Overloading**
   - Multiple ways to initialize objects
   - Default vs. parameterized constructors

4. **Instance Methods**
   - Methods operate on object-specific data
   - Business logic encapsulated within the class

5. **Real-World Modeling**
   - Order processing domain concepts
   - Business rules and validations

## 🚀 Getting Started

### Prerequisites
- .NET 6.0 SDK or later
- Any C# IDE (Visual Studio, VS Code, Rider)

### Running the Application

1. **Create a new Console Application**
   ```bash
   dotnet new console -n OrderProcessing
   cd OrderProcessing
   ```

2. **Implement the Order class** as designed above

3. **In Main() method, demonstrate:**
   - Creating multiple order objects
   - Using both constructors
   - Calling instance methods
   - Displaying order summaries

4. **Build and Run**
   ```bash
   dotnet build
   dotnet run
   ```

## 📊 Class Interaction Diagram

```
┌──────────────────┐         ┌──────────────────┐
│      Main()      │         │      Order       │
├──────────────────┤         ├──────────────────┤
│ Create Objects   │───────┬>│ Instance 1       │
│ Call Methods     │       │ │ - ID: 101        │
│ Display Results  │       │ │ - Customer: Rahul│
└──────────────────┘       │ │ - Total: 720     │
                           │ └──────────────────┘
                           │
                           │ ┌──────────────────┐
                           └>│ Instance 2       │
                             │ - ID: 102        │
                             │ - Customer: Priya│
                             │ - Total: 1250    │
                             └──────────────────┘
```

## 🔍 Common Use Cases

- **E-commerce Platforms**: Process customer orders
- **Inventory Systems**: Track order values
- **Billing Applications**: Calculate totals with discounts
- **Reporting Systems**: Generate order summaries

## ✅ Best Practices Demonstrated

- ✓ **Encapsulation**: Private fields with property access
- ✓ **Validation**: Input checking in properties
- ✓ **Single Responsibility**: Each method does one thing
- ✓ **State Management**: Track discount application
- ✓ **Readability**: Clear naming conventions
- ✓ **Immutability**: Read-only properties where appropriate

---

## 📝 Note

This implementation focuses on **non-static class design** principles. Each order object maintains its own independent state, and all operations are performed through instance members, demonstrating proper object-oriented design in C#.

**Happy Coding!** 🚀
