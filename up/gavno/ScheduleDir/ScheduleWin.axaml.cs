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
using up.gavno.CPayments;
using up.gavno.Reports;
using up.gavno.ScheduleDir;
using up.gavno.ServicePayments;
using up.gavno.Services;
using up.gavno.StudentsDir;
using up.gavno.TeachersDir;
using up.Views;

namespace up.gavno.Schedule;

public partial class ScheduleWin : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro"; "server=localhost;database=language_school;user=user_1;password=1234";
    private List<Entities.Schedule> _schedule;
    private MySqlConnection _connection; 
    public ScheduleWin()
    {
        InitializeComponent();
        ShowTable();
    }

    private void ShowTable()
    {
        string sql = """
                     select Group_name, Day_name, Time from Schedule
                     join language_school.`Group` G on G.Group_id = Schedule.`Group`
                     join language_school.Days_of_week Dow on Dow.Day_id = Schedule.Day_of_week
                     """;
        _schedule = new List<Entities.Schedule>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curSchedule = new Entities.Schedule(
                reader.GetString("Group_name"),
                reader.GetString("Day_name"),
                reader.GetTimeOnly("Time")
            );
            
            _schedule.Add(curSchedule);
        }
        _connection.Close();
        ScheduleGrid.ItemsSource = _schedule;
    }

    private void SearchClient_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Entities.Schedule> ScheduleSearch = _schedule.Where(x => 
            x.Group.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase) ||
            x.Day_of_week.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        ScheduleGrid.ItemsSource = ScheduleSearch;
        if (SearchClient.Text == "")
        {
            ShowTable();
        }
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddSchedule add = new AddSchedule();
        add.ShowDialog(this);
    }

    private void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = ScheduleGrid.SelectedItem as Entities.Schedule;
        if (MyValue is null) return;
        
        EditSchedule edit = new EditSchedule(MyValue);
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
        var MyValue = ScheduleGrid.SelectedItem as Entities.Schedule;
        if (MyValue is null) return;
        
        DeleteSchedule edit = new DeleteSchedule(MyValue);
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
        AttLogsWin logsWin = new AttLogsWin();
        OpenWindow(logsWin);
    }
}