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
    private pochta pochta = new pochta();
    private bool dyrFlag = true;
    private bool pochtaFlag = true;
    public RabStol()
    {
        InitializeComponent();
        DateTime currentDate = DateTime.Now;
        var calendarDate = this.FindControl<CalendarDatePicker>("date");
        calendarDate.SelectedDate = currentDate;
        dyrachyo.Show();
        MoveWindow(1418, 0, dyrachyo);
        dyrachyo.Closing += (sender, args) =>
        {
            dyrFlag = false;
            (sender as Window)?.Hide();
            args.Cancel = true;
        };
        pochta.Closing += (sender, args) =>
        {
            pochtaFlag = false;
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
        Console.WriteLine(pochta.Position.Y);
        Console.WriteLine(pochta.Position.X);
    }

    private void Posts_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        PostWin post = new PostWin();
        post.ShowDialog(this);
    }

    private void ExitBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine(dyrachyo.Position.X);
        ExitWindow exit = new ExitWindow();
        exit.ShowDialog(this);
        
    }

    private void MessageBtn_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        pochta.Show();
        MoveWindow(1267, 718, pochta);
        
    }

    private void Btn_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if (dyrFlag == true)
        {
            dyrachyo.Width = 350;
            dyrachyo.Height = 350;
            MoveWindow(1418, 0, dyrachyo);
        }
        else
        {
            dyrachyo.Show();
            dyrachyo.Width = 350;
            dyrachyo.Height = 350;
            MoveWindow(1418, 0, dyrachyo);
            dyrFlag = true;
        }
        
    }
}