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
            Server = "localhost",//"10.10.1.24" "localhost"
            Database = "itog", //"pro1_1" "itog" 
            UserID = "user_1", //"user_01" "user_1"
            Password = "1234" //"user01pro" "1234"
        };
    }
    
    public void MoveWindow(double x, double y, Window window)
    {
        window.Position = new PixelPoint((int)x, (int)y);
    }
    
}