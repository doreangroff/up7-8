using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.StudentsDir;

public partial class AddStudent : Window
{
    private List<Students> _students;
    private List<Clients> _clients;
    private List<Group> _groups;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    
    public AddStudent()
    {
        InitializeComponent();
        FillClients();
        FillGroup();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _students = new List<Students>();
        MySqlConnection _connection;
        string sql = """
                        insert into Students (Client, `Group`)
                        values (@Client, @Group)
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Group", MySqlDbType.Int32);
        command.Parameters["@Group"].Value = (CBGroup.SelectedItem as up.Entities.Group).Group_id;
        command.Parameters.Add("@Client", MySqlDbType.Int32);
        command.Parameters["@Client"].Value = (CBoxClient.SelectedItem as up.Entities.Clients).Client_id;
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
    
    private void FillClients()
    {
        string sql = "select * from Clients";
        _clients = new List<Clients>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curStud = new Clients(
                reader.GetInt32("Client_id"),
                reader.GetString("Client_name"),
                reader.GetString("Phone_number"),
                reader.GetDateTime("Birthday"),
                reader.GetString("Language_needs")
            );
            
            _clients.Add(curStud);
        }
        _connection.Close();
        var studComboBox = this.Find<ComboBox>("CBoxClient");
        studComboBox.ItemsSource = _clients;
    }
    private void FillGroup()
    {
        string sql = """
                     select Group_id, Group_name, Course_name, Stud_max_count from `Group`
                     join language_school.Courses C on C.Course_id = `Group`.Course
                     """;
        _groups = new List<Group>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curGroup = new Group(
                reader.GetInt32("Group_id"),
                reader.GetString("Group_name"),
                reader.GetString("Course_name"),
                reader.GetInt32("Stud_max_count")
            );
            
            _groups.Add(curGroup);
        }
        _connection.Close();
        var groupComboBox = this.Find<ComboBox>("CBGroup");
        groupComboBox.ItemsSource = _groups;
    }
}