using System;
using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.DepartmentsDir;

public partial class EditDep : RabStol
{
    private readonly Department _selectedDepartment;
    private List<Department> _departments;
    
    public EditDep(Department selectedDepartment)
    {
        InitializeComponent();
        DataContext = selectedDepartment;
        _selectedDepartment = selectedDepartment;
        _departments = new List<Department>();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = "update departments set department_name = @name where department_id = @id";
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@name", DbType.String);
                command.Parameters["@name"].Value = TBName.Text;
                command.Parameters.Add("@id", DbType.Int32);
                command.Parameters["@id"].Value = _selectedDepartment.DepartmnetId;
                command.ExecuteNonQuery();
            }
            con.Close();
            this.Close();
        }
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}