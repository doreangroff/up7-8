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
using up.gavno.TeachersDir;
using up.Views;

namespace up.gavno.Groups;

public partial class Groups : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro"; "server=localhost;database=language_school;user=user_1;password=1234";
    private List<Group> _group;
    private MySqlConnection _connection; 
    public Groups()
    {
        InitializeComponent();
        ShowTable();
        
    }

    private void SearchClient_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Group> GroupSearch = _group.Where(x => 
            x.Group_id.ToString() == SearchClient.Text || 
            x.Course.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase) ||
            x.Group_name.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase) ||
            x.Stud_max_count.ToString() == SearchClient.Text).ToList();
        GroupGrid.ItemsSource = GroupSearch;
        if (SearchClient.Text == "")
        {
            ShowTable();
        }
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddGroup addGroup = new AddGroup();
        addGroup.ShowDialog(this);
    }

    private void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = GroupGrid.SelectedItem as Group;
        if (MyValue is null) return;
        
        EditGroup edit = new EditGroup(MyValue);
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
        var MyValue = GroupGrid.SelectedItem as Group;
        if (MyValue is null) return;
        
        DeleteGroup edit = new DeleteGroup(MyValue);
        edit.ShowDialog(this);
        edit.Closed += (o, args) =>
        {
            if (edit.Result)
            {
                ShowTable();
            }
        };
    }
    

    public void ShowTable()
    {
        string sql = """
                     select Group_id, Group_name, Course_name, Stud_max_count from `Group`
                     join language_school.Courses C on C.Course_id = `Group`.Course
                     """;
        _group = new List<Group>();
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
            
            _group.Add(curGroup);
        }
        _connection.Close();
        GroupGrid.ItemsSource = _group;
    }

    private void CPaymentsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        CPaymentsWin cp = new CPaymentsWin();
        OpenWindow(cp);
    }
    
    private void CourceBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        CourceWindow cource = new CourceWindow();
        OpenWindow(cource);
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

    private void AttendanceBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AttLogsWin logs = new AttLogsWin();
        OpenWindow(logs);
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
    
    public void OpenWindow(Window window)
    {
        window.Show();
        this.Close();
    }

    private void ClientsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ClientsWin client = new ClientsWin();
        OpenWindow(client);
    }
}