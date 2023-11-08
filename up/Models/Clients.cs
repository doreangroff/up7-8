using System;

namespace up.Entities;

public class Clients
{
    public Clients(int clientId, string clientName, string phoneNumber, DateTime birthday, string languageNeeds)
    {
        Client_id = clientId;
        Client_name = clientName;
        Phone_number = phoneNumber;
        Birthday = birthday;
        Language_needs = languageNeeds;
    }
    
    

    public int Client_id { get; set; }
    public string Client_name { get; set; }
    public string Phone_number { get; set; }
    public DateTime Birthday { get; set; }
    public string Language_needs { get; set; }
}