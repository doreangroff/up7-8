namespace up.Entities;

public class Group
{
    public Group(int groupId, string groupName, string course, int studMaxCount)
    {
        Group_id = groupId;
        Group_name = groupName;
        Course = course;
        Stud_max_count = studMaxCount;
    }

    public int Group_id { get; set; }
    public string Group_name { get; set; }
    public string Course { get; set; }
    public int Stud_max_count { get; set; }
}