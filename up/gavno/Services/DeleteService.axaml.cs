using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;

namespace up.gavno.Services;

public partial class DeleteService : Window
{
    private readonly Entities.Services _ser;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    private MySqlConnection _connection;
    
    public DeleteService(Entities.Services ser)
    {
        InitializeComponent();
        _ser = ser;
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from Services where Service_id = @Service_id LIMIT 1";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Service_id", MySqlDbType.Int32);
        command.Parameters["@Service_id"].Value = _ser.Service_id;
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