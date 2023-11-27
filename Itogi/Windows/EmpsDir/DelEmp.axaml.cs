using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.EmpsDir;

public partial class DelEmp : RabStol
{
    private readonly Employee _selectedEmployee;
    
    public DelEmp(Employee selectedEmployee)
    {
        InitializeComponent();
        _selectedEmployee = selectedEmployee;
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                        SET FOREIGN_KEY_CHECKS=0;
                        Delete from employees where employee_id = @id limit 1
                     """;
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@id", DbType.Int32);
                command.Parameters["@id"].Value = _selectedEmployee.EmployeeId;
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