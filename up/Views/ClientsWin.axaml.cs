using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using up.Entities;
using up.gavno;
using up.gavno.Attadnadnce_log;
using up.gavno.Groups;
using up.gavno.Reports;
using up.gavno.Schedule;
using up.gavno.ServicePayments;
using up.gavno.Services;
using up.gavno.StudentsDir;
using up.gavno.TeachersDir;
using up.ViewModels;
using up.Views;
using up.Views.Dialogs.ClientsDialogs;

namespace up;

public partial class ClientsWin : Window
{
    public ClientsViewModel ViewModel => (DataContext as ClientsViewModel)!;
    private List<Clients> _clients;
    public ClientsWin()
    {
        InitializeComponent();
        DataContext = new ClientsViewModel();
    }


    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddClientWin addClient = new AddClientWin();
        addClient.ShowDialog(this);
        
    }

    private void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        
        var MyValue = ClientsGrid.SelectedItem as Clients;
        if (MyValue is null) return;
        
        EditClientWin editClient = new EditClientWin(MyValue);
        editClient.ShowDialog(this);
        
    }

    private void DelClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = ClientsGrid.SelectedItem as Clients;
        if (myValue is null) return;
        DeleteClientWindow del = new DeleteClientWindow(myValue);
        del.ShowDialog(this);
    }

    private void OpenEditDialog()
    {
        ViewModel.OpenEditWindow();
    }

    private void CourceBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        CourceWindow cource = new CourceWindow();
        OpenWindow(cource);
    }

    private void CPaymentsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        CPaymentsWin cp = new CPaymentsWin();
        OpenWindow(cp);
    }

    private void GroupBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Groups groups = new Groups();
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
}