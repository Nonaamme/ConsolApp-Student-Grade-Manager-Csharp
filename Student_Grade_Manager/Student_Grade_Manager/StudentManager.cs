namespace Student_Grade_Manager;

public class StudentManager
{
    private readonly List<Student> _students = new();
    private int _nextId = 1;

    public IReadOnlyList<Student> Students => _students.AsReadOnly();

    public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public void AddStudent(string name, string surname)
    {
        name = name?.Trim() ?? "";
        surname = surname?.Trim() ?? "";
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        _students.Add(new Student
        {
            Id = _nextId++,
            Name = name,
            Surname = surname
        });
    }

    public void EditStudent(int id, string name, string surname)
    {
        var student = GetById(id) ?? throw new InvalidOperationException($"Student with Id {id} not found.");
        name = name?.Trim() ?? "";
        surname = surname?.Trim() ?? "";
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        student.Name = name;
        student.Surname = surname;
    }

    public bool RemoveStudent(int id)
    {
        var student = GetById(id);
        if (student == null) return false;
        _students.Remove(student);
        return true;
    }

    public List<Student> SearchStudent(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return _students.ToList();
        var term = searchTerm.Trim();
        return _students
            .Where(s => s.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        s.Surname.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        s.Id.ToString().Contains(term))
            .ToList();
    }

    public void ListAllStudents()
    {
        foreach (var s in _students)
            Console.WriteLine(s);
        if (_students.Count == 0)
            Console.WriteLine("No students.");
    }

    public void LoadStudents(IEnumerable<Student> students)
    {
        _students.Clear();
        _students.AddRange(students);
        _nextId = _students.Count == 0 ? 1 : _students.Max(s => s.Id) + 1;
    }

    public IEnumerable<Student> GetAllStudents() => _students.ToList();
}
