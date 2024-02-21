using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.TeachersDir;

public partial class EditTeacher : Window
{
    private readonly Teachers _teach;
    private List<Teachers> _teachers;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    
    public EditTeacher(Teachers teach)
    {
        InitializeComponent();
        DataContext = teach;
        _teach = teach;
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _teachers = new List<Teachers>();
        MySqlConnection _connection;
        string sql = """
                        update Teachers set Teacher_id = @Teacher_id, 
                        Teacher_name = @Teacher_name
                        where Teacher_id = @Teacher_id
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Teacher_id", MySqlDbType.Int32);
        command.Parameters["@Teacher_id"].Value = TBxId.Text;
        command.Parameters.Add("@Teacher_name", MySqlDbType.String);
        command.Parameters["@Teacher_name"].Value = TBName.Text;
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }
}