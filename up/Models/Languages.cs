namespace up.Entities;

public class Languages
{
    public Languages(int languageId, string lanName)
    {
        Language_id = languageId;
        Lan_name = lanName;
    }

    public int Language_id { get; set; }
    public string Lan_name { get; set; }
}