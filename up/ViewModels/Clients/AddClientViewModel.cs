using System;
using System.Collections.Generic;
using Avalonia.Collections;
using Avalonia.Controls;
using HarfBuzzSharp;
using MySqlConnector;
using ReactiveUI;
using up.Entities;

namespace up.ViewModels;

public class AddClientViewModel : ViewModelBase
{
    private MySqlConnection _connection;
    private string _con ="server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                              //"server=localhost;database=language_school;user=user_1;password=1234";
    private AvaloniaList<Clients> _clients;
    private int? _tbid;
    private string _name;
    private string _phone;
    private DateOnly _birthday;
    private string _needs;
    private string _language;
    private string _level;
    private AvaloniaList<Languages> _languagesList;
    private Languages _selectedLanguage;
    private AvaloniaList<Language_levels> _levelsList;
    private Language_levels _selectedLevel;

    public AddClientViewModel()
    {
        FillLan();
        FillLevels();
    }

    public Language_levels Selectedlevel
    {
        get => _selectedLevel;
        set => this.RaiseAndSetIfChanged(ref _selectedLevel, value);
    }
    public AvaloniaList<Language_levels> LevelsList
    {
        get => _levelsList;
        set => this.RaiseAndSetIfChanged(ref _levelsList, value);
    }
    
    public Languages SelectedLanguage
    {
        get => _selectedLanguage;
        set => this.RaiseAndSetIfChanged(ref _selectedLanguage, value);
    }
    public AvaloniaList<Languages> LanguagesList
    {
        get => _languagesList;
        set => this.RaiseAndSetIfChanged(ref _languagesList, value);
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
    
    public DateOnly Birthday
    {
        get => _birthday;
        set => this.RaiseAndSetIfChanged(ref _birthday, value);
    }
    
    public string Needs
    {
        get => _needs;
        set => this.RaiseAndSetIfChanged(ref _needs, value);
    }
    
    public string Language
    {
        get => _language;
        set => this.RaiseAndSetIfChanged(ref _language, value);
    }

    public void AddClient()
    {
        _clients = new AvaloniaList<Clients>();
        MySqlConnection _connection;
        string sql1 = "insert into Clients (Client_id, Client_name, Phone_number, Birthday, Language_needs)" +
                     " values (@Client_id, @Client_name, @Phone_number, @Birthday, @Language_needs)";
        string sql2 = "";
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql1, _connection);
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
    
    public void FillLan()
    {
        string sql = "select Language_id, Lan_name from Languages";
        _languagesList = new AvaloniaList<Languages>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curLan = new Languages(
                reader.GetInt32("Language_id"),
                reader.GetString("Lan_name")
            );
            _languagesList.Add(curLan);
        }
        _connection.Close();
    }
    
    public void FillLevels()
    {
        string sql = "select Level_id, Level_name from Language_levels";
        _levelsList = new AvaloniaList<Language_levels>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curLevel = new Language_levels(
                reader.GetInt32("Level_id"),
                reader.GetString("Level_name")
            );
            _levelsList.Add(curLevel);
        }
        _connection.Close();
    }
}