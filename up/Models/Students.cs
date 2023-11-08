namespace up.Entities;

public class Students
{
    public Students(string client, string group)
    {
        Client = client;
        Group = group;
    }

    public string Client { get; set; }
    public string Group { get; set; }
}