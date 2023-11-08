using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.StudentsDir;

public partial class DeleteStudent : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    private MySqlConnection _connection;
    private readonly Students _stud;
    
    public DeleteStudent(Students stud)
    {
        InitializeComponent();
        _stud = stud;
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from Students where Client = @Client and `Group` = @Group LIMIT 1";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Client", MySqlDbType.String);
        command.Parameters["@Client"].Value = _stud.Client;
        command.Parameters.Add("@Group", MySqlDbType.String);
        command.Parameters["@Group"].Value = _stud.Group;
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