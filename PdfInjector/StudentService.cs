
namespace PdfInjector;

public static class StudentService{

    public static Person GetStudent(string handle)
    {
        var student = new Person {
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
