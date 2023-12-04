using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Itogi.Entities;
using Itogi.Windows;
using Itogi.Windows.DepartmentsDir;
using Itogi.Windows.EmpsDir;
using Itogi.Windows.PostsDir;
using MySqlConnector;

namespace Itogi;

public partial class RabStol : Window
{
    dyrachyo dyrachyo = new dyrachyo() { Topmost = true };
    private pochta pochta = new pochta();
    private Emp_infoWin info = new Emp_infoWin();
    private EmployeeWin emps = new EmployeeWin();
    private DepWin dep = new DepWin();
    private PostWin post = new PostWin();
    
    private bool dyrFlag = true;
    private bool pochtaFlag = false;
    private bool infoFlag = false;
    private bool empsFlag = false;
    private bool depFlag = false;
    private bool postFlag = false;
    
    public RabStol()
    {
        InitializeComponent();
        DateTime currentDate = DateTime.Now;
        var calendarDate = this.FindControl<CalendarDatePicker>("date");
        calendarDate.SelectedDate = currentDate;
        InitDurachyo();
        
        pochta = new pochta();
        info = new Emp_infoWin();
        emps = new EmployeeWin();
        dep = new DepWin();
        post = new PostWin();
    }

    private void InitDurachyo()
    {
        dyrachyo = new dyrachyo();
        dyrachyo.Show();
        MoveWindow(1418, 0, dyrachyo);
        dyrachyo.Closing += delegate
        {
            dyrFlag = false;
        };

    }

    public void MoveWindow(double x, double y, Window window)
    {
        window.Position = new PixelPoint((int)x, (int)y);
    }
    
    private void Info_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        info.Show();
        infoFlag = true;
        info.Closing += delegate { infoFlag = false;};
    }

    private void Emps_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        emps.Show();
        empsFlag = true;
        emps.Closing += delegate { empsFlag = false;};
    }

    private void Deps_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        dep.Show();
        depFlag = true;
        dep.Closing += delegate { depFlag = false;};
        
    }

    private void Posts_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        post.Show();
        postFlag = true;
        post.Closing += delegate { postFlag = false;};
    }

    private void ExitBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ExitWindow exit = new ExitWindow();
        exit.ShowDialog(this);
        
    }

    private void MessageBtn_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        pochta.Show();
        MoveWindow(1217, 718, pochta);
        pochtaFlag = true;
        pochta.Closing += delegate { pochtaFlag = false;};
    }

    private void Btn_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if (dyrFlag == true)
        {
            dyrachyo.Width = 300;
            dyrachyo.Height = 300;
            MoveWindow(1418, 0, dyrachyo);
        }
        else
        {
            InitDurachyo();
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
            pochta.Show();
            MoveWindow(1217, 718, pochta);
            pochtaFlag = true;
        }
    }

    private void InfoBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (infoFlag == false)
        {
            info.Show();
            infoFlag = true;
        }
        else
        {
            info.Close();
            infoFlag = false;
        }
    }

    private void EmpBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (empsFlag == false)
        {
            emps.Show();
            empsFlag = true;
        }
        else
        {
            emps.Close();
            empsFlag = false;
        }
    }

    private void DepBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (depFlag == false)
        {
            dep.Show();
            depFlag = true;
        }
        else
        {
            dep.Close();
            depFlag = false;
        }
    }

    private void PostBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (postFlag == false)
        {
            post.Show();
            postFlag = true;
        }
        else
        {
            post.Close();
            postFlag = false;
        }
    }
    

    private void Fivaproldge(object? sender, PointerEventArgs e)
    {
        if (sender is Button button)
        {
            button.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#564b70"));
            button.PointerExited += (_, _) =>
            {
                button.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#7C68AD"));
            };
        }
    }
}