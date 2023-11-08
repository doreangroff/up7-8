using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using up.Entities;
using up.ViewModels;

namespace up.Views.Dialogs.ClientsDialogs;

public partial class EditClientWin : Window
{
    
    public EditClientWin(Clients clients)
    {
        InitializeComponent();
        DataContext = new EditCleintViewModel(clients);
        DPBirthday.SelectedDate = clients.Birthday;
        
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as EditCleintViewModel)!.EditClient();
        ClientsWin client = new ClientsWin();
        (client.DataContext as ClientsViewModel)!.ShowTable();
        this.Close();
    }
}