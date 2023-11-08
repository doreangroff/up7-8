using System;

namespace up.Entities;

public class Attendance_logs
{
    public Attendance_logs(DateOnly lessonDate, string group, string client, int attendance)
    {
        Lesson_date = lessonDate;
        Group = group;
        Client = client;
        Attendance = attendance;
    }

    public DateOnly Lesson_date { get; set; }
    public string Group { get; set; }
    public string Client { get; set; }

    public int Attendance;

    public string Att
    {
        get
        {
            if (Attendance == 1)
                return "Отсутсвовал";
            else
            {
                return "Присутствовал";
            }
        }
    }
    
}