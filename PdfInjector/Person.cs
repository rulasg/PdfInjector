
public class Person
{
    public string Name { get; set; }
    public string Handle { get; set; }
    public string Company { get; set; }

    //ctor
    public Person()
    {
        Name = "";
        Handle = "";
        Company = "";
    }

    public bool Test(){
        if (Name == "" || Handle == "" || Company == "")
        {
            return false;
        }
        return true;
    }
}