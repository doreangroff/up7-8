using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using up.Entities;
using up.ViewModels;
using up.ViewModels.Cousrces;

namespace up.Views.Dialogs.CourceDialogs;

public partial class EditCourceWin : Window
{
    public EditCourceWin(Courses courses)
    {
        InitializeComponent();
        DataContext = new EditCourceViewModel(courses);
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as EditCourceViewModel)!.EditCource();
        CourceWindow cource = new CourceWindow();
        (cource.DataContext as CourseViewModel)!.ShowTable();
        this.Close();
    }
}