namespace up.Entities;

public class Services
{
    public Services(int serviceId, string serviceName, double serviceCost, string teacher)
    {
        Service_id = serviceId;
        Service_name = serviceName;
        Service_cost = serviceCost;
        Teacher = teacher;
    }

    public int Service_id { get; set; }
    public string Service_name { get; set; }
    public double Service_cost { get; set; }
    public string Teacher { get; set; }
}