using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySqlConnector;
using OxyPlot;
using OxyPlot.Avalonia;
using OxyPlot.Axes;
using OxyPlot.Series;
using CategoryAxis = OxyPlot.Avalonia.CategoryAxis;
using LinearAxis = OxyPlot.Avalonia.LinearAxis;

namespace up.gavno.Reports;

public partial class Grafik : Window
{
    private PlotView plotView;
    public Grafik()
    {
        this.InitializeComponent();
        this.AttachDevTools();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        plotView = this.FindControl<PlotView>("graf");
        
        var plotModel = new PlotModel();
        //var cs = new columnSeries();
        
        using (var connection = new MySqlConnection("server=localhost;database=language_school;user=user_1;password=1234"))
        {
            connection.Open();
            var query = """
                        ELECT group_name, COUNT(*) AS StudentCount FROM Students
                        join language_school.`group` g on g.Group_id = Students.`Group`
                        GROUP BY `Group`
                        """;
            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var groupName = reader["Group"].ToString();
                var studentCount = Convert.ToInt32(reader["StudentCount"]);
                //cs.Items.Add(new ColumnItem(studentCount) { Label = groupName });
            }
        }
        //plotModel.Series.Add(cs);
        
        plotModel.Title = "Количество студентов в группах";
        //plotModel.Axes.Add(new CategoryAxis { Position = AxisPosition.Bottom });
       //plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Количество студентов" });
        
        plotView.Model = plotModel;

    }
    
    
}