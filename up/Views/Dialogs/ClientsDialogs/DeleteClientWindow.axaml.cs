using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using up.Entities;
using up.ViewModels;

namespace up.Views.Dialogs.ClientsDialogs;

public partial class DeleteClientWindow : Window
{
    public DeleteClientWindow(Clients clients)
    {
        InitializeComponent();
        DataContext = new DeleteClientViewModel(clients);
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as DeleteClientViewModel)!.DeleteClient();
        this.Close();
       
    }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
       this.Close();
    }
}