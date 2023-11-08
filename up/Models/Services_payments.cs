namespace up.Entities;

public class Services_payments
{
    public Services_payments(int sPayment, string client, string service)
    {
        SPayment = sPayment;
        Client = client;
        Service = service;
    }

    public int SPayment { get; set; }
    public string Client { get; set; }
    public string Service { get; set; }
}