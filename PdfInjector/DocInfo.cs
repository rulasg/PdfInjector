using iTextSharp.testutils;

public class DocInfo{

    public string StampName {get; set;}
    public Person Student {get; set;}
    public Person Trainer {get; set;}
    public string CourseName {get; set;}
    public string CourseDate {get; set;}

    public string?Id {get; set;}

    //ctor
    public DocInfo(){
        Student = new Person();
        Trainer = new Person();
        CourseName = "";
        CourseDate = "";
        Id = "";
        StampName = "";
    }

    public DocInfo(Person student, Person trainer, string courseName, string courseDate, string id, string stampName)
    {
        StampName = stampName;
        Student = student;
        Trainer = trainer;
        CourseName = courseName;
        CourseDate = courseDate;
        Id = id;
    }

    public bool Test(){
        if(Student.Test() && Trainer.Test() && 
            CourseName != "" && CourseDate != "" && Id != "" && StampName != ""){

            return true;
        }
        return false;
    }
}