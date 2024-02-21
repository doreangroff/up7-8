using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.Languages;

public partial class AddLanguage : Window
{
    private List<Entities.Languages> _languages;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    
    public AddLanguage()
    {
        InitializeComponent();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _languages = new List<Entities.Languages>();
        MySqlConnection _connection;
        string sql = """
                        insert into Languages (Language_id, Lan_name)
                        values (@Language_id, @Lan_name)
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Language_id", MySqlDbType.Int32);
        command.Parameters["@Language_id"].Value = TBxId.Text;
        command.Parameters.Add("@Lan_name", MySqlDbType.String);
        command.Parameters["@Lan_name"].Value = TBName.Text;
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
}