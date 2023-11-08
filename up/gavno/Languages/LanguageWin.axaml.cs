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
using up.gavno.Languages;
using up.gavno.Reports;
using up.gavno.Schedule;
using up.gavno.ServicePayments;
using up.gavno.Services;
using up.gavno.StudentsDir;
using up.gavno.TeachersDir;
using up.Views;

namespace up.gavno;

public partial class LanguageWin : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro"; "server=localhost;database=language_school;user=user_1;password=1234";
    private List<Entities.Languages> _languages;
    private MySqlConnection _connection;
    public LanguageWin()
    {
        InitializeComponent();
        ShowTable();
    }

    private void ShowTable()
    {
        string sql = "select * from Languages";
        _languages = new List<Entities.Languages>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curLan = new Entities.Languages(
                reader.GetInt32("Language_id"),
                reader.GetString("Lan_name")
            );
            _languages.Add(curLan);
        }
        _connection.Close();
        LanguageGrid.ItemsSource = _languages;
    }

    private void SearchClient_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Entities.Languages> LanSearch = _languages.Where(x => 
            x.Language_id.ToString() == SearchClient.Text || 
            x.Lan_name.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        LanguageGrid.ItemsSource = LanSearch;
        if (SearchClient.Text == "")
        {
            ShowTable();
        }
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddLanguage add = new AddLanguage();
        add.ShowDialog(this);
    }

    private void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = LanguageGrid.SelectedItem as Entities.Languages;
        if (myValue is null)
        {
            return;
        }
        EditLanguage del = new EditLanguage(myValue);
        del.ShowDialog(this);
        del.Closed += (o, args) =>
        {
            if (del.Result)
            {
                ShowTable();
            }
        };
    }

    private void DelClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = LanguageGrid.SelectedItem as Entities.Languages;
        if (myValue is null)
        {
            return;
        }
        DeleteLanguage del = new DeleteLanguage(myValue);
        del.ShowDialog(this);
        del.Closed += (o, args) =>
        {
            if (del.Result)
            {
                ShowTable();
            }
        };
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