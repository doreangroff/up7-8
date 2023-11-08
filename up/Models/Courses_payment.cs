namespace up.Entities;

public class Courses_payment
{
    public Courses_payment(int cPayment, string course, string client, string month)
    {
        CPayment = cPayment;
        Course = course;
        Client = client;
        Month = month;
    }

    public int CPayment { get; set; }
    public string Course { get; set; }
    public string Client { get; set; }
    public string Month { get; set; }
}