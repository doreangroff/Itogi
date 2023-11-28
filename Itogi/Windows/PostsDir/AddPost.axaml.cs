using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.PostsDir;

public partial class AddPost : ConnectionWin
{
    private List<Post> _posts;
    
    public AddPost()
    {
        InitializeComponent();
        _posts = new List<Post>();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                        insert into posts (post_name)
                        values (@name)
                     """;
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@name", DbType.String);
                command.Parameters["@name"].Value = TBName.Text;
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