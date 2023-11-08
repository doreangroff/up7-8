using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;
using up.ViewModels.Cousrces;

namespace up.Views.Dialogs.CourceDialogs;

public partial class AddCourceWin : Window
{
    private List<Languages> _languages;
    private List<Teachers> _teachers;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                               //"server=localhost;database=language_school;user=user_1;password=1234";
    public AddCourceWin()
    {
        InitializeComponent();
        DataContext = new AddCourceViewModel();
        
    }

    

    private void FillTeacher()
    {
        throw new System.NotImplementedException();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as AddCourceViewModel)!.AddCource();
        this.Close();
    }
}