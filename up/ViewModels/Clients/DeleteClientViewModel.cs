using System;
using System.Data;
using Avalonia.Collections;
using MySqlConnector;
using ReactiveUI;
using up.Entities;

namespace up.ViewModels;

public class DeleteClientViewModel : ViewModelBase
{
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                               //"server=localhost;database=language_school;user=user_1;password=1234";
    private AvaloniaList<Clients> _clients;
    private int _client_id;

    public int Client_id
    {
        get => _client_id;
        set => this.RaiseAndSetIfChanged(ref _client_id, value);
    }
    
    public DeleteClientViewModel(Clients selectedClient)
    {
        Client_id = selectedClient.Client_id;
    }
    
    public void DeleteClient()
    {
        string sql = """
                     SET FOREIGN_KEY_CHECKS=0;
                     Delete from Clients where Client_id = @Client_id LIMIT 1;
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Client_id", DbType.Int32);
        command.Parameters["@Client_id"].Value = _client_id;
        command.ExecuteNonQuery();
        _connection.Close();
    }
}
