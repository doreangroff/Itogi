using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.InfoDir;

public partial class EditInfo : RabStol
{
    private readonly Emp_info _selectedInfo;
    private List<Department> _departments;
    private List<Employee> _employees;
    private List<Post> _posts;


    public EditInfo(Emp_info selectedInfo)
    {
        InitializeComponent();
        DataContext = selectedInfo;
        _selectedInfo = selectedInfo;
        _departments = new List<Department>();
        _employees = new List<Employee>();
        _posts = new List<Post>();
            
        FillDeps();
        FillEmployees();
        FiilPosts();
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
                depsComboBox.SelectedValue = _selectedInfo.DepartmentID;
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
                postComboBox.SelectedValue = _selectedInfo.PostID;
            }
        }
    }

    private void FillEmployees()
    {
        string sql = "select * from employees";
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
                            reader.GetInt32("gender"),
                            reader.GetString("phone_number")
                        ));
                    }
                }
                con.Close();
                var empComboBox = this.Find<ComboBox>("CBEmp");
                empComboBox.ItemsSource = _employees;
                empComboBox.SelectedValue = _selectedInfo.EpmloyeeID;
            }
        }
    }

    private void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                        update emp_info set employee = @emp,
                                            post = @post,
                                            department = @dep,
                                            working_hours = @hours,
                                            working_rate = @rate,
                                            salary = @salary
                        where employee = @empId and
                        post = @postId and
                        department = @depId
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
                command.Parameters.Add("@empId", DbType.Int32);
                command.Parameters["@empId"].Value = _selectedInfo.EpmloyeeID;
                command.Parameters.Add("@postId", DbType.Int32);
                command.Parameters["@postId"].Value = _selectedInfo.PostID;
                command.Parameters.Add("@depId", DbType.Int32);
                command.Parameters["@depId"].Value = _selectedInfo.DepartmentID;
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