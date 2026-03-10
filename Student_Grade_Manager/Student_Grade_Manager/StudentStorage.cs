using System.Text.Json;

namespace Student_Grade_Manager;

public static class StudentStorage
{
    private static readonly string FilePath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "students.json");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    public static List<Student> Load()
    {
        if (!File.Exists(FilePath)) return new List<Student>();
        try
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }
        catch
        {
            return new List<Student>();
        }
    }

    public static void Save(IEnumerable<Student> students)
    {
        var json = JsonSerializer.Serialize(students.ToList(), Options);
        File.WriteAllText(FilePath, json);
    }
}
