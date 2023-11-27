using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Itogi.Entities;
using MySqlConnector;

namespace Itogi.Windows.EmpsDir;

public partial class EditEmp : RabStol
{
    private readonly Employee _selectedEmployee;
    private List<Gender> _genders;
    
    public EditEmp(Employee selectedEmployee)
    {
        InitializeComponent();
        DataContext = selectedEmployee;
        _selectedEmployee = selectedEmployee;
        _genders = new List<Gender>();
        FillGenders();

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
                var genComboBox = this.Find<ComboBox>("CBGender");
                genComboBox.ItemsSource = _genders;
                Console.WriteLine(_selectedEmployee.Gender);
                genComboBox.SelectedItem = genComboBox.ItemsSource.Cast<Gender>()
                    .First(it => it.GenderName == _selectedEmployee.Gender);
                //genComboBox.SelectedItem = _selectedEmployee.Gender;
            }
        }
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                        update employees set fio = @fio,
                                            age = @age,
                                            gender = @gender,
                                            phone_number = @number
                        where employee_id = @id
                     """;
        using (var con = new MySqlConnection(_connectionSB.ConnectionString))
        {
            con.Open();
            using (var command = con.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add("@fio", DbType.String);
                command.Parameters["@fio"].Value = TBFio.Text;
                command.Parameters.Add("@age", DbType.Int32);
                command.Parameters["@age"].Value = TBAge.Text;
                command.Parameters.Add("@gender", DbType.Int32);
                command.Parameters["@gender"].Value = CBGender.SelectedIndex + 1;
                command.Parameters.Add("@number", DbType.String);
                command.Parameters["@number"].Value = TBPhone.Text;
                command.Parameters.Add("@id", DbType.Int32);
                command.Parameters["@id"].Value = _selectedEmployee.EmployeeId;
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