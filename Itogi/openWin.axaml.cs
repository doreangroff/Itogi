using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace Itogi;

public partial class openWin : Window
{
    private DispatcherTimer timer;
    
    public openWin()
    {
        InitializeComponent();
        AttachHandlers();
        InitializeTimer();
        RabStol stol = new RabStol();
        stol.Show();
        
    }
    
    private void AttachHandlers()
    {
        this.Closing += MainWindow_Closing;
    }
    
    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        this.Hide();
    }
    
    private void InitializeTimer()
    {
        timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(3)
        };
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
       timer.Stop();
       this.Close();
       
    }
}