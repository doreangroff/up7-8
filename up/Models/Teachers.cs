namespace up.Entities;

public class Teachers
{
    public Teachers(int teacherId, string teacherName)
    {
        Teacher_id = teacherId;
        Teacher_name = teacherName;
    }

    public int Teacher_id { get; set; }
    public string Teacher_name { get; set; }
}