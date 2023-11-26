namespace Itogi.Entities;

public class Employee
{
    public Employee(int employeeId, string fio, int age, int gender, string phoneNumber)
    {
        EmployeeId = employeeId;
        FIO = fio;
        Age = age;
        Gender = gender;
        Phone_number = phoneNumber;
    }

    public int EmployeeId { get; set; }
    public string FIO { get; set; }
    public int Age { get; set; }
    public int Gender;

    public string Gen
    {
        get
        {
            if (Gender == 1) return "Мужской";
            else
            {
                return "Женский";
            }
        }
    }
    public string Phone_number { get; set; }
}