using Student_Grade_Manager;

var manager = new StudentManager();
manager.LoadStudents(StudentStorage.Load());

try
{
    RunMenu(manager);
}
finally
{
    StudentStorage.Save(manager.GetAllStudents());
    Console.WriteLine("Data saved. Goodbye.");
}

static void RunMenu(StudentManager manager)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("1. Add student");
        Console.WriteLine("2. Edit student");
        Console.WriteLine("3. Delete student");
        Console.WriteLine("4. Add grade");
        Console.WriteLine("5. View all students");
        Console.WriteLine("6. Search student");
        Console.WriteLine("7. Statistics");
        Console.WriteLine("0. Exit and save all changes");
        Console.Write("Choice: ");

        if (!int.TryParse(Console.ReadLine(), out var choice))
        {
            Console.WriteLine("Invalid input.");
            continue;
        }

        switch (choice)
        {
            case 0: return;
            case 1: AddStudent(manager); break;
            case 2: EditStudent(manager); break;
            case 3: DeleteStudent(manager); break;
            case 4: AddGrade(manager); break;
            case 5: ViewAll(manager); break;
            case 6: SearchStudent(manager); break;
            case 7: Statistics(manager); break;
            default: Console.WriteLine("Unknown option."); break;
        }
    }
}

static void AddStudent(StudentManager manager)
{
    Console.Write("Name: ");
    var name = Console.ReadLine() ?? "";
    Console.Write("Surname: ");
    var surname = Console.ReadLine() ?? "";
    try
    {
        manager.AddStudent(name, surname);
        Console.WriteLine("Student added.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void EditStudent(StudentManager manager)
{
    var id = ReadId(manager);
    if (id == null) return;
    Console.Write("Name: ");
    var name = Console.ReadLine() ?? "";
    Console.Write("Surname: ");
    var surname = Console.ReadLine() ?? "";
    try
    {
        manager.EditStudent(id.Value, name, surname);
        Console.WriteLine("Student updated.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void DeleteStudent(StudentManager manager)
{
    var id = ReadId(manager);
    if (id == null) return;
    if (manager.RemoveStudent(id.Value))
        Console.WriteLine("Student deleted.");
    else
        Console.WriteLine("Student not found.");
}

static void AddGrade(StudentManager manager)
{
    var id = ReadId(manager);
    if (id == null) return;
    var student = manager.GetById(id.Value);
    if (student == null) { Console.WriteLine("Student not found."); return; }
    Console.Write("Grade (1-6): ");
    if (!int.TryParse(Console.ReadLine(), out var grade))
    {
        Console.WriteLine("Invalid number.");
        return;
    }
    try
    {
        student.AddGrade(grade);
        Console.WriteLine($"Grade {grade} added. Average: {student.GetAverageGrade():F2}");
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void ViewAll(StudentManager manager)
{
    Console.WriteLine("--- All students ---");
    manager.ListAllStudents();
}

static void SearchStudent(StudentManager manager)
{
    Console.Write("Search (name, surname or id): ");
    var term = Console.ReadLine() ?? "";
    var results = manager.SearchStudent(term);
    Console.WriteLine($"Found {results.Count} student(s):");
    foreach (var s in results) Console.WriteLine(s);
}

static void Statistics(StudentManager manager)
{
    var list = manager.GetAllStudents().ToList();
    if (list.Count == 0) { Console.WriteLine("No students."); return; }

    var withGrades = list.Where(s => s.Grades.Count > 0).ToList();
    if (withGrades.Count > 0)
    {
        var top = withGrades.OrderByDescending(s => s.GetAverageGrade()).First();
        var lowest = withGrades.OrderBy(s => s.GetAverageGrade()).First();
        Console.WriteLine($"This statistic for students only with avg > 0");
        Console.WriteLine($"Top student:   {top}");
        Console.WriteLine($"Lowest student: {lowest}");
    }

    Console.Write("Show students with average above (1-6, or Enter to skip): ");
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line)) return;
    if (!double.TryParse(line.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out var threshold) ||
        threshold < 1 || threshold > 6)
    {
        Console.WriteLine("Invalid threshold.");
        return;
    }
    var above = list.Where(s => s.Grades.Count > 0 && s.GetAverageGrade() >= threshold).ToList();
    Console.WriteLine($"Students with average >= {threshold}: {above.Count}");
    foreach (var s in above) Console.WriteLine(s);
}

static int? ReadId(StudentManager manager)
{
    Console.Write("Student Id: ");
    if (!int.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Invalid Id."); return null; }
    if (manager.GetById(id) == null) { Console.WriteLine("Student not found."); return null; }
    return id;
}
