# 📔 Daily Logger - Console Application

## Project Overview

A simple yet practical console application that helps users maintain a daily journaling habit. The application prompts users to enter their daily reflection and appends it to a persistent file, ensuring all entries are preserved over time.

## Features

* 📝 Prompts users for a daily reflection/thought
* 💾 Automatically saves entries to `journal.txt`
* 🔄 Appends new entries without overwriting previous ones
* 📅 Timestamps can be optionally included with each entry
* 📂 Creates the journal file automatically if it doesn't exist

## Learning Objectives

* Understanding file I/O operations in C#
* Working with StreamWriter and append mode
* Handling file paths and file existence
* Implementing user input validation
* Understanding boolean parameters in constructors

## How It Works

1. The application starts and welcomes the user
2. Prompts for today's reflection/thought
3. Captures user input
4. Opens/Creates `journal.txt` in append mode
5. Writes the entry to the file (optionally with timestamp)
6. Confirms successful save to the user
7. Ends the program

## Key Concept: File Append Mode

The critical technical aspect is using the boolean parameter in StreamWriter:

* `new StreamWriter(filePath, true)` → Appends to existing file
* `new StreamWriter(filePath, false)` → Overwrites file
* The `true` parameter ensures all journal entries are preserved

## Expected File Format

Each entry in `journal.txt` could look like:

**text**

```
[2024-01-15] Today I learned about file handling in C#...
[2024-01-16] Successfully implemented the daily logger...
```

## Project Flowchart

**text**

```
┌─────────────────────┐
│        START        │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Display Welcome    │
│      Message        │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Prompt User for     │
│ Daily Reflection    │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│   Read User Input   │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Open/Create journal.txt│
│ with append=true    │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Add Timestamp to    │
│     Entry?          │
│   ┌───┴───┐         │
│  Yes      No        │
└──┬───────┬┘         │
   ↓       ↓          │
┌─────────────────────┐
│ Format Entry with   │
│ or without timestamp│
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Write Entry to     │
│      File           │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Close File Stream  │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Display Success     │
│     Message         │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│         END         │
└─────────────────────┘
```

## Error Handling Considerations

* Handle cases where file is in use
* Manage disk space limitations
* Handle permission issues
* Validate non-empty input

## Extensions/Ideas for Enhancement

* Add date/time stamps automatically
* View previous entries
* Search functionality
* Multiple journal files (work, personal, etc.)
* Entry encryption
* Export functionality

## Prerequisites

* Basic C# knowledge
* Understanding of console applications
* Familiarity with file paths and I/O concepts

This project serves as an excellent introduction to persistent data storage in C# while creating something practical and immediately useful for daily journaling habits.
