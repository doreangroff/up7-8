namespace up.Entities;

public class Language_levels
{
    public Language_levels(int levelId, string levelName)
    {
        Level_id = levelId;
        Level_name = levelName;
    }

    public int Level_id { get; set; }
    public string Level_name { get; set; }
}