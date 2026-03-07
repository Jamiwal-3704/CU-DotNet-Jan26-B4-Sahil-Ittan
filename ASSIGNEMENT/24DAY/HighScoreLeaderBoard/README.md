# 📅 Day 24 – Challenge 02

## 🏁 The High-Score Leaderboard (SortedDictionary)

---

## 🎯 Objective

This challenge is designed to help students understand how **sorted collections** work in .NET and how they can be used to build  **real-time ranking systems** .

You are developing a **racing game leaderboard** where:

* Each player has a **fastest lap time**
* The leaderboard must **always stay sorted**
* The fastest player should always appear at the top

---

## 🧠 Core Concept

* Using a **SortedDictionary**
* Automatic sorting based on **keys**
* Understanding why **keys are immutable**
* Efficient retrieval of the **best (minimum) value**
* Updating records while maintaining order

---

## 🧩 Problem Description

The leaderboard stores:

* **Lap Time (seconds)** → as the **Key**
* **Player Name** → as the **Value**

Since lap time determines ranking, the collection must always remain sorted so the leaderboard is **ready to display at any moment** without extra sorting logic.

---

## 📌 Task 1 – Initialize the Leaderboard

### 🔹 Purpose

Create a sorted collection that:

* Automatically maintains order
* Uses lap time as the ranking factor

### 🔹 Why a Sorted Dictionary?

* Sorting is done **internally**
* No manual sorting required
* Fastest lap time always appears first

---

### 🔄 Initialization Flow

<pre class="overflow-visible! px-0!" data-start="1570" data-end="1704"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Start Program</span><br/><span>     ↓</span><br/><span>Create Sorted Collection</span><br/><span>     ↓</span><br/><span>Define Key → Lap Time</span><br/><span>Define Value → Player Name</span><br/><span>     ↓</span><br/><span>Leaderboard Ready</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Task 2 – Populate the Leaderboard

### 🔹 Purpose

Add multiple player records with different lap times.

Even if records are added in a random order, the collection ensures:

* Data is stored **sorted by lap time**
* Insertion order does not affect ranking

---

### 🔄 Data Insertion Flow

<pre class="overflow-visible! px-0!" data-start="2008" data-end="2144"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Add Player Record</span><br/><span>     ↓</span><br/><span>Insert into SortedDictionary</span><br/><span>     ↓</span><br/><span>Dictionary Reorders by Key</span><br/><span>     ↓</span><br/><span>Leaderboard Updated Automatically</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Task 3 – Automatic Sorting Verification

### 🔹 Goal

Verify that:

* Players are displayed from **fastest to slowest**
* The player with the **lowest lap time** appears first

This confirms that sorting is:

* Automatic
* Based on key values
* Independent of insertion order

---

### 🔄 Sorting Logic Flow

<pre class="overflow-visible! px-0!" data-start="2464" data-end="2595"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Player Added Last</span><br/><span>     ↓</span><br/><span>Key Comparison Occurs</span><br/><span>     ↓</span><br/><span>Lower Lap Time?</span><br/><span>     ↓</span><br/><span>Move to Top Position</span><br/><span>     ↓</span><br/><span>Leaderboard Sorted</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Task 4 – Gold Medal Time (Fastest Lap)

### 🥇 What Is the Gold Medal Time?

The **smallest lap time** in the leaderboard.

### 🔹 Requirement

* Retrieve the fastest lap time
* Do not use loops
* Use the fact that the collection is already sorted

---

### 🔄 Fastest Time Retrieval Flow

<pre class="overflow-visible! px-0!" data-start="2898" data-end="3025"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Access Sorted Collection</span><br/><span>     ↓</span><br/><span>Get First Entry</span><br/><span>     ↓</span><br/><span>First Key = Fastest Lap Time</span><br/><span>     ↓</span><br/><span>Gold Medal Winner Identified</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 📌 Task 5 – Updating a Player Record

### 🔹 Scenario

A player improves their lap time.

### ⚠ Important Rule

* **Keys in a SortedDictionary cannot be modified**
* Lap time is the key → it must be replaced

---

### 🔹 Correct Update Strategy

1. Remove the old record
2. Insert a new record with the updated time
3. Let the dictionary re-sort automatically

---

### 🔄 Update Flow Chart

<pre class="overflow-visible! px-0!" data-start="3427" data-end="3592"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Player Improves Time</span><br/><span>     ↓</span><br/><span>Old Lap Time Found</span><br/><span>     ↓</span><br/><span>Remove Old Entry</span><br/><span>     ↓</span><br/><span>Insert New Lap Time Entry</span><br/><span>     ↓</span><br/><span>Dictionary Re-Sorts</span><br/><span>     ↓</span><br/><span>Leaderboard Updated</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## ✅ Learning Outcomes

By completing this challenge, students will understand:

* How sorted collections work internally
* Why keys control ordering
* How to retrieve ranked data efficiently
* Why keys are immutable
* How real-world leaderboards are implemented

---

## 🌍 Real-World Use Cases

* Game leaderboards
* Ranking systems
* Performance tracking apps
* Priority-based systems
* Competitive scoring platforms

---

📌 **Note**

This README intentionally focuses on  **conceptual understanding only** .

No implementation or solution code has been included.
