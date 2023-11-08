using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;
using up.gavno.Attadnadnce_log;
using up.gavno.Reports;
using up.gavno.Schedule;
using up.gavno.ServicePayments;
using up.gavno.Services;
using up.gavno.TeachersDir;
using up.Views;

namespace up.gavno.StudentsDir;

public partial class StudentsWin : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
    private List<Students> _students;
    private MySqlConnection _connection;
    public StudentsWin()
    {
        InitializeComponent();
        ShowTable();
    }

    private void ShowTable()
    {
        string sql = """
                     select Client_name, Group_name from students
                     join language_school.`group` g on g.Group_id = students.`Group`
                     join language_school.clients c on c.Client_id = students.Client
                     """;
        _students = new List<Students>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curStud = new Students(
                reader.GetString("Client_name"),
                reader.GetString("Group_name")
            );
            
            _students.Add(curStud);
        }
        _connection.Close();
        StudentGrid.ItemsSource = _students;
    }

    private void SearchClient_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Students> StudSearch = _students.Where(x => 
            x.Client.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase) || 
            x.Group.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        StudentGrid.ItemsSource = StudSearch;
        if (SearchClient.Text == "")
        {
            ShowTable();
        }
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddStudent add = new AddStudent();
        add.ShowDialog(this);
    }

    private void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = StudentGrid.SelectedItem as Students;
        if (MyValue is null) return;
        
        EditStudent edit = new EditStudent(MyValue);
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
        var MyValue = StudentGrid.SelectedItem as Students;
        if (MyValue is null) return;
        
        DeleteStudent edit = new DeleteStudent(MyValue);
        edit.ShowDialog(this);
        edit.Closed += (o, args) =>
        {
            if (edit.Result)
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

    private void AttendanceBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AttLogsWin logs = new AttLogsWin();
        OpenWindow(logs);
    }
}