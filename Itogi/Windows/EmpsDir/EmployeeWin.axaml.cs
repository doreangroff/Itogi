using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using Itogi.Entities;
using Itogi.Windows.InfoDir;
using MySqlConnector;

namespace Itogi.Windows.EmpsDir;

public partial class EmployeeWin : ConnectionWin
{
    private List<Employee> _employees;
    private List<Gender> _genders;
    bool flag = false;
    
    public EmployeeWin()
    {
        InitializeComponent();
        _employees = new List<Employee>();
        _genders = new List<Gender>();
        ShowTable();
        FillGenders();

    }
    
    public void FillGenders()
    {
        string sql = "select * from genders";
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
                        _genders.Add(new Gender(
                            reader.GetInt32("gender_id"),
                            reader.GetString("gender_name")
                        ));
                    }
                }
                con.Close();
                var genComboBox = this.Find<ComboBox>("GenderFiltr");
                genComboBox.ItemsSource = _genders;
            }
        }
    }

    public void ShowTable()
    {
        _employees = new List<Employee>();
        string sql = """
                        select employee_id, fio, age, gender_name, phone_number from employees
                        join pro1_1.genders g on g.gender_id = employees.gender 
                        order by employee_id
                     """;
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
                        _employees.Add(new Employee(
                            reader.GetInt32("employee_id"),
                            reader.GetString("fio"),
                            reader.GetInt32("age"),
                            reader.GetString("gender_name"),
                            reader.GetString("phone_number")
                            ));
                    }
                }
                con.Close();
            }
        }

        EmpGrid.ItemsSource = _employees;
    }

    private void SearchEmp_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var searchEmployee = _employees.Where(x => 
            x.EmployeeId.ToString() == searchEmp.Text ||
            x.FIO.Contains(searchEmp.Text, StringComparison.OrdinalIgnoreCase) || 
            x.PhoneNumber.Contains(searchEmp.Text, StringComparison.OrdinalIgnoreCase) ||
            x.Age.ToString() == searchEmp.Text).ToList();
        EmpGrid.ItemsSource = searchEmployee;
        if (searchEmp is null) ShowTable();
    }

    private void SortBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (flag == false)
        {
            SortBtn.Background = Brushes.Gold;
            var sortEmps = _employees;
            sortEmps = sortEmps.OrderBy(x => x.FIO).ToList();
            EmpGrid.ItemsSource = sortEmps;
            flag = true;
        }
        else
        {
            SortBtn.Background = Brushes.White;
            EmpGrid.ItemsSource = _employees;
            flag = false;
        }
        
    }

    private void GenderFiltr_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var genComboBox = (ComboBox)sender;
        var curGen = genComboBox.SelectedItem as Gender;
        var filteredEmps = _employees.Where(x => x.Gender == curGen.GenderName).ToList();
        EmpGrid.ItemsSource = filteredEmps;
    }

    private void ResetFiltr_OnClick(object? sender, RoutedEventArgs e)
    {
        SortBtn.Background = Brushes.White;
        EmpGrid.ItemsSource = _employees;
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AddEmp add = new AddEmp();
        add.ShowDialog(this);
        add.Closed += delegate { ShowTable(); };
    }

    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedEmp = EmpGrid.SelectedItem as Employee;
        if(selectedEmp is null) return;

        EditEmp edit = new EditEmp(selectedEmp);
        edit.ShowDialog(this);
        edit.Closed += delegate { ShowTable(); };
    }

    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedEmp = EmpGrid.SelectedItem as Employee;
        if(selectedEmp is null) return;

        DelEmp del = new DelEmp(selectedEmp);
        del.ShowDialog(this);
        del.Closed += delegate { ShowTable(); };
    }
}