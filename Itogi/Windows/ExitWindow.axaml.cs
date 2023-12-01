using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Itogi.Windows;

public partial class ExitWindow : Window
{
    public ExitWindow()
    {
        InitializeComponent();
        
       
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}