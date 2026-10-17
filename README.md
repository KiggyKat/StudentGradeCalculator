# Student Grade Calculator — Windows Forms

This project builds the form using C# code.

## Project files
- `StudentGradeCalculator.csproj`: project configuration
- `Program.cs`: starts the Windows Forms application
- `GradeCalculatorForm.cs`: form controls, layout, events, validation, calculation, and results

## What it does
Enter a nonblank student name and four numeric grades between 0 and 100. Decimal grades are accepted. Calculate Grade shows the arithmetic average to two decimal places and the corresponding letter grade (A ≥ 90, B ≥ 80, C ≥ 70, D ≥ 60, otherwise F). Clear resets everything; Exit closes the form.

## Events
`Load`, `Click` for three buttons, `TextChanged` for five inputs, and `FormClosing`.

## Layout and keyboard support
Uses `TableLayoutPanel`, `FlowLayoutPanel`, `Dock`, `AutoSize`, `MinimumSize`, and logical `TabIndex` values. Enter activates Calculate Grade.
