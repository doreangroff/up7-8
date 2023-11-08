using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;
using up.ViewModels;

namespace up.Views.Dialogs.ClientsDialogs;

public partial class AddClientWin : Window
{
    private List<Languages> _languages;
    private List<Language_levels> _levels;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                               //"server=localhost;database=language_school;user=user_1;password=1234";

    public AddClientWin()
    {
        InitializeComponent();
        DataContext = new AddClientViewModel();
        
        
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as AddClientViewModel)!.AddClient();
        this.Close();
        
    }

    private void Experience_OnChecked(object? sender, RoutedEventArgs e)
    {
        CBLanguage.IsVisible = true;
        CBLevel.IsVisible = true;
    }
    
    

    

    private void Experience_OnUnchecked(object? sender, RoutedEventArgs e)
    {
        CBLanguage.IsVisible = false;
        CBLevel.IsVisible = false;
    }
}