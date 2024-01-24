
namespace PdfInjector;

public static class StudentService{

    public static Student GetStudent(string handle)
    {
        var student = new Student {
            Name = GetNameFromGitHub(handle),
            Handle = handle
        };

        return student;
    }

    public static string GetNameFromGitHub(string handle)
    {
        // gh api users/handle
        return "Student Name";
    }
}
