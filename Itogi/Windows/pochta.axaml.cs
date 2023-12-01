using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Itogi.Windows;

public partial class pochta : Window
{
    private Random rand = new Random();
    public pochta()
    {
        InitializeComponent();
        string[] mesMas = { "Ты молодец, у тебя все получится!!!", "Брекшпрекникс"};
        int m = rand.Next(0, 2);
        message.Text = mesMas[m];
    }
}