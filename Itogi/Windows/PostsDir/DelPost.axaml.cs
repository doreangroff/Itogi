using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.PostsDir;

public partial class DelPost : RabStol
{
    private readonly Post _selectedPost;
    
    public DelPost(Post selectedPost)
    {
        InitializeComponent();
        _selectedPost = selectedPost;
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                        SET FOREIGN_KEY_CHECKS=0;
                        Delete from posts where post_id = @id limit 1
                     """;
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@id", DbType.Int32);
                command.Parameters["@id"].Value = _selectedPost.PostId;
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