using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using Itogi.Windows.DepartmentsDir;
using MySqlConnector;

namespace Itogi.Windows.PostsDir;

public partial class PostWin : RabStol
{
    private List<Post> _posts;
    public PostWin()
    {
        InitializeComponent();
        _posts = new List<Post>();
        ShowTable();
        
    }

    private void ShowTable()
    {
        _posts = new List<Post>();
        string sql = "select post_id, post_name from posts";
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
                PostGrid.ItemsSource = _posts;
            }
        }
    }

    private void SearchPost_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var searchPost = _posts.Where(x =>
            x.PostId.ToString() == searchPosts.Text ||
            x.PostName.Contains(searchPosts.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        PostGrid.ItemsSource = searchPost;
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AddPost add = new AddPost();
        add.ShowDialog(this);
        add.Closed += delegate(object? o, EventArgs args) { ShowTable(); };
    }

    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedPost = PostGrid.SelectedItem as Post;
        if(selectedPost is null) return;

        EditPost edit = new EditPost(selectedPost);
        edit.ShowDialog(this);
        edit.Closed += delegate { ShowTable(); };
    }

    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedPost = PostGrid.SelectedItem as Post;
        if(selectedPost is null) return;

        DelPost del = new DelPost(selectedPost);
        del.ShowDialog(this);
        del.Closed += delegate { ShowTable(); };
    }
}