using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySqlConnector;

namespace Itogi;

public partial class ConnectionWin : Window
{
    public MySqlConnectionStringBuilder _connectionSB;
    
    public ConnectionWin()
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
}