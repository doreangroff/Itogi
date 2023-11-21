using System.Collections.Generic;
using Avalonia.Controls;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi;

public partial class Emp_infoWin : Window
{
    private List<Emp_info> _info { get; set; }
    private MySqlConnectionStringBuilder _connectionSB;
    public Emp_infoWin()
    {
        InitializeComponent();
        Info = new List<Emp_info>();

        _connectionSB = new MySqlConnectionStringBuilder
        {
            Server = "10.10.1.24",
            Database = "pro1_1",
            UserID = "user_01",
            Password = "user01pro"
        };

        ShowTable();
    }

    private void ShowTable()
    {
        string sql = """
                        select fio, post_name, department_name, working_rate, working_hours, salary, employee, post, department from emp_info
                     join pro1_1.departments d on d.department_id = emp_info.department
                     join pro1_1.employees e on e.employee_id = emp_info.employee
                     join pro1_1.posts p on p.post_id = emp_info.post
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
                        _info.Add();
                    }
                }
            }
        }
    }
}