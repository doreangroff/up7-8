namespace up.Entities;

public class Language_experience
{
    public Language_experience(string client, string language, string level)
    {
        Client = client;
        Language = language;
        Level = level;
    }

    public string Client { get; set; }
    public string Language { get; set; }
    public string Level { get; set; }
}