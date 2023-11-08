using System;
using Avalonia.Collections;
using MySqlConnector;
using ReactiveUI;
using up.Entities;

namespace up.ViewModels;

public class EditCleintViewModel : ViewModelBase
{
    private MySqlConnection _connection;

    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
    private AvaloniaList<Clients> _clients;
    private int? _tbid;
    private string _name;
    private string _phone;
    private DateTime _birthday;
    private string _needs;

    public EditCleintViewModel(Clients selectedClient)
    {
        TBId = selectedClient.Client_id;
        Name = selectedClient.Client_name;
        Phone = selectedClient.Phone_number;
        Birthday = selectedClient.Birthday;
        Needs = selectedClient.Language_needs;
    }

    public AvaloniaList<Clients> Clients
    {
        get => _clients;
        set => this.RaiseAndSetIfChanged(ref _clients, value);
    }

    public int? TBId
    {
        get => _tbid;
        set => this.RaiseAndSetIfChanged(ref _tbid, value);
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string Phone
    {
        get => _phone;
        set => this.RaiseAndSetIfChanged(ref _phone, value);
    }

    public DateTime Birthday
    {
        get => _birthday;
        set => this.RaiseAndSetIfChanged(ref _birthday, value);
    }

    public string Needs
    {
        get => _needs;
        set => this.RaiseAndSetIfChanged(ref _needs, value);
    }


    public void EditClient()
    {
        MySqlConnection _connection;
        string sql = """
                     update Clients 
                     set Client_id = @Client_id,
                        Client_name = @Client_name,
                        Phone_number = @Phone_number,
                        Birthday = @Birthday,
                        Language_needs = @Language_needs 
                     where Client_id = @Client_id
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Client_id", MySqlDbType.Int32);
        command.Parameters["@Client_id"].Value = TBId;
        command.Parameters.Add("@Client_name", MySqlDbType.String);
        command.Parameters["@Client_name"].Value = Name;
        command.Parameters.Add("@Phone_number", MySqlDbType.String);
        command.Parameters["@Phone_number"].Value = Phone;
        command.Parameters.Add("@Birthday", MySqlDbType.DateTime);
        command.Parameters["@Birthday"].Value = Birthday;
        command.Parameters.Add("@Language_needs", MySqlDbType.String);
        command.Parameters["@Language_needs"].Value = Needs;
        command.ExecuteNonQuery();
        _connection.Close();
        
    }
}