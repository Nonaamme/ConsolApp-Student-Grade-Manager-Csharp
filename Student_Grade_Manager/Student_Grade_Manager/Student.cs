namespace Student_Grade_Manager;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public List<int> Grades { get; set; } = new();

    public void AddGrade(int grade)
    {
        if (grade < 1 || grade > 6)
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 1 and 6.");
        Grades.Add(grade);
    }

    public double GetAverageGrade()
    {
        if (Grades.Count == 0) return 0;
        return Grades.Average();
    }

    public override string ToString() =>
        $"{Id}: {Name} {Surname} | Grades: [{string.Join(", ", Grades)}] | Avg: {GetAverageGrade():F2}";
}
