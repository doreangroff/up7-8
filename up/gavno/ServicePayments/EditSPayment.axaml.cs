using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using up.Entities;

namespace up.gavno.ServicePayments;

public partial class EditSPayment : Window
{
    private readonly Services_payments _sp;
    private List<Services_payments> _sPayments;
    private List<Clients> _clients;
    private List<Entities.Services> _services;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
    //"server=localhost;database=language_school;user=user_1;password=1234";
    public EditSPayment(Services_payments sp)
    {
        InitializeComponent();
        DataContext = sp;
        _sp = sp;
        FillClients();
        FillServices();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
        
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _sPayments = new List<Services_payments>();
        MySqlConnection _connection;
        string sql = """
                        update Servicese_payments set SPayment_id = @SPayment_id,
                        Client = @Client,
                        Service = @Service    
                        where SPayment_id = @SPayment_id
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@SPayment_id", MySqlDbType.Int32);
        command.Parameters["@SPayment_id"].Value = TBxId.Text;
        command.Parameters.Add("@Client", MySqlDbType.Int32);
        command.Parameters["@Client"].Value = (CBClient.SelectedItem as up.Entities.Clients).Client_id;
        command.Parameters.Add("@Service", MySqlDbType.Int32);
        command.Parameters["@Service"].Value = (CBService.SelectedItem as up.Entities.Services).Service_id;
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
    
    private void FillClients()
    {
        string sql = "select * from Clients";
        _clients = new List<Clients>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curStud = new Clients(
                reader.GetInt32("Client_id"),
                reader.GetString("Client_name"),
                reader.GetString("Phone_number"),
                reader.GetDateTime("Birthday"),
                reader.GetString("Language_needs")
            );
            
            _clients.Add(curStud);
        }
        _connection.Close();
        var studComboBox = this.Find<ComboBox>("CBClient");
        studComboBox.ItemsSource = _clients;
        studComboBox.SelectedItem = studComboBox.ItemsSource.Cast<Clients>().First(it => it.Client_name == _sp.Client);
    }
    
    private void FillServices()
    {
        string sql = """
                     select Service_id, Service_name, Service_cost, Teacher_name from services
                     join language_school.teachers t on t.Teacher_id = services.Teacher
                     """;
        _services = new List<Entities.Services>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curService = new Entities.Services(
                reader.GetInt32("Service_id"),
                reader.GetString("Service_name"),
                reader.GetDouble("Service_cost"),
                reader.GetString("Teacher_name")
            );
            
            _services.Add(curService);
        }
        _connection.Close();
        var serviceComboBox = this.Find<ComboBox>("CBService");
        serviceComboBox.ItemsSource = _services;
        serviceComboBox.SelectedItem = serviceComboBox.ItemsSource.Cast<Entities.Services>().First(it => it.Service_name == _sp.Service);
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }
}