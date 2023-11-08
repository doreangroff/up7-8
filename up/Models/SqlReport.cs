namespace up.Entities;

public class SqlReport
{
    public SqlReport(string student, int attendace)
    {
        Student = student;
        Attendace = attendace;
    }

    public string Student { get; set; }
    public int Attendace { get; set; }
}