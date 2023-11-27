using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.DepartmentsDir;

public partial class DepWin : RabStol
{
    private List<Department> _departments;
    
    public DepWin()
    {
        InitializeComponent();
        _departments = new List<Department>();
        ShowTable();
    }

    private void ShowTable()
    {
        _departments = new List<Department>();
        string sql = "select department_id, department_name from departments";
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _departments.Add(new Department(
                            reader.GetInt32("department_id"),
                            reader.GetString("department_name")
                        ));
                    }
                }
                con.Close();
                DepGrid.ItemsSource = _departments;
            }
        }
    }

    private void SearchDep_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var searchDepartment = _departments.Where(x =>
            x.DepartmnetId.ToString() == searchDep.Text ||
            x.DepartmentName.Contains(searchDep.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        DepGrid.ItemsSource = searchDepartment;
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AddDep add = new AddDep();
        add.ShowDialog(this);
        add.Closed += delegate(object? o, EventArgs args) { ShowTable(); };
    }

    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedDep = DepGrid.SelectedItem as Department;
        if(selectedDep is null) return;

        EditDep edit = new EditDep(selectedDep);
        edit.ShowDialog(this);
        edit.Closed += delegate { ShowTable(); };
    }

    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedDep = DepGrid.SelectedItem as Department;
        if(selectedDep is null) return;

        DelDep del = new DelDep(selectedDep);
        del.ShowDialog(this);
        del.Closed += delegate { ShowTable(); };
    }
}