using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Windows;
using Itogi.Windows.DepartmentsDir;
using Itogi.Windows.EmpsDir;
using Itogi.Windows.PostsDir;
using MySqlConnector;

namespace Itogi;

public partial class RabStol : Window
{
    dyrachyo dyrachyo = new dyrachyo();
    private bool dyrFlag = true;
    public RabStol()
    {
        InitializeComponent();
       
        dyrachyo.Show();
        MoveWindow(1363, 0, dyrachyo);
        dyrachyo.Closing += (sender, args) =>
        {
            dyrFlag = false;
            (sender as Window)?.Hide();
            args.Cancel = true;
        };
    }
    
    public void MoveWindow(double x, double y, Window window)
    {
        window.Position = new PixelPoint((int)x, (int)y);
    }

    

    private void Info_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        Emp_infoWin info = new Emp_infoWin();
        info.ShowDialog(this);
    }

    private void Emps_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        EmployeeWin emp = new EmployeeWin();
        emp.ShowDialog(this);
    }

    private void Deps_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        DepWin dep = new DepWin();
        dep.ShowDialog(this);
    }

    private void Posts_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        PostWin post = new PostWin();
        post.ShowDialog(this);
    }

    private void ExitBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ExitWindow exit = new ExitWindow();
        exit.ShowDialog(this);
        
    }

    private void MessageBtn_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        Console.WriteLine(dyrachyo.Position.X);
        Console.WriteLine(dyrachyo.Position.Y);
    }

    private void Btn_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if (dyrFlag == true)
        {
            dyrachyo.Width = 400;
            dyrachyo.Height = 400;
            MoveWindow(1363, 0, dyrachyo);
        }
        else
        {
            dyrachyo.Show();
            dyrachyo.Width = 400;
            dyrachyo.Height = 400;
            MoveWindow(1363, 0, dyrachyo);
            dyrFlag = true;
        }
        
    }
}