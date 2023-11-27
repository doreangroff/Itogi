using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySqlConnector;

namespace Itogi;

public partial class RabStol : Window
{
    public MySqlConnectionStringBuilder _connectionSB;
    
    public RabStol()
    {
        InitializeComponent();
        _connectionSB = new MySqlConnectionStringBuilder
        {
            Server = "10.10.1.24",//"10.10.1.24" "localhost"
            Database = "pro1_1", //"pro1_1" "itog" 
            UserID = "user_01", //"user_01" "user_1"
            Password = "user01pro" //"user01pro" "1234"
        };
    }
    
    public void MoveWindow(double x, double y, Window window)
    {
        window.Position = new PixelPoint((int)x, (int)y);
    }
    
}