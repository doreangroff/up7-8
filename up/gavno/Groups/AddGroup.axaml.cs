﻿using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.Groups;

public partial class AddGroup : Window
{
    private List<Courses> _courses;
    private List<Group> _groups;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    
    public AddGroup()
    {
        InitializeComponent();
        FillCources();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
       this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _groups = new List<Group>();
        MySqlConnection _connection;
        string sql = """
                        insert into `Group` (Group_id, Group_name, Course, Stud_max_count)
                        values (@Group_id, @Group_name, @Course, @Stud_max_count)
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Group_id", MySqlDbType.Int32);
        command.Parameters["@Group_id"].Value = TBxId.Text;
        command.Parameters.Add("@Course", MySqlDbType.Int32);
        command.Parameters["@Course"].Value = (CBoxCource.SelectedItem as up.Entities.Courses).Course_id;
        command.Parameters.Add("@Group_name", MySqlDbType.Int32);
        command.Parameters["@Group_name"].Value = TBName.Text;
        command.Parameters.Add("@Stud_max_count", MySqlDbType.Int32);
        command.Parameters["@Stud_max_count"].Value = TBCount.Text;
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
    
    private void FillCources()
    {
        string sql = "select Course_id, Course_name, Teacher_name, Lan_name, cost from Courses" +
                     " join language_school.Languages l on l.Language_id = Courses.Language" +
                     " join language_school.Teachers t on t.Teacher_id = Courses.Teacher";
        _courses = new List<Courses>();
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
            _courses.Add(curCourse);
        }
        _connection.Close();
        var courceComboBox = this.Find<ComboBox>("CBoxCource");
        courceComboBox.ItemsSource = _courses;
    }
    
    
}