using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.InfoDir;

public partial class DelInfo : ConnectionWin
{
    private readonly Emp_info _selectedInfo;
    
    public DelInfo(Emp_info selectedInfo)
    {
        InitializeComponent();
        _selectedInfo = selectedInfo;
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                        SET FOREIGN_KEY_CHECKS=0;
                        Delete from emp_info where employee = @emp and
                        post = @post and
                        department = @dep LIMIT 1;
                     """;
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@emp", DbType.Int32);
                command.Parameters["@emp"].Value = _selectedInfo.EpmloyeeID;
                command.Parameters.Add("@post", DbType.Int32);
                command.Parameters["@post"].Value = _selectedInfo.PostID;
                command.Parameters.Add("@dep", DbType.Int32);
                command.Parameters["@dep"].Value = _selectedInfo.DepartmentID;
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