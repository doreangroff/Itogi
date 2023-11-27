using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.EmpsDir;

public partial class AddEmp : RabStol
{
    private List<Employee> _employees;
    private List<Gender> _genders;
    public AddEmp()
    {
        InitializeComponent();
        _employees = new List<Employee>();
    }

    public void FillGenders()
    {
        string sql = "select * from genders";
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _genders.Add(new Gender(
                            reader.GetInt32("gender_id"),
                            reader.GetString("gender_name")
                        ));
                    }
                }

                con.Close();
                var genComboBox = this.Find<ComboBox>("GenderFiltr");
                genComboBox.ItemsSource = _genders;
            }
        }
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                     insert into employees (fio, age, gender, phone_number)
                     values (@fio, @age, @gender, @phone)
                     """;
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@fio", MySqlDbType.String);
                command.Parameters["@fio"].Value = TBFio.Text;
                command.Parameters.Add("@age", MySqlDbType.Int32);
                command.Parameters["@age"].Value = TBAge.Text;
                command.Parameters.Add("@gender", MySqlDbType.String);
                command.Parameters["@gender"].Value = CBGender.SelectedIndex+1;
                command.Parameters.Add("@phone", MySqlDbType.String);
                command.Parameters["@phone"].Value = TBPhone.Text;
                command.ExecuteNonQuery();
            }
            con.Close();
            this.Close();
        }
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}