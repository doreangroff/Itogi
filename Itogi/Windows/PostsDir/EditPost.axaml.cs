using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.PostsDir;

public partial class EditPost : RabStol
{
    private readonly Post _selectedPost;
    private List<Post> _posts;
    
    public EditPost(Post selectedPost)
    {
        InitializeComponent();
        DataContext = selectedPost;
        _selectedPost = selectedPost;
        _posts = new List<Post>();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = "update posts set post_name = @name where post_id = @id";
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@name", DbType.String);
                command.Parameters["@name"].Value = TBName.Text;
                command.Parameters.Add("@id", DbType.Int32);
                command.Parameters["@id"].Value = _selectedPost.PostId;
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