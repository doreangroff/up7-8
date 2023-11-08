using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using up.Entities;
using up.ViewModels.Cousrces;

namespace up.Views.Dialogs.CourceDialogs;

public partial class DeleteCourceWin : Window
{
    public DeleteCourceWin(Courses courses)
    {
        InitializeComponent();
        DataContext = new DeleteCourceViewModel(courses);
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as DeleteCourceViewModel)!.DeleteCource();
        this.Close();
    }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}