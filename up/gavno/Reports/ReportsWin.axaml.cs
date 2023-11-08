using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using up.gavno.Attadnadnce_log;
using up.gavno.Schedule;
using up.gavno.ServicePayments;
using up.gavno.Services;
using up.gavno.StudentsDir;
using up.gavno.TeachersDir;
using up.Views;

namespace up.gavno.Reports;

public partial class ReportsWin : Window
{
    public ReportsWin()
    {
        InitializeComponent();
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

    private void GridBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AttendanceReport att = new AttendanceReport();
        att.ShowDialog(this);
    }

    private void GrafBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Grafik grafik = new Grafik();
        grafik.ShowDialog(this);
    }
}