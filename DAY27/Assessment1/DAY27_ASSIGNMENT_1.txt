Assignment: The Daily Logger
Objective: Create a console application that prompts a user for a "Daily Reflection." Every time the program runs, it should save the user's input to a file named journal.txt. Crucially, it must not overwrite previous entries; it must add them to the end of the file.

Step 1: The Code Implementation
You can use the StreamWriter constructor that accepts a file path and a boolean for append mode.

Key Technical Details
The Boolean Toggle: In new StreamWriter(filePath, true), the true tells the system to seek the end of the file before writing. If you set it to false (or omit it), the file is overwritten.