using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.Services;

public partial class EditService : Window
{
    private readonly Entities.Services _ser;
    private List<Entities.Services> _services;
    private List<Teachers> _teachers;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    
    public EditService(Entities.Services ser)
    {
        InitializeComponent();
        DataContext = ser;
        _ser = ser;
        FillTeachers();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _services = new List<Entities.Services>();
        MySqlConnection _connection;
        string sql = """
                        update Services set Service_id = @Service_id, 
                            Service_name = @Service_name, 
                            Service_cost = @Service_cost,
                            Teacher = @Teacher 
                            where Service_id = @Service_id
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Service_id", MySqlDbType.Int32);
        command.Parameters["@Service_id"].Value = TBxId.Text;
        command.Parameters.Add("@Service_name", MySqlDbType.Int32);
        command.Parameters["@Service_name"].Value = TBName.Text;
        command.Parameters.Add("@Service_cost", MySqlDbType.Double);
        command.Parameters["@Service_cost"].Value = TBCost.Text;
        command.Parameters.Add("@Teacher", MySqlDbType.Int32);
        command.Parameters["@Teacher"].Value = (CBTeacher.SelectedItem as up.Entities.Teachers).Teacher_id;
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
    
    private void FillTeachers()
    {
        string sql = "select Teacher_id, Teacher_name from Teachers";
        _teachers = new List<Teachers>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curTeacher = new Teachers(
                reader.GetInt32("Teacher_id"),
                reader.GetString("Teacher_name")
            );
            _teachers.Add(curTeacher);
        }
        _connection.Close();
        var teacherComboBox = this.Find<ComboBox>("CBTeacher");
        teacherComboBox.ItemsSource = _teachers;
        teacherComboBox.SelectedItem = teacherComboBox.ItemsSource.Cast<Teachers>().First(it => it.Teacher_name == _ser.Teacher);
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }
}