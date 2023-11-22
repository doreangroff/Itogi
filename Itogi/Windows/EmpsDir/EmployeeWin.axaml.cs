using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace Itogi.Windows.EmpsDir;

public partial class EmployeeWin : Window
{
    
    private const int targetY = 160;
    private const int moveStep = 40;
    public EmployeeWin()
    {
        InitializeComponent();
        StartLoopAsync();
        //this.IsVisible = false;

    }
    
    private async Task StartLoopAsync()
    {
        await Task.Run(async () =>
        {
            double currentX = this.Position.X;
            int currentY = this.Position.Y;
            Console.WriteLine(currentX);
            Console.WriteLine(currentY);
            Dispatcher.UIThread.Post(() =>
            {
                Position = new PixelPoint(Position.X, 0);
            });

            while (currentY > -1000)
            {
                Console.WriteLine(currentX);
                Console.WriteLine(currentY);
                Dispatcher.UIThread.Post(() =>
                {
                    Position = new PixelPoint(Position.X, currentY);
                });
                MoveWindow(460, currentY - moveStep);
                currentY -= moveStep;
                await Task.Delay(20);
            }

            //this.IsVisible = true;
            while (currentY < targetY)
            {
                
                Dispatcher.UIThread.Post(() =>
                {
                    Position = new PixelPoint(Position.X, currentY);
                });
                MoveWindow(currentX, currentY + moveStep);
                currentY += moveStep;
                await Task.Delay(20);
            }
        });
        
    }
    
    private void MoveWindow(double x, double y)
    {
        this.Position = new PixelPoint((int)x, (int)y);
    }
}