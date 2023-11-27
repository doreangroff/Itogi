using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.DepartmentsDir;

public partial class AddDep : RabStol
{
    private List<Department> _departments;
    
    public AddDep()
    {
        InitializeComponent();
        _departments = new List<Department>();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                        insert into departments (department_name)
                        values (@name)
                     """;
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@name", DbType.String);
                command.Parameters["@name"].Value = TBName.Text;
                command.ExecuteNonQuery();
            }
            con.Close();
            this.Close();
        }
    }
}