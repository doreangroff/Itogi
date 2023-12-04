using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Itogi.Windows;
using Itogi.Windows.DepartmentsDir;
using Itogi.Windows.EmpsDir;
using Itogi.Windows.PostsDir;
using MySqlConnector;

namespace Itogi;

public partial class RabStol : Window
{
    dyrachyo dyrachyo = new dyrachyo()
    {
        Topmost = true
    };
    private pochta pochta = new pochta();
    private bool dyrFlag = true;
    private bool pochtaFlag = false;
    private bool infoFlag = false;
    
    public RabStol()
    {
        InitializeComponent();
        DateTime currentDate = DateTime.Now;
        var calendarDate = this.FindControl<CalendarDatePicker>("date");
        calendarDate.SelectedDate = currentDate;
        
        InitDurachyo();
    }

    private void InitDurachyo()
    {
        dyrachyo = new dyrachyo();
        dyrachyo.Show();
        MoveWindow(1411, 0, dyrachyo);
        
    }

    private void InitPochta()
    {
        pochta = new pochta();
        pochta.Show();
        MoveWindow(1213, 718, pochta);
        pochtaFlag = true;
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
        Console.WriteLine(dyrFlag.ToString());
        ExitWindow exit = new ExitWindow();
        exit.ShowDialog(this);
        
    }

    private void MessageBtn_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        InitPochta();
        pochta.Closing += delegate { pochtaFlag = false; Console.WriteLine(pochtaFlag.ToString());};
    }

    private void Btn_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if (dyrFlag == true)
        {
            dyrachyo.Width = 300;
            dyrachyo.Height = 300;
            MoveWindow(1411, 0, dyrachyo);
        }
        else
        {
            dyrachyo.Show();
            dyrachyo.Width = 300;
            dyrachyo.Height = 300;
            MoveWindow(1411, 0, dyrachyo);
            dyrFlag = true;
        }
        
    }

    private void Cam_OnClick(object? sender, RoutedEventArgs e)
    {
        
        if (dyrFlag == true)
        {
            dyrachyo.Close();
            dyrFlag = false;
        }
        else
        {
            InitDurachyo();
            dyrFlag = true;
        }
        ButtonColors(dyrFlag, Cam);
    }

    private void Poch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (pochtaFlag == true)
        {
            pochta.Close();
            pochtaFlag = false;
        }
        else
        {
            InitPochta();
            pochtaFlag = true;
        }
        ButtonColors(pochtaFlag, poch);
    }

    private void InfoBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Emp_infoWin info = new Emp_infoWin();
        OpenWindows(infoFlag, info);
    }

    private void EmpBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DepBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void PostBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ButtonColors(bool flag, Button button)
    {
        if (flag == true)
        {
            button.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#564b70")); //#564b70
        }
        else
        {
            button.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#7C68AD"));
        }
    }

    private void OpenWindows(bool flag, Window window)
    {
        if (flag == true)
        {
            window.Close();
        }
        else
        {
            window.Show();
        }
    }
}