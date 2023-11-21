using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Itogi.Windows.EmpsDir;

public partial class EmployeeWin : Window
{
    
    private const double targetY = 140;
    private const double moveStep = 40;
    public EmployeeWin()
    {
        InitializeComponent();
        //EmployeeWin emp = new EmployeeWin();
        //emp.Opened += NewWindow_Opened;

    }
    
    private async void NewWindow_Opened(object sender, EventArgs e)
    {
        await StartLoopAsync();
    }
    
    private async Task StartLoopAsync()
    {
        double currentX = this.Position.X;
        double currentY = this.Position.Y;
        while (currentY < targetY)
        {
            MoveWindow(currentX, currentY + moveStep);
            currentY += moveStep;
            await Task.Delay(40);
        }

    }
    
    private void MoveWindow(double x, double y)
    {
        this.Position = new PixelPoint((int)x, (int)y);
    }
}