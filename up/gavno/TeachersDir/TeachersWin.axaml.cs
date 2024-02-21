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
using up.gavno.StudentsDir;
using up.Views;

namespace up.gavno.TeachersDir;

public partial class TeachersWin : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
                                                                                                 public List<Teachers> _teachers;
    private MySqlConnection _connection; 
    public TeachersWin()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql = "select * from Teachers";
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
        TeachersGrid.ItemsSource = _teachers;    }

    public void SearchClient_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Teachers> TeacherSearch = _teachers.Where(x => 
            x.Teacher_id.ToString() == SearchClient.Text || 
            x.Teacher_name.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        TeachersGrid.ItemsSource = TeacherSearch;
        if (SearchClient.Text == "")
        {
            ShowTable();
        }
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddTeacher add = new AddTeacher();
        add.ShowDialog(this);
    }

    public void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = TeachersGrid.SelectedItem as Teachers;
        if (myValue is null)
        {
            return;
        }
        EditTeacher del = new EditTeacher(myValue);
        del.ShowDialog(this);
        del.Closed += (o, args) =>
        {
            if (del.Result)
            {
                ShowTable();
            }
        };
    }

    public void DelClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = TeachersGrid.SelectedItem as Teachers;
        if (myValue is null)
        {
            return;
        }
        DeleteTeacher del = new DeleteTeacher(myValue);
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

    public void ClientsBtn_OnClick(object? sender, EventArgs e)
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
    public List<Teachers> FilterTeachers(string searchText)
    {
        return _teachers.Where(x => 
            x.Teacher_id.ToString() == searchText || 
            x.Teacher_name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}