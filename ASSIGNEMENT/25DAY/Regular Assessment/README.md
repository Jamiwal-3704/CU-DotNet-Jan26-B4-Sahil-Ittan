# 📅 Day 25 – Challenge 01

## 🔐 The Secure Terminal (Masked Input Handling)

---

## 🎯 Objective

This challenge focuses on **secure keyboard input handling** in a terminal-based application.

The goal is to understand how:

* Input can be read **character-by-character**
* Sensitive data can be **hidden while typing**
* Buffered input logic differs from direct key reading

This simulates real-world scenarios like:

* PIN entry
* Password input
* Secure terminal authentication

---

## 🧠 Core Concepts Covered

* Character-level input handling
* Difference between buffered input and direct key input
* Masking sensitive data
* Using loops to control fixed-length input
* Understanding keyboard events

---

## 🧩 Problem Overview

You are building a **secure terminal interface** that performs two key actions:

1. Accepts a **4-digit access PIN** securely
2. Ensures that typed digits are **not visible** on the screen

Instead of displaying the actual digits, the terminal must:

* Show `*` characters
* Still internally store the real digits
* Maintain correct input length

---

## 📌 Task 1 – The Masked PIN (Using ReadKey)

### 🔹 Purpose

To securely capture a **4-digit PIN** without exposing the digits on the screen.

This mimics:

* ATM PIN entry
* Secure login terminals
* Authentication prompts

---

## 🔐 Why Masked Input Is Important

* Prevents shoulder-surfing attacks
* Protects sensitive user data
* Improves security in shared environments

---

## 🔹 Input Rules

* Exactly **4 digits** must be entered
* Each key press:
  * Captures the actual character internally
  * Displays `*` instead of the digit
* Input is processed **one key at a time**

---

## 🧠 Key Technical Idea (Conceptual)

Instead of reading the entire input at once:

* The program listens to **individual key presses**
* Each key press is handled immediately
* Display behavior and storage behavior are separated

---

## 🔄 Masked PIN Input Flow Chart

<pre class="overflow-visible! px-0!" data-start="2199" data-end="2494"><div class="relative w-full my-4"><div class=""><div class="relative"><div class="h-full min-h-0 min-w-0"><div class="h-full min-h-0 min-w-0"><div class="border border-token-border-light border-radius-3xl corner-superellipse/1.1 rounded-3xl"><div class="h-full w-full border-radius-3xl bg-token-bg-elevated-secondary corner-superellipse/1.1 overflow-clip rounded-3xl lxnfua_clipPathFallback"><div class="pointer-events-none absolute inset-x-4 top-12 bottom-4"><div class="pointer-events-none sticky z-40 shrink-0 z-1!"><div class="sticky bg-token-border-light"></div></div></div><div class=""><div class="relative z-0 flex max-w-full"><div id="code-block-viewer" dir="ltr" class="q9tKkq_viewer cm-editor z-10 light:cm-light dark:cm-light flex h-full w-full flex-col items-stretch ͼk ͼy"><div class="cm-scroller"><div class="cm-content q9tKkq_readonly"><span>Start Program</span><br/><span>     ↓</span><br/><span>Prompt User for PIN</span><br/><span>     ↓</span><br/><span>Initialize Empty PIN Storage</span><br/><span>     ↓</span><br/><span>Repeat Until 4 Digits Entered</span><br/><span>     ↓</span><br/><span>Read Single Key Press</span><br/><span>     ↓</span><br/><span>Capture Actual Character</span><br/><span>     ↓</span><br/><span>Store Character Internally</span><br/><span>     ↓</span><br/><span>Print "*" to Console</span><br/><span>     ↓</span><br/><span>Check Digit Count</span><br/><span>     ↓</span><br/><span>PIN Entry Complete</span></div></div></div></div></div></div></div></div></div><div class=""><div class=""></div></div></div></div></div></pre>

---

## 🔹 Important Logic Explanation

### 📥 Capturing Input

* The terminal reads **one key press at a time**
* The actual character is captured internally

### 👁️ Display Control

* The pressed digit is **never shown**
* A `*` symbol is printed instead

### 🧾 Storage

* Actual digits are stored in a string
* Masking affects only display, not data

---

## 🔑 Key Property Concept

* Each key press provides access to the **actual character**
* This allows:
  * Secure storage
  * Controlled display
  * Input validation if needed later

---

## ✅ Expected Outcome

After completing this challenge, the terminal should:

* Accept exactly **4 digits**
* Display `****` on screen
* Internally store the real PIN
* Prevent accidental exposure of sensitive input

---

## 🌍 Real-World Applications

* Login systems
* Banking applications
* Admin terminals
* Secure command-line tools
* Authentication modules

---

## 📌 Notes

* This challenge focuses on **input security**
* No encryption is involved yet
* The emphasis is on **input handling and masking**
* No implementation or solution code is included intentionally

---

## 🎯 Learning Outcome Summary

By completing this challenge, students will understand:

* How terminal input works at a low level
* The difference between key reading and line reading
* How to mask sensitive input
* How secure terminals handle authentication input
