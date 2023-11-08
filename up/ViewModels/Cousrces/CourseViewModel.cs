using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Collections;
using MySqlConnector;
using ReactiveUI;
using up.Entities;
using up.Views;
using up.Views.Dialogs.CourceDialogs;

namespace up.ViewModels.Cousrces;

public class CourseViewModel : ViewModelBase
{
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                               //"server=localhost;database=language_school;user=user_1;password=1234";
    private AvaloniaList<Courses> _coursesFull;
    private AvaloniaList<Courses> _courses;
    private string _searchText = string.Empty;
    private Courses selectedCource;
    
    public string SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }
    
    public AvaloniaList<Courses> Cources
    {
        get => _courses;
        set => this.RaiseAndSetIfChanged(ref _courses, value);
    }

    public CourseViewModel()
    {
        ShowTable();
    }

    public void ShowTable()
    {
        string sql = "select Course_id, Course_name, Teacher_name, Lan_name, cost from Courses" +
                     " join language_school.Languages l on l.Language_id = Courses.Language" +
                     " join language_school.Teachers t on t.Teacher_id = Courses.Teacher";
        _coursesFull = new AvaloniaList<Courses>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curCourse = new Courses(
                reader.GetInt32("Course_id"),
                reader.GetString("Course_name"),
                reader.GetString("Teacher_name"),
                reader.GetString("Lan_name"),
                reader.GetDouble("cost")
            );
            _coursesFull.Add(curCourse);
        }
        _connection.Close();

        Cources = _coursesFull;
        this.WhenAnyValue(x => x.SearchText)
            .Subscribe(FilterCources);
    }
    
    
    private void FilterCources(string searchText)
    {
        var filterCources = _coursesFull.Where(x =>
            x.Course_name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            x.Course_id.ToString() == searchText).ToList();
        Cources = new AvaloniaList<Courses>(filterCources);
        
    }
    
    public void OpenEditWindow()
    {
        CourceWindow cource = new CourceWindow();
        EditCourceWin edit = new EditCourceWin(selectedCource);
        edit.DataContext = new EditCourceWin(selectedCource);
        edit.ShowDialog(cource);
    }
}