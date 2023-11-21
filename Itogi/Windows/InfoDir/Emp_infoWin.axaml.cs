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

namespace Itogi;

public partial class Emp_infoWin : Window
{
    private const double targetY = 940; 
    private const double moveStep = 40;
    
    private List<Emp_info> _info { get; set; }
    private MySqlConnectionStringBuilder _connectionSB;
    bool flag = false;
    public Emp_infoWin()
    {
        InitializeComponent();
        _info = new List<Emp_info>();

        _connectionSB = new MySqlConnectionStringBuilder
        {
            Server = "localhost",//"10.10.1.24" "localhost"
            Database = "itog", //"pro1_1" "itog" 
            UserID = "user_1", //"user_01" "user_1"
            Password = "1234" //"user01pro" "1234"
        };
        
        ShowTable();
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
                            Working_rate = reader.GetFloat("working_rate"),
                            Working_hours = reader.GetInt32("working_hours"),
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
        var searchTextInfo = _info.Where(x => x.Epmloyee.Contains(searchInfo.Text, StringComparison.OrdinalIgnoreCase) ||
                                              x.Post.Contains(searchInfo.Text, StringComparison.OrdinalIgnoreCase) ||
                                              x.Department.Contains(searchInfo.Text, StringComparison.OrdinalIgnoreCase) ||
                                              x.Working_rate.ToString() == searchInfo.Text ||
                                              x.Working_hours.ToString() == searchInfo.Text ||
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


    private async void EmployeesBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        await StartLoopAsync();
    }
    
    private async Task StartLoopAsync()
    {
        EmployeeWin emp = new EmployeeWin();
        double currentX = this.Position.X;
        double currentY = this.Position.Y;
        while (currentY < targetY)
        {
            MoveWindow(currentX, currentY + moveStep);
            currentY += moveStep;
            await Task.Delay(40);
        }
        emp.Show();
        this.Close();
        emp.Position = new PixelPoint(459, -460);

    }
    
    private void MoveWindow(double x, double y)
    {
        this.Position = new PixelPoint((int)x, (int)y);
    }
}