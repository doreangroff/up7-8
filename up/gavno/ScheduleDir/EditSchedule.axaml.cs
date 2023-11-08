using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.ScheduleDir;

public partial class EditSchedule : Window
{
    private readonly Entities.Schedule _sch;
    private List<Entities.Schedule> _schedules;
    private List<Group> _groups;
    private List<Days_of_week> _days;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    
    public EditSchedule(Entities.Schedule sch)
    {
        InitializeComponent();
        DataContext = sch;
        _sch = sch;
        //Picker.SelectedTime = sch.Time;
        FillDays();
        FillGroup();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _schedules = new List<Entities.Schedule>();
        MySqlConnection _connection;
        string sql = """
                        update Schedule set `Group` = @Group, 
                        Day_of_week = @Day_of_week, 
                        Time = @Time
                        where `Group` = @Group and Day_of_week = @Day_of_week
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Group", MySqlDbType.Int32);
        command.Parameters["@Group"].Value = (CBGroup.SelectedItem as up.Entities.Group).Group_id;
        command.Parameters.Add("@Day_of_week", MySqlDbType.Int32);
        command.Parameters["@Day_of_week"].Value = (CBDay.SelectedItem as up.Entities.Days_of_week).Day_id;
        command.Parameters.Add("@Time", MySqlDbType.Time);
        command.Parameters["@Time"].Value = Picker.SelectedTime;
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
    
    private void FillDays()
    {
        string sql = "select * from Days_of_week";
        _days = new List<Days_of_week>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curDay = new Days_of_week(
                reader.GetInt32("Day_id"),
                reader.GetString("Day_name")
            );

            _days.Add(curDay);
        }

        _connection.Close();
        var daysComboBox = this.Find<ComboBox>("CBDay");
        daysComboBox.ItemsSource = _days;
        daysComboBox.SelectedItem = daysComboBox.ItemsSource.Cast<Days_of_week>().First(it => it.Day_name == _sch.Day_of_week);
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
        groupComboBox.SelectedItem = groupComboBox.ItemsSource.Cast<Group>().First(it => it.Group_name == _sch.Group);
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }
    public bool Result { get; private set; }
}