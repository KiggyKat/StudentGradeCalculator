using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace StudentGradeCalculator;

public class GradeCalculatorForm : Form
{
    private readonly TextBox txtStudentName = new();
    private readonly TextBox txtGrade1 = new();
    private readonly TextBox txtGrade2 = new();
    private readonly TextBox txtGrade3 = new();
    private readonly TextBox txtGrade4 = new();

    private readonly Button btnCalculate = new() { Text = "Calculate Grade", AutoSize = true };
    private readonly Button btnClear = new() { Text = "Clear", AutoSize = true };
    private readonly Button btnExit = new() { Text = "Exit", AutoSize = true };

    private readonly Label lblStudentValue = new() { Text = "—", AutoSize = true };
    private readonly Label lblAverageValue = new() { Text = "—", AutoSize = true };
    private readonly Label lblLetterValue = new() { Text = "—", AutoSize = true };
    private readonly Label lblStatus = new() { Text = "Status: Ready", AutoSize = true, ForeColor = Color.DarkSlateBlue };

    public GradeCalculatorForm()
    {
        Name = "frmGradeCalculator";
        Text = "Student Grade Calculator";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(470, 610);
        Size = new Size(650, 700);
        Font = new Font("Segoe UI", 10F);
        AutoScaleMode = AutoScaleMode.Font;
        BuildInterface();
        ConnectEvents();
    }

    private void BuildInterface()
    {
        // Dock and TableLayoutPanel keep sections aligned when the form is resized.
        var main = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(18),
            ColumnCount = 1,
            RowCount = 6
        };
        main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        main.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        main.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        main.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        main.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        main.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        main.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        Controls.Add(main);

        var lblInstructions = new Label
        {
            Name = "lblInstructions",
            Text = "Enter a student name and four assignment grades (0–100).",
            AutoSize = true,
            Margin = new Padding(3, 2, 3, 14)
        };
        main.Controls.Add(lblInstructions, 0, 0);

        var grpStudent = MakeGroup("grpStudent", "Student Information");
        var studentGrid = MakeGrid();
        AddField(studentGrid, 0, "lblStudentName", "Student Name:", txtStudentName, "txtStudentName", 0);
        grpStudent.Controls.Add(studentGrid);
        main.Controls.Add(grpStudent, 0, 1);

        var grpGrades = MakeGroup("grpGrades", "Assignment Grades");
        var gradeGrid = MakeGrid();
        AddField(gradeGrid, 0, "lblGrade1", "Assignment 1:", txtGrade1, "txtGrade1", 1);
        AddField(gradeGrid, 1, "lblGrade2", "Assignment 2:", txtGrade2, "txtGrade2", 2);
        AddField(gradeGrid, 2, "lblGrade3", "Assignment 3:", txtGrade3, "txtGrade3", 3);
        AddField(gradeGrid, 3, "lblGrade4", "Assignment 4:", txtGrade4, "txtGrade4", 4);
        grpGrades.Controls.Add(gradeGrid);
        main.Controls.Add(grpGrades, 0, 2);

        var buttons = new FlowLayoutPanel
        {
            Name = "pnlActions",
            AutoSize = true,
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            Margin = new Padding(0, 10, 0, 10)
        };
        btnCalculate.Name = "btnCalculate";
        btnClear.Name = "btnClear";
        btnExit.Name = "btnExit";
        btnCalculate.TabIndex = 5;
        btnClear.TabIndex = 6;
        btnExit.TabIndex = 7;
        buttons.Controls.AddRange(new Control[] { btnCalculate, btnClear, btnExit });
        main.Controls.Add(buttons, 0, 3);

        var grpResults = MakeGroup("grpResults", "Results");
        var resultsGrid = MakeGrid();
        AddOutput(resultsGrid, 0, "lblResultStudent", "Student:", lblStudentValue, "lblStudentValue");
        AddOutput(resultsGrid, 1, "lblResultAverage", "Average:", lblAverageValue, "lblAverageValue");
        AddOutput(resultsGrid, 2, "lblResultLetter", "Letter Grade:", lblLetterValue, "lblLetterValue");
        grpResults.Controls.Add(resultsGrid);
        main.Controls.Add(grpResults, 0, 4);

        lblStatus.Name = "lblStatus";
        lblStatus.MaximumSize = new Size(570, 0);
        lblStatus.Margin = new Padding(3, 12, 3, 3);
        main.Controls.Add(lblStatus, 0, 5);
        AcceptButton = btnCalculate;
    }

    private static GroupBox MakeGroup(string name, string title) => new()
    {
        Name = name,
        Text = title,
        Dock = DockStyle.Fill,
        AutoSize = true,
        Padding = new Padding(12),
        Margin = new Padding(0, 5, 0, 8)
    };

    private static TableLayoutPanel MakeGrid()
    {
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            ColumnCount = 2,
            Padding = new Padding(2)
        };
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        return grid;
    }

    private static void AddField(TableLayoutPanel grid, int row, string labelName,
        string caption, TextBox input, string inputName, int tabIndex)
    {
        grid.RowCount = row + 1;
        grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        grid.Controls.Add(new Label
        {
            Name = labelName, Text = caption, AutoSize = true,
            Anchor = AnchorStyles.Left, Margin = new Padding(3, 7, 3, 7)
        }, 0, row);
        input.Name = inputName;
        input.Dock = DockStyle.Fill;
        input.TabIndex = tabIndex;
        input.Margin = new Padding(3, 4, 3, 4);
        grid.Controls.Add(input, 1, row);
    }

    private static void AddOutput(TableLayoutPanel grid, int row, string labelName,
        string caption, Label output, string outputName)
    {
        grid.RowCount = row + 1;
        grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        grid.Controls.Add(new Label
        {
            Name = labelName, Text = caption, AutoSize = true,
            Margin = new Padding(3, 6, 3, 6)
        }, 0, row);
        output.Name = outputName;
        output.Margin = new Padding(3, 6, 3, 6);
        grid.Controls.Add(output, 1, row);
    }

    private void ConnectEvents()
    {
        Load += frmGradeCalculator_Load;
        btnCalculate.Click += btnCalculate_Click;
        btnClear.Click += btnClear_Click;
        btnExit.Click += btnExit_Click;
        txtStudentName.TextChanged += Input_TextChanged;
        txtGrade1.TextChanged += Input_TextChanged;
        txtGrade2.TextChanged += Input_TextChanged;
        txtGrade3.TextChanged += Input_TextChanged;
        txtGrade4.TextChanged += Input_TextChanged;
        FormClosing += frmGradeCalculator_FormClosing;
    }

    private void frmGradeCalculator_Load(object? sender, EventArgs e)
    {
        lblStatus.Text = "Status: Ready";
        ActiveControl = txtStudentName;
    }

    private void Input_TextChanged(object? sender, EventArgs e)
    {
        ClearResults();
        lblStatus.Text = "Status: Input changed. Select Calculate Grade to update results.";
    }

    private void btnCalculate_Click(object? sender, EventArgs e)
    {
        if (!ValidateInputs(out string studentName, out double[] grades))
            return;

        double average = CalculateAverage(grades[0], grades[1], grades[2], grades[3]);
        string letterGrade = DetermineLetterGrade(average);
        DisplayResults(studentName, average, letterGrade);
    }

    private bool ValidateInputs(out string studentName, out double[] grades)
    {
        studentName = txtStudentName.Text.Trim();
        grades = new double[4];
        if (studentName.Length == 0)
        {
            ShowValidationError("Enter the student's name.", txtStudentName);
            return false;
        }

        TextBox[] fields = { txtGrade1, txtGrade2, txtGrade3, txtGrade4 };
        for (int i = 0; i < fields.Length; i++)
        {
            // Accept decimal grades, but reject empty, nonnumeric, and out-of-range values.
            if (!double.TryParse(fields[i].Text, NumberStyles.Float,
                    CultureInfo.CurrentCulture, out double value)
                || !double.IsFinite(value) || value < 0 || value > 100)
            {
                ShowValidationError($"Enter a number from 0 to 100 for Assignment {i + 1}.", fields[i]);
                return false;
            }
            grades[i] = value;
        }
        return true;
    }

    private void ShowValidationError(string message, TextBox field)
    {
        ClearResults();
        lblStatus.Text = "Status: " + message;
        lblStatus.ForeColor = Color.Firebrick;
        field.Focus();
        field.SelectAll();
    }

    private static double CalculateAverage(double grade1, double grade2, double grade3, double grade4)
    {
        return (grade1 + grade2 + grade3 + grade4) / 4.0;
    }

    private static string DetermineLetterGrade(double average)
    {
        if (average >= 90) return "A";
        else if (average >= 80) return "B";
        else if (average >= 70) return "C";
        else if (average >= 60) return "D";
        else return "F";
    }

    private void DisplayResults(string studentName, double average, string letterGrade)
    {
        lblStudentValue.Text = studentName;
        lblAverageValue.Text = average.ToString("F2", CultureInfo.CurrentCulture);
        lblLetterValue.Text = letterGrade;
        lblStatus.ForeColor = Color.DarkGreen;
        lblStatus.Text = "Status: Calculation completed successfully.";
    }

    private void ClearResults()
    {
        lblStudentValue.Text = "—";
        lblAverageValue.Text = "—";
        lblLetterValue.Text = "—";
        lblStatus.ForeColor = Color.DarkSlateBlue;
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        txtStudentName.Clear();
        txtGrade1.Clear();
        txtGrade2.Clear();
        txtGrade3.Clear();
        txtGrade4.Clear();
        ClearResults();
        lblStatus.Text = "Status: Ready";
        txtStudentName.Focus();
    }

    private void btnExit_Click(object? sender, EventArgs e) => Close();

    private void frmGradeCalculator_FormClosing(object? sender, FormClosingEventArgs e)
    {
        // No files or database connections need cleanup in this simple application.
    }
}
