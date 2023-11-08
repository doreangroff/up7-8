using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.CPayments;

public partial class DeleteCPayment : Window
{
    private readonly Courses_payment _cp;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                //"server=localhost;database=language_school;user=user_1;password=1234";
    private MySqlConnection _connection;
    public DeleteCPayment(Courses_payment cp)
    {
        InitializeComponent();
        _cp = cp;
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from Courses_payment where CPayment_id = @CPayment_id LIMIT 1";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@CPayment_id", MySqlDbType.Int32);
        command.Parameters["@CPayment_id"].Value = _cp.CPayment;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }
}