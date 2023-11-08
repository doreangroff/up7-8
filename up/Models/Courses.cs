namespace up.Entities;

public class Courses
{
    public Courses(int courseId, string courseName, string teacher, string language, double cost)
    {
        Course_id = courseId;
        Course_name = courseName;
        Teacher = teacher;
        Language = language;
        Cost = cost;
    }

    public int Course_id { get; set; }
    public string Course_name { get; set; }
    public string Teacher { get; set; }
    public string Language { get; set; }
    public double Cost { get; set; }
}