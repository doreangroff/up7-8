namespace up.Entities;

public class Days_of_week
{
    public Days_of_week(int dayId, string dayName)
    {
        Day_id = dayId;
        Day_name = dayName;
    }

    public int Day_id { get; set; }
    public string Day_name { get; set; }
}