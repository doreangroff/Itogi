using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.DepartmentsDir;

public partial class DelDep : ConnectionWin
{
    private readonly Department _selectedDepartment;
    
    public DelDep(Department selectedDepartment)
    {
        InitializeComponent();
        _selectedDepartment = selectedDepartment;
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                        SET FOREIGN_KEY_CHECKS=0;
                        Delete from departments where department_id = @id limit 1
                     """;
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@id", DbType.Int32);
                command.Parameters["@id"].Value = _selectedDepartment.DepartmnetId;
                command.ExecuteNonQuery();
            }
            con.Close();
            this.Close();
        }
    }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}