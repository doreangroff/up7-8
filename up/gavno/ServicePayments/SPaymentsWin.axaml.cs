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
using up.gavno.Services;
using up.gavno.StudentsDir;
using up.gavno.TeachersDir;
using up.Views;

namespace up.gavno.ServicePayments;

public partial class SPaymentsWin : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro"; "server=localhost;database=language_school;user=user_1;password=1234";
    private List<Services_payments> _sPayments;
    private MySqlConnection _connection;
    public SPaymentsWin()
    {
        InitializeComponent();
        ShowTable();
    }

    private void ShowTable()
    {
        string sql = """
                     select SPayment_id, Client_name, Service_name from Servicese_payments
                     join language_school.Clients C on C.Client_id = Servicese_payments.Client
                     join language_school.Services S on S.Service_id = Servicese_payments.Service
                     """;
        _sPayments = new List<Services_payments>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curSP = new Services_payments(
                reader.GetInt32("SPayment_id"),
                reader.GetString("Client_name"),
                reader.GetString("Service_name")
            );
            
            _sPayments.Add(curSP);
        }
        _connection.Close();
        SPaymnentsGrid.ItemsSource = _sPayments;    
    }

    private void SearchClient_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Services_payments> SPSearch = _sPayments.Where(x => 
            x.SPayment.ToString() == SearchClient.Text || 
            x.Client.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase) ||
            x.Service.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        SPaymnentsGrid.ItemsSource = SPSearch;
        if (SearchClient.Text == "")
        {
            ShowTable();
        }
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddSPayments add = new AddSPayments();
        add.ShowDialog(this);
    }

    private void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = SPaymnentsGrid.SelectedItem as Services_payments;
        if (myValue is null)
        {
            return;
        }
        EditSPayment del = new EditSPayment(myValue);
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
        var myValue = SPaymnentsGrid.SelectedItem as Services_payments;
        if (myValue is null)
        {
            return;
        }
        DeleteSPayment del = new DeleteSPayment(myValue);
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

    private void AttendanceBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AttLogsWin logs = new AttLogsWin();
        OpenWindow(logs);
    }
}