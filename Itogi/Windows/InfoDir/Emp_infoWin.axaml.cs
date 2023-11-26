using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Itogi.Entities;
using MySqlConnector;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Itogi.Windows.EmpsDir;
using Itogi.Windows.InfoDir;

namespace Itogi;

public partial class Emp_infoWin : RabStol
{
    private List<Emp_info> _info;
    private List<Department> _departments;
    bool flag = false;

    public Emp_infoWin()
    {
        InitializeComponent();
        _info = new List<Emp_info>();
        _departments = new List<Department>();
        FillDeps();
        ShowTable();
        

    }

    public void FillDeps()
    {
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
                var depsComboBox = this.Find<ComboBox>("DepFiltr");
                depsComboBox.ItemsSource = _departments;
            }
        }
    }

    private void ShowTable()
    {
        string sql = """
                        select fio, post_name, department_name, working_rate, working_hours, salary, employee, post, department from emp_info
                     join itog.departments d on d.department_id = emp_info.department
                     join itog.employees e on e.employee_id = emp_info.employee
                     join itog.posts p on p.post_id = emp_info.post
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
                        _info.Add(new Emp_info()
                        {
                            Epmloyee = reader.GetString("fio"),
                            Post = reader.GetString("post_name"),
                            Department = reader.GetString("department_name"),
                            WorkingRate = reader.GetFloat("working_rate"),
                            WorkingHours = reader.GetInt32("working_hours"),
                            Salary = reader.GetFloat("salary"),
                            EpmloyeeID = reader.GetInt32("employee"),
                            PostID = reader.GetInt32("post"),
                            DepartmentID = reader.GetInt32("department")
                        });
                    }
                }

                con.Close();
            }
        }

        InfoGrid.ItemsSource = _info;
    }

    private void SearchInfo_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var searchTextInfo = _info.Where(x =>
                x.Epmloyee.Contains(searchInfo.Text, StringComparison.OrdinalIgnoreCase) ||
                x.Post.Contains(searchInfo.Text, StringComparison.OrdinalIgnoreCase) ||
                x.Department.Contains(searchInfo.Text, StringComparison.OrdinalIgnoreCase) ||
                x.WorkingRate.ToString() == searchInfo.Text ||
                x.WorkingHours.ToString() == searchInfo.Text ||
                x.Salary.ToString() == searchInfo.Text)
            .ToList();
        InfoGrid.ItemsSource = searchTextInfo;
        if (searchInfo is null) ShowTable();
    }

    private void SortBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (flag == false)
        {
            SortBtn.Background = Brushes.Gold;
            var sortEmps = _info;
            sortEmps = sortEmps.OrderBy(x => x.Epmloyee).ToList();
            InfoGrid.ItemsSource = sortEmps;
            flag = true;
        }
        else
        {
            SortBtn.Background = Brushes.White;
            InfoGrid.ItemsSource = _info;
            flag = false;
        }

    }
    
    private void DepFiltr_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var depComboBox = (ComboBox)sender;
        var curDep = depComboBox.SelectedItem as Department;
        var filteredInfo = _info.Where(x => x.Department == curDep.DepartmentName).ToList();
        InfoGrid.ItemsSource = filteredInfo;
    }

    private void ResetFiltr_OnClick(object? sender, RoutedEventArgs e)
    {
        SortBtn.Background = Brushes.White;
        InfoGrid.ItemsSource = _info;
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AddInfo add = new AddInfo();
        add.ShowDialog(this);
        add.Closed += delegate { ShowTable(); };
    }

    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedInfo = InfoGrid.SelectedItem as Emp_info;
        if (selectedInfo is null) return;

        DelInfo del = new DelInfo(selectedInfo);
        del.ShowDialog(this);
        del.Closed += delegate { ShowTable(); };
    }

    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedInfo = InfoGrid.SelectedItem as Emp_info;
        if (selectedInfo is null) return;

        EditInfo edit = new EditInfo(selectedInfo);
        edit.ShowDialog(this);
        edit.Closed += delegate { ShowTable(); };
    }
}