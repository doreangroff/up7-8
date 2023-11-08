using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.Groups;

public partial class DeleteGroup : Window
{
    private readonly Group _groups;
    private MySqlConnection _connection;

    private string _con = "server=localhost;database=language_school;user=user_1;password=1234;";//"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    
    public DeleteGroup(Group group)
    {
        InitializeComponent();
        _groups = group;
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from `Group` where Group_id = @Group_id LIMIT 1";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Group_id", MySqlDbType.Int32);
        command.Parameters["@Group_id"].Value = _groups.Group_id;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }
}