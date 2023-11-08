using System.Data;
using Avalonia.Collections;
using MySqlConnector;
using ReactiveUI;
using up.Entities;

namespace up.ViewModels.Cousrces;

public class DeleteCourceViewModel : ViewModelBase
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
    private MySqlConnection _connection;
    private AvaloniaList<Courses> _courses;
    private int _cource_id;

    public int Cource_id
    {
        get => _cource_id;
        set => this.RaiseAndSetIfChanged(ref _cource_id, value);
    }
    
    public DeleteCourceViewModel(Courses selectedCource)
    {
        Cource_id = selectedCource.Course_id;
    }
    
    public void DeleteCource()
    {
        string sql = """
                     SET FOREIGN_KEY_CHECKS=0;
                     Delete from Courses where Course_id = @Course_id LIMIT 1;
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Course_id", DbType.Int32);
        command.Parameters["@Course_id"].Value = _cource_id;
        command.ExecuteNonQuery();
        _connection.Close();
    }
}