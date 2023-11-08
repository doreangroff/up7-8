using Avalonia.Collections;
using MySqlConnector;
using ReactiveUI;
using up.Entities;

namespace up.ViewModels.Cousrces;

public class EditCourceViewModel : ViewModelBase
{
    private string _con = "server=localhost;database=language_school;user=user_1;password=1234"; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";
                                                                                                 //"server=localhost;database=language_school;user=user_1;password=1234";
    private AvaloniaList<Courses> _cousrces; 
    private AvaloniaList<Languages> _languagesList;
    private AvaloniaList<Teachers> _teachersList;
    private int? _tbid;
    private string _name;
    private int _teacher;
    private Languages _selectedLanguage;
    private Teachers _selectedTeacher;
    private double? _cost;
    private MySqlConnection _connection;

    public EditCourceViewModel(Courses selectedCource)
    {
        FillLan();
        FillTeacher();
        TBId = selectedCource.Course_id;
        Name = selectedCource.Course_name;
        SelectedLanguage.Lan_name = selectedCource.Language;
        SelectedTeacher.Teacher_name = selectedCource.Teacher;
        Cost = selectedCource.Cost;
    }
    
    public AvaloniaList<Courses> Cources
    {
        get => _cousrces;
        set => this.RaiseAndSetIfChanged(ref _cousrces, value);
    }
    
    public AvaloniaList<Languages> LanguagesList
    {
        get => _languagesList;
        set => this.RaiseAndSetIfChanged(ref _languagesList, value);
    }

    public AvaloniaList<Teachers> TeachersList
    {
        get => _teachersList;
        set => this.RaiseAndSetIfChanged(ref _teachersList, value);
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

    public int Teacher
    {
        get => _teacher;
        set => this.RaiseAndSetIfChanged(ref _teacher, value);
    }

    public Languages SelectedLanguage
    {
        get => _selectedLanguage;
        set => this.RaiseAndSetIfChanged(ref _selectedLanguage, value);
    }

    public Teachers SelectedTeacher
    {
        get => _selectedTeacher;
        set => this.RaiseAndSetIfChanged(ref _selectedTeacher, value);
    }

    public double? Cost
    {
        get => _cost;
        set => this.RaiseAndSetIfChanged(ref _cost, value);
    }
    
    private void FillLan()
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
    
    private void FillTeacher()
    {
        string sql = "select Teacher_id, Teacher_name from Teachers";
        _teachersList = new AvaloniaList<Teachers>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curTeacher = new Teachers(
                reader.GetInt32("Teacher_id"),
                reader.GetString("Teacher_name")
            );
            _teachersList.Add(curTeacher);
        }
        _connection.Close();
    }
    
    public void EditCource()
    {
        MySqlConnection _connection;
        string sql = """
                     update Courses
                     set Course_id = @Course_id,
                        Course_name = @Course_name,
                        Teacher = @Teacher,
                        Language = @Language,
                        Cost = @Cost
                     where Course_id = @Course_id
                     """;
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Course_id", MySqlDbType.Int32);
        command.Parameters["@Course_id"].Value = TBId;
        command.Parameters.Add("@Course_name", MySqlDbType.String);
        command.Parameters["@Course_name"].Value = Name;
        command.Parameters.Add("@Teacher", MySqlDbType.Int32);
        command.Parameters["@Teacher"].Value = SelectedTeacher.Teacher_id;
        command.Parameters.Add("@Language", MySqlDbType.Int32);
        command.Parameters["@Language"].Value = SelectedLanguage.Language_id;
        command.Parameters.Add("@Cost", MySqlDbType.Double);
        command.Parameters["@Cost"].Value = Cost;
        command.ExecuteNonQuery();
        _connection.Close();
        
    }
}