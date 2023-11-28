using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.InfoDir;

public partial class AddInfo : ConnectionWin
{
    private List<Emp_info> _info;
    private List<Employee> _employees;
    private List<Post> _posts;
    private List<Department> _departments;
    
    public AddInfo()
    {
        InitializeComponent();
        _info = new List<Emp_info>();
        _departments = new List<Department>();
        _employees = new List<Employee>();
        _posts = new List<Post>();
        FillEmployees();
        FiilPosts();
        FillDeps();
    }

    private void FillDeps()
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
                var depsComboBox = this.Find<ComboBox>("CBDep");
                depsComboBox.ItemsSource = _departments;
            }
        }
    }

    private void FiilPosts()
    {
        string sql = "select * from posts";
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
                        _posts.Add(new Post(
                            reader.GetInt32("post_id"),
                            reader.GetString("post_name")
                        ));
                    }
                }
                
                con.Close();
                var postComboBox = this.Find<ComboBox>("CBPost");
                postComboBox.ItemsSource = _posts;
            }
        }
    }

    private void FillEmployees()
    {
        string sql = "select employee_id, fio, age, gender_name, phone_number from employees " +
                     "join pro1_1.genders g on g.gender_id = employees.gender " +
                     "order by employee_id";
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
                var empComboBox = this.Find<ComboBox>("CBEmp");
                empComboBox.ItemsSource = _employees;
            }
        }
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                        insert into emp_info (employee, post, department, working_hours, working_rate, salary)
                        values (@emp, @post, @dep, @hours, @rate, @salary)
                     """;
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@emp", DbType.Int32);
                command.Parameters["@emp"].Value = (CBEmp.SelectedItem as Itogi.Entities.Employee).EmployeeId;
                command.Parameters.Add("@post", DbType.Int32);
                command.Parameters["@post"].Value = (CBPost.SelectedItem as Itogi.Entities.Post).PostId;
                command.Parameters.Add("@dep", DbType.Int32);
                command.Parameters["@dep"].Value = (CBDep.SelectedItem as Itogi.Entities.Department).DepartmnetId;
                command.Parameters.Add("@hours", DbType.Int32);
                command.Parameters["@hours"].Value = TBHours.Text;
                command.Parameters.Add("@rate", DbType.Double);
                command.Parameters["@rate"].Value = TBRate.Text;
                command.Parameters.Add("@salary", DbType.Double);
                command.Parameters["@salary"].Value = TBSalary.Text;
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