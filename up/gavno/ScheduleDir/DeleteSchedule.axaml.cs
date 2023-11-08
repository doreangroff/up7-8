using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;

namespace up.gavno.ScheduleDir;

public partial class DeleteSchedule : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    private MySqlConnection _connection;
    private readonly Entities.Schedule _schedule;
    
    public DeleteSchedule(Entities.Schedule schedule)
    {
        InitializeComponent();
        _schedule = schedule;
    }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from Schedule where `Group` = @Group and Day_of_week = @Day_of_week LIMIT 1";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Group", MySqlDbType.Int32);
        command.Parameters["@Group"].Value = _schedule.Group;
        command.Parameters.Add("@Day_of_week", MySqlDbType.Int32);
        command.Parameters["@Day_of_week"].Value = _schedule.Day_of_week;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }
}