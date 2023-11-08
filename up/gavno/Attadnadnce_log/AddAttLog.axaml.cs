using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.Attadnadnce_log;

public partial class AddAttLog : Window
{
    private List<Group> _groups;
    private List<Clients> _clients;
    private List<Attendance_logs> _logs;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
    
    public AddAttLog()
    {
        InitializeComponent();
        FillStud();
        FillGroup();
        FillAtt();
    }

    private void FillAtt()
    {
        /*string sql = """
                      select Lesson_date, Group_name, Client_name, Attendance from Attendance_logs
                      join language_school.`Group` G on G.Group_id = Attendance_logs.`Group`
                      join language_school.Clients C on C.Client_id = Attendance_logs.Client
                      """;
        _logs = new List<Attendance_logs>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curLog = new Attendance_logs(
                reader.GetDateOnly("Lesson_date"),
                reader.GetString("Group_name"),
                reader.GetString("Client_name"),
                reader.GetInt32("Attendance")
            );
            
            _logs.Add(curLog);
        }
        _connection.Close();
        var attComboBox = this.Find<ComboBox>("CBoxAtt");
        attComboBox.ItemsSource = _logs;
        
        */
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
        var groupComboBox = this.Find<ComboBox>("CBoxGroup");
        groupComboBox.ItemsSource = _groups;
    }

    private void FillStud()
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
        var studComboBox = this.Find<ComboBox>("CBoxStud");
        studComboBox.ItemsSource = _clients;
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _logs = new List<Attendance_logs>();
        MySqlConnection _connection;
        string sql = """
                        insert into Attendance_logs (Lesson_date, attendance_logs.Group, Client, Attendance)
                        values (@Lesson_date, @Group, @Client, @Attendance)
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Lesson_date", MySqlDbType.DateTime);
        command.Parameters["@Lesson_date"].Value = DPLesson.SelectedDate;
        command.Parameters.Add("@Group", MySqlDbType.Int32);
        command.Parameters["@Group"].Value = (CBoxGroup.SelectedItem as up.Entities.Group).Group_id;
        command.Parameters.Add("@Client", MySqlDbType.Int32);
        command.Parameters["@Client"].Value = (CBoxStud.SelectedItem as up.Entities.Clients).Client_id;
        command.Parameters.Add("@Attendance", MySqlDbType.Int32);
        command.Parameters["@Attendance"].Value = CBoxAtt.SelectedIndex;
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
    
    
}