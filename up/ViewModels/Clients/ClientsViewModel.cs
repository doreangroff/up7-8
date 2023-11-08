using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Collections;
using MySqlConnector;
using ReactiveUI;
using up.Entities;
using up.Views.Dialogs.ClientsDialogs;

namespace up.ViewModels;

public class ClientsViewModel : ViewModelBase
{
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                               //"server=localhost;database=language_school;user=user_1;password=1234";
    private AvaloniaList<Clients> _clientsFull;
    private AvaloniaList<Clients> _clients;
    private string _searchText = string.Empty;
    private Clients selectedClient;

    //public ICommand EditCommand { get; }  
        
    public string SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    public AvaloniaList<Clients> Clients
    {
        get => _clients;
        set => this.RaiseAndSetIfChanged(ref _clients, value);
    }

    public ClientsViewModel()
    {
        ShowTable();
        //EditCommand = ReactiveCommand.Create(OpenEditWindow);
    }

    public Clients SelectedClient
    {
        get => selectedClient;
        set => this.RaiseAndSetIfChanged(ref selectedClient, value);
    }
    
    private void FilterClients(string searchText)
    {
        var filterClients = _clientsFull.Where(x =>
                                               x.Client_name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                               x.Client_id.ToString() == searchText).ToList();
        Clients = new AvaloniaList<Clients>(filterClients);
        
    }

    public void ShowTable()
    {
        string sql = "select Client_id, Client_name, Phone_number, Birthday, Language_needs from Clients";
        _clientsFull = new AvaloniaList<Clients>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curClient = new Clients(
                reader.GetInt32("Client_id"),
                reader.GetString("Client_name"),
                reader.GetString("Phone_number"),
                reader.GetDateTime("Birthday"),
                reader.GetString("Language_needs")
            );
            _clientsFull.Add(curClient);
        }
        _connection.Close();

        Clients = _clientsFull;

        this.WhenAnyValue(x => x.SearchText)
            .Subscribe(FilterClients);
    }

    
    public void OpenEditWindow()
    {
        ClientsWin client = new ClientsWin();
        EditClientWin edit = new EditClientWin(selectedClient);
        edit.DataContext = new EditCleintViewModel(selectedClient);
        edit.ShowDialog(client);
    }
    
}