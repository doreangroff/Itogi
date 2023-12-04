using System;
using System.Drawing;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Utilities;
using Brushes = Avalonia.Media.Brushes;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;

namespace Itogi.Windows;

public partial class pochta : Window
{
    private Random rand = new Random();
    private StackPanel stack;
    private StackPanel panel;
    private Avalonia.Controls.Image image;
    private static string prevText;
    string[] mesMas = { "Ты молодец, у тебя все получится!!!", "Брекшпрекникс"};
    
    public pochta()
    {
        InitializeComponent();
        stack = this.FindControl<StackPanel>("textPanel");
        image = this.FindControl<Avalonia.Controls.Image>("imagePanel");
        panel = this.FindControl<StackPanel>("Panel");
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
            Margin = new Thickness(5, 50, 0, 0),
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
        Avalonia.Controls.Image image = new Avalonia.Controls.Image()
        {
            Source = new Avalonia.Media.Imaging.Bitmap(AssetLoader.Open(new Uri($"avares://itogi/photos/sunglasses.png"))),
            Margin = new Thickness(0, 35, 5, 0),
            Width = 50,
            Height = 50,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        
        panel.Children.Add(image);
    }

    private void Button2_Click(object? sender, RoutedEventArgs e)
    {
        Avalonia.Controls.Image image = new Avalonia.Controls.Image()
        {
            Source = new Avalonia.Media.Imaging.Bitmap(AssetLoader.Open(new Uri($"avares://itogi/photos/weary.png"))),
            Margin = new Thickness(0, 35, 5, 0),
            Width = 50,
            Height = 50,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        panel.Children.Add(image);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Avalonia.Controls.Image image = new Avalonia.Controls.Image()
        {
            Source = new Avalonia.Media.Imaging.Bitmap(AssetLoader.Open(new Uri($"avares://itogi/photos/cry.png"))),
            Margin = new Thickness(0, 35, 5, 0),
            Width = 50,
            Height = 50,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        panel.Children.Add(image);
    }
}