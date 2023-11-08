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
using up.gavno.Schedule;
using up.gavno.ServicePayments;
using up.gavno.Services;
using up.gavno.StudentsDir;
using up.gavno.TeachersDir;
using up.Views;

namespace up.gavno;

public partial class CPaymentsWin : Window
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
    private List<Courses_payment> _cPayments;
    private MySqlConnection _connection; 
    public CPaymentsWin()
    {
        InitializeComponent();
        ShowTable();
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddCPaymentWin add = new AddCPaymentWin();
        add.ShowDialog(this);
    }

    private void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = CPaymnentsGrid.SelectedItem as Courses_payment;
        if (MyValue is null) return;
        
        EditCPayment edit = new EditCPayment(MyValue);
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
        var myValue = CPaymnentsGrid.SelectedItem as Courses_payment;
        if (myValue is null)
        {
            return;
        }
        DeleteCPayment del = new DeleteCPayment(myValue);
        del.ShowDialog(this);
        del.Closed += (o, args) =>
        {
            if (del.Result)
            {
                ShowTable();
            }
        };
    }
    

    public void ShowTable()
    {
        string sql = """
                     select CPayment_id, Course_name, Client_name, Month_name from Courses_payment
                         join language_school.Courses C on C.Course_id = Courses_payment.Course
                        join language_school.Clients C2 on C2.Client_id = Courses_payment.Client
                        join language_school.Months M on M.Month_id = Courses_payment.Month
                     """;
        _cPayments = new List<Courses_payment>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curCP = new Courses_payment(
                reader.GetInt32("CPayment_id"),
                reader.GetString("Course_name"),
                reader.GetString("Client_name"),
                reader.GetString("Month_name")
                );
            
            _cPayments.Add(curCP);
        }
        _connection.Close();
        CPaymnentsGrid.ItemsSource = _cPayments;
    }

    private void SearchClient_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Courses_payment> CPSearch = _cPayments.Where(x => 
            x.CPayment.ToString() == SearchClient.Text || 
            x.Course.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase) ||
            x.Client.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase) ||
            x.Month.Contains(SearchClient.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        CPaymnentsGrid.ItemsSource = CPSearch;
        if (SearchClient.Text == "")
        {
            ShowTable();
        }
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