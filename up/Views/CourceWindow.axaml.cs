using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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
using up.ViewModels.Cousrces;
using up.Views.Dialogs.CourceDialogs;

namespace up.Views;

public partial class CourceWindow : Window
{
    public CourceWindow()
    {
        InitializeComponent();
        DataContext = new CourseViewModel();
    }

    public void OpenWindow(Window window)
    {
        window.Show();
        this.Close();
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        AddCourceWin addCource = new AddCourceWin();
        addCource.ShowDialog(this);
    }

    private void EditClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = CourceGrid.SelectedItem as Courses;
        if (MyValue is null) return;
        
        EditCourceWin cource = new EditCourceWin(MyValue);
        cource.ShowDialog(this);
    }

    private void DelClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = CourceGrid.SelectedItem as Courses;
        if (myValue is null) return;
        DeleteCourceWin del = new DeleteCourceWin(myValue);
        del.ShowDialog(this);
    }

    private void ClientsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ClientsWin client = new ClientsWin();
        client.Show();
        this.Close();
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
}