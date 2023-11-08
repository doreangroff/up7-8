namespace up.Entities;

public class Months
{
    public Months(int monthId, string monthName)
    {
        Month_id = monthId;
        Month_name = monthName;
    }

    public int Month_id { get; set; }
    public string Month_name { get; set; }
}