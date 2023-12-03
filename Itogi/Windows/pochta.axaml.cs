using System;
using System.Drawing;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Brushes = Avalonia.Media.Brushes;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;

namespace Itogi.Windows;

public partial class pochta : Window
{
    private Random rand = new Random();
    private StackPanel stack;
    private Avalonia.Controls.Image image;
    private static string prevText;
    string[] mesMas = { "Ты молодец, у тебя все получится!!!", "Брекшпрекникс"};
    
    public pochta()
    {
        InitializeComponent();
        stack = this.FindControl<StackPanel>("Panel");
        image = this.FindControl<Avalonia.Controls.Image>("imagePanel");
        int m = rand.Next(0, 2);
        Console.WriteLine(m.ToString());
        prevText = $"{mesMas[m]}";
        message.Text = prevText;
        AttachHandlers();
    }

    private void AddTextBlock(string text)
    {
        TextBlock textBlock = new TextBlock
        {
            Text = text,
            VerticalAlignment  = VerticalAlignment.Center,
            Padding = new Thickness(5, 0, 0, 0 )
        };
        
        Border border = new Border()
        {
            Child = textBlock,
            BorderThickness = new Thickness(1),
            BorderBrush = Brushes.Black,
            CornerRadius = new CornerRadius(10),
            Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#42AAFF")),
            Margin = new Thickness(5, 7, 0, 0),
            Height = 30,
            HorizontalAlignment = HorizontalAlignment.Left
            
        };
        stack.Children.Add(border);
    }


    private void AttachHandlers()
    {
        this.Closing += PochtaCloserd;
    }

    private void PochtaCloserd(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        this.Hide();
        
        int m = rand.Next(0, 2);
        string newText = $"{mesMas[m]}";
        
        AddTextBlock(newText);
        prevText = newText;
    }

    private void Button1_Click(object? sender, RoutedEventArgs e)
    {
        SetImage("photos/sunglasses.png");
    }

    private void Button2_Click(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void SetImage(string imagePath)
    {
        image.Source = new Avalonia.Media.Imaging.Bitmap(imagePath);
        
    }
}