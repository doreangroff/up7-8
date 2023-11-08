using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;
using up.gavno.Reports;
using up.gavno.Schedule;
using up.gavno.ServicePayments;
using up.gavno.Services;
using up.gavno.StudentsDir;
using up.gavno.TeachersDir;
using up.Views;

namespace up.gavno.Attadnadnce_log;

public partial class AttLogsWin : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
    private List<Attendance_logs> _logs;
    private MySqlConnection _connection;
    public AttLogsWin()
    {
        InitializeComponent();
        ShowTable();
    }

    private void ShowTable()
    {
        string sql = """
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
        AttendanceGrid.ItemsSource = _logs;
    }

    private void SearchClient_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Attendance_logs> LogSearch = _logs.Where(x => 
            x.Att.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase) || 
            x.Group.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase) ||
            x.Client.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        AttendanceGrid.ItemsSource = LogSearch;
        if (SearchClient.Text == "")
        {
            ShowTable();
        }
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddAttLog att = new AddAttLog();
        att.ShowDialog(this);
    }

    private void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = AttendanceGrid.SelectedItem as Attendance_logs;
        if (MyValue is null) return;
        
        EditAttLog edit = new EditAttLog(MyValue);
        edit.ShowDialog(this);
        edit.Closed += (o, args) =>
        {
            if (edit.Result)
            {
                ShowTable();
            }
        };
    }

    private void DelClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = AttendanceGrid.SelectedItem as Attendance_logs;
        if (myValue is null)
        {
            return;
        }
        DeleteAttLog del = new DeleteAttLog(myValue);
        del.ShowDialog(this);
        del.Closed += (o, args) =>
        {
            if (del.Result)
            {
                ShowTable();
            }
        };
    }
    
    public void OpenWindow(Window window)
    {
        window.Show();
        this.Close();
    }

    private void CourceBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        CourceWindow cource = new CourceWindow();
        OpenWindow(cource);
    }

    private void GroupBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Groups.Groups groups = new Groups.Groups();
        OpenWindow(groups);
    }

    private void LanguageBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        LanguageWin language = new LanguageWin();
        OpenWindow(language);
    }

    private void ScheduleBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ScheduleWin schedule = new ScheduleWin();
        OpenWindow(schedule);
    }
    
    private void ServiceBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ServicesWin service = new ServicesWin();
        OpenWindow(service);    
    }

    private void SPaymentsBtn_OnClickPaymentsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        SPaymentsWin sp = new SPaymentsWin();
        OpenWindow(sp); 
    }

    private void TeacherBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        TeachersWin teacher = new TeachersWin();
        OpenWindow(teacher);
    }

    private void StudentsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        StudentsWin stud = new StudentsWin();
        OpenWindow(stud);
    }

    private void ReportsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ReportsWin report = new ReportsWin();
        OpenWindow(report);
    }

    private void ClientsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ClientsWin client = new ClientsWin();
        OpenWindow(client);
    }

    private void CPaymentsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        CPaymentsWin cp = new CPaymentsWin();
        OpenWindow(cp);
    }
}