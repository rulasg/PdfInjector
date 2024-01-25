public class DocInfo{
    public Person Student {get;}
    public Person Trainer {get;}
    public string CourseName {get;}
    public string CourseDate {get;}

    public string Id {get;}

    public DocInfo(Person student, Person trainer, string courseName, string courseDate, string id)
    {
        Student = student;
        Trainer = trainer;
        CourseName = courseName;
        CourseDate = courseDate;
        Id = id;
    }
}