using System;

namespace up.Entities;

public class Schedule
{
    public Schedule(string group, string dayOfWeek, TimeOnly time)
    {
        Group = group;
        Day_of_week = dayOfWeek;
        Time = time;
    }

    public string Group { get; set; }
    public int GroupId { get; set; }
    public string Day_of_week { get; set; }
    public int DayId { get; set; }
    public TimeOnly Time { get; set; }
}