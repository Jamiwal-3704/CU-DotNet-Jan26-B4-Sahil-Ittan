# 🏢 Company Hierarchy - Organizational Tree Structure

## 📋 Project Overview

A C# console application that models and visualizes a company's organizational structure using a tree data structure. This project demonstrates hierarchical relationships between employees, from the CEO down to individual contributors.

## 🎯 Purpose

The application creates a visual representation of a company's reporting structure, showing:

* Who reports to whom within the organization
* The chain of command from top to bottom
* Each employee's position and level in the hierarchy

## 🌳 Data Structure: Tree

This project implements a **Tree Data Structure** where:

* **Root Node** : The CEO (top of organization)
* **Parent Nodes** : Managers and leads who have direct reports
* **Child Nodes** : Employees who report to someone
* **Leaf Nodes** : Employees with no direct reports

### Tree Structure Visualized

**text**

```
CEO (Jordan Smith)
    └── CTO (Alex Chen)
        └── Dev Manager (Sarah Vane)
            ├── Senior Dev (Leo G.)
            └── Junior Dev (Maya R.)
```

## 🏗️ Architecture Components

### 1. EmployeeNode Class (Tree Node)

Each employee in the organization is represented as a node containing:

* **Name** : Employee's full name
* **Position** : Job title/role in company
* **Reports** : List of direct reports (children nodes)

 **Purpose** : Serves as the building block for the entire organizational tree.

### 2. OrganizationTree Class (Tree Manager)

Manages the overall structure and provides:

* **Root Reference** : Points to the top of organization (CEO)
* **Display Method** : Public interface to show the structure
* **Recursive Printing** : Private method that traverses the tree

 **Purpose** : Controls access to the tree and handles visualization.

## 🔄 How It Works

### Step 1: Creating the Structure

The application first creates individual employee nodes:

* CEO (Jordan Smith) - Top of organization
* CTO (Alex Chen) - Reports to CEO
* Dev Manager (Sarah Vane) - Reports to CTO
* Developers (Leo G. and Maya R.) - Report to Manager
* CFO (Sahil Ittan) - Reports to CEO

### Step 2: Building Relationships

Each employee is linked to their manager through the `AddReport()` method:

* CEO adds CTO and CFO as direct reports
* CTO adds Dev Manager as a direct report
* Dev Manager adds both developers as direct reports

### Step 3: Displaying the Hierarchy

The `Display()` method triggers a recursive traversal:

1. Starts at the CEO (root)
2. Prints the current employee with proper indentation
3. Recursively calls itself for each direct report
4. Increases indentation level for each hierarchy level

## 📊 Sample Output

When executed, the program produces:

**text**

```
ORGANIZATION STRUCTURE
======================
Jordan Smith (CEO)
    └── Alex Chen (CTO)
        └── Sarah Vane (Dev Manager)
            └── Leo G. (Senior Dev)
            └── Maya R. (Junior Dev)
    └── Sahil Ittan (CFO)

Press any key to exit...
```

## 🎓 Key Concepts Demonstrated

### 1. Recursion

The `PrintRecursive` method calls itself to traverse the tree:

* **Base Case** : Implicitly handled when node has no reports
* **Recursive Case** : For each report, call the method again with increased depth

### 2. Tree Traversal

Uses **Depth-First Traversal** to navigate the hierarchy:

* Visits parent node first
* Then explores each child branch completely before moving to next sibling

### 3. Object Composition

* EmployeeNode objects contain references to other EmployeeNode objects
* Creates parent-child relationships through object references

### 4. Encapsulation

* Public `Display()` method provides controlled access
* Private `PrintRecursive()` handles internal implementation details
* Prevents external code from disrupting the recursive logic

## 💼 Real-World Application

This project mimics real-world HR and organizational tools:

* **Company Directories** : Visualizing reporting structures
* **Org Charts** : Creating visual representations of companies
* **Succession Planning** : Understanding management chains
* **Team Building** : Identifying teams and their leads

## 🔍 Interesting Observation

The code contains a deliberate logical error:

**csharp**

```
cfo.AddReport(cfo);  // CFO reporting to themselves!
```

This creates a self-loop in the tree structure, which doesn't affect the output but demonstrates an important concept about data integrity in hierarchical structures.

## 🚀 Potential Enhancements

1. **Input Validation** : Prevent self-reporting
2. **Search Functionality** : Find employees by name or position
3. **Tree Statistics** : Calculate depth, width, number of employees
4. **Export Feature** : Save org chart to file or Excel
5. **Multiple Root Support** : Handle organizations with multiple top-level leaders
6. **Employee Details** : Add more fields (salary, hire date, etc.)
7. **Interactive Navigation** : Allow user to explore different branches

## 📚 Learning Outcomes

This project teaches:

* **Data Structures** : Tree implementation in C#
* **Recursion** : Understanding recursive method calls
* **Object-Oriented Design** : Class relationships and responsibilities
* **Algorithm Visualization** : Seeing data structures in action
* **Problem Solving** : Modeling real-world hierarchies
