# 📊 Assignment: The Loan Portfolio Manager

---

## 🎯 Objective

This assignment focuses on **file handling, data persistence, and business-rule evaluation** in a real-world financial scenario.

You will conceptually design a system that:

* Stores loan data in a **CSV file**
* Reads loan records back into the application
* Calculates financial values
* Classifies loans based on **risk level**

This simulates how backend systems manage  **financial portfolios** .

---

## 🧠 Core Concepts Covered

* Object-based data modeling
* CSV file format and parsing
* Writing and reading structured data
* Append vs overwrite file behavior
* Defensive programming (data safety)
* Business logic classification
* Calculated (derived) fields

---

## 🧩 Problem Overview

You are managing a **loan portfolio** for multiple clients.

Each loan contains:

* Client name
* Principal amount
* Interest rate

The system must:

1. Save loan data to a CSV file
2. Read the file back into memory
3. Calculate interest values
4. Identify **loan risk levels**
5. Display results in a clear, readable format

---

## 📌 Step 1 – Defining the Data Structure

### 🔹 Purpose

A structured data model ensures:

* Consistency
* Easy storage
* Easy parsing
* Clear business logic

Each **Loan** represents a single financial agreement.

---

### 🔹 Logical Representation

| Field         | Meaning                 |
| ------------- | ----------------------- |
| Client Name   | Borrower’s name        |
| Principal     | Loan amount             |
| Interest Rate | Percentage rate applied |

---

### 🔄 Object Concept Flow

<pre class="overflow-visible! px-0!" data-start="1861" data-end="1968"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Loan Data Entered</span><br/><span>     ↓</span><br/><span>Loan Object Created</span><br/><span>     ↓</span><br/><span>Stored in Collection</span><br/><span>     ↓</span><br/><span>Written to CSV File</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Step 2 – Writing Loan Data to CSV

### 🔹 Purpose

Persist loan records so data:

* Survives program restarts
* Can be shared or opened in Excel
* Can be reprocessed later

---

### 🔹 CSV Structure Concept

<pre class="overflow-visible! px-0!" data-start="2188" data-end="2254"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Header Row</span><br/><span>↓</span><br/><span>Loan Record 1</span><br/><span>Loan Record 2</span><br/><span>Loan Record 3</span><br/><span>...</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

### 🔹 Why a Header Row Is Important

* Makes the file **human-readable**
* Helps with documentation
* Must be **skipped during processing** to avoid parsing errors

---

### 🔄 CSV Writing Flow

<pre class="overflow-visible! px-0!" data-start="2457" data-end="2576"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Start Program</span><br/><span>     ↓</span><br/><span>Create / Open CSV File</span><br/><span>     ↓</span><br/><span>Write Header Row</span><br/><span>     ↓</span><br/><span>Write Loan Records</span><br/><span>     ↓</span><br/><span>Close File</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Step 3 – Reading & Parsing CSV Data

### 🔹 Purpose

Convert stored text data back into usable values.

CSV parsing involves:

* Reading one line at a time
* Splitting the line using commas
* Converting text values into numbers

---

### 🔹 Key Parsing Concept

<pre class="overflow-visible! px-0!" data-start="2850" data-end="2950"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Single Line of Text</span><br/><span>     ↓</span><br/><span>Split by ","</span><br/><span>     ↓</span><br/><span>Array of Values</span><br/><span>     ↓</span><br/><span>Map to Loan Properties</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

### 🔄 CSV Reading Flow

<pre class="overflow-visible! px-0!" data-start="2982" data-end="3165"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Open CSV File</span><br/><span>     ↓</span><br/><span>Read Header Line (Ignore)</span><br/><span>     ↓</span><br/><span>Read Next Line</span><br/><span>     ↓</span><br/><span>Split into Values</span><br/><span>     ↓</span><br/><span>Convert to Correct Types</span><br/><span>     ↓</span><br/><span>Create Loan Object</span><br/><span>     ↓</span><br/><span>Repeat Until EOF</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Critical Concept – Data Safety

### ⚠ Problem

CSV files may contain:

* Typos
* Missing values
* Invalid numeric data

Unprotected parsing can crash the program.

---

### ✅ Solution Concept

* Validate numeric fields before using them
* Handle conversion failures safely
* Skip or report invalid records instead of crashing

---

### 🔄 Safe Parsing Flow

<pre class="overflow-visible! px-0!" data-start="3535" data-end="3695"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Read Value</span><br/><span>     ↓</span><br/><span>Try to Convert</span><br/><span>     ↓</span><br/><span>Success? ── Yes ──► Continue Processing</span><br/><span>     │</span><br/><span>     No</span><br/><span>     ↓</span><br/><span>Handle Error Gracefully</span><br/><span>     ↓</span><br/><span>Move to Next Record</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Calculated Field – Interest Amount

### 🔹 Purpose

The **total interest amount** is not stored directly.

It is derived from existing values.

---

### 🔹 Calculation Logic (Conceptual)

<pre class="overflow-visible! px-0!" data-start="3895" data-end="3952"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Interest Amount</span><br/><span>= Principal × Interest Rate ÷ 100</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

### 🔄 Calculation Flow

<pre class="overflow-visible! px-0!" data-start="3984" data-end="4107"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Loan Loaded</span><br/><span>     ↓</span><br/><span>Read Principal</span><br/><span>     ↓</span><br/><span>Read Interest Rate</span><br/><span>     ↓</span><br/><span>Apply Formula</span><br/><span>     ↓</span><br/><span>Display Calculated Interest</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Risk Classification Logic

Each loan must be classified based on its  **interest rate** .

---

### 🔎 Risk Categories

| Interest Rate | Risk Level  |
| ------------- | ----------- |
| > 10%         | High Risk   |
| 5% – 10%     | Medium Risk |
| < 5%          | Low Risk    |

---

### 🔄 Risk Evaluation Flow

<pre class="overflow-visible! px-0!" data-start="4402" data-end="4537"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Read Interest Rate</span><br/><span>     ↓</span><br/><span>Rate > 10?</span><br/><span>     ├── Yes → High Risk</span><br/><span>     ↓ No</span><br/><span>Rate ≥ 5?</span><br/><span>     ├── Yes → Medium Risk</span><br/><span>     ↓ No</span><br/><span>Low Risk</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Append Mode Challenge

### 🔹 Objective

Instead of overwriting data:

* Accept a **new loan entry**
* Add it to the **existing CSV file**

---

### 🔹 Why Append Mode Matters

* Preserves historical data
* Enables incremental updates
* Reflects real-world database behavior

---

### 🔄 Append Flow

<pre class="overflow-visible! px-0!" data-start="4850" data-end="4976"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>User Enters New Loan</span><br/><span>     ↓</span><br/><span>Open CSV in Append Mode</span><br/><span>     ↓</span><br/><span>Write New Record</span><br/><span>     ↓</span><br/><span>Close File</span><br/><span>     ↓</span><br/><span>Portfolio Updated</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Display Requirements

The final output should clearly show:

* Client Name
* Principal (formatted as currency)
* Interest Rate
* Calculated Interest Amount
* Risk Classification

This ensures both **technical correctness** and  **business clarity** .

---

## ✅ Expected Learning Outcomes

After completing this assignment, students will understand:

* How objects map to file storage
* How CSV parsing works internally
* How to handle unsafe data
* How to compute derived values
* How business rules drive classification
* How financial systems process portfolios

---

## 🌍 Real-World Applications

* Banking systems
* Loan management software
* Financial analytics platforms
* Risk assessment tools
* Accounting backends

---

📌 **Note**

This README is  **intentionally conceptual only** .

No implementation or solution code has been included.
