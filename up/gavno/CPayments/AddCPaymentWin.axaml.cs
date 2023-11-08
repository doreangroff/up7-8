using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.CPayments;

public partial class AddCPaymentWin : Window
{
    private List<Courses> _courses;
    private List<Clients> _clients;
    private List<Months> _months;
    private List<Courses_payment> _cPayments;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
    public AddCPaymentWin()
    {
        InitializeComponent();
        FillCources();
        FillClients();
        FillMonth();
    }

    private void FillMonth()
    {
        string sql = "select * from months";
        _months = new List<Months>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curMonth = new Months(
                reader.GetInt32("Month_id"),
                reader.GetString("Month_name")
            );
            
            _months.Add(curMonth);
        }
        _connection.Close();
        var monthComboBox = this.Find<ComboBox>("CBoxMonth");
        monthComboBox.ItemsSource = _months;
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

    private void FillCources()
    {
        string sql = "select Course_id, Course_name, Teacher_name, Lan_name, cost from Courses" +
                     " join language_school.Languages l on l.Language_id = Courses.Language" +
                     " join language_school.Teachers t on t.Teacher_id = Courses.Teacher";
        _courses = new List<Courses>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curCourse = new Courses(
                reader.GetInt32("Course_id"),
                reader.GetString("Course_name"),
                reader.GetString("Teacher_name"),
                reader.GetString("Lan_name"),
                reader.GetDouble("cost")
            );
            _courses.Add(curCourse);
        }
        _connection.Close();
        var courceComboBox = this.Find<ComboBox>("CBoxCource");
        courceComboBox.ItemsSource = _courses;
    }
    
    

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _cPayments = new List<Courses_payment>();
        MySqlConnection _connection;
        string sql = """
                        insert into Courses_payment (CPayment_id, Course, Client, Month)
                        values (@CPayment_id, @Course, @Client, @Month)
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@CPayment_id", MySqlDbType.Int32);
        command.Parameters["@CPayment_id"].Value = TBxId.Text;
        command.Parameters.Add("@Course", MySqlDbType.Int32);
        command.Parameters["@Course"].Value = (CBoxCource.SelectedItem as up.Entities.Courses).Course_id;
        command.Parameters.Add("@Client", MySqlDbType.Int32);
        command.Parameters["@Client"].Value = (CBoxClient.SelectedItem as up.Entities.Clients).Client_id;
        command.Parameters.Add("@Month", MySqlDbType.Int32);
        command.Parameters["@Month"].Value = (CBoxMonth.SelectedItem as up.Entities.Months).Month_id;
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
    
    
}