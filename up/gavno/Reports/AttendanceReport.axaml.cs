using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.Reports;

public partial class AttendanceReport : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
    private MySqlConnection _connection;
    private List<SqlReport> _sr;
    public AttendanceReport()
    {
        InitializeComponent();
        ShowTable();
    }

    private void ShowTable()
    {
        string sql = """
                     select Client_name as Student, Sum(Attendance) as Toatal_Absences
                     from attendance_logs join language_school.clients c on c.Client_id = attendance_logs.Client
                     GROUP BY Client
                     """;
        _sr = new List<SqlReport>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curRep = new SqlReport(
                reader.GetString("Student"),
                reader.GetInt32("Toatal_Absences")
            );
            _sr.Add(curRep);
        }
        _connection.Close();
        AttReportGrid.ItemsSource = _sr;
    }
}