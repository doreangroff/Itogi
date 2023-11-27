namespace Itogi.Entities;

public class Employee
{
    public Employee(int employeeId, string fio, int age, string gender, string phoneNumber)
    {
        EmployeeId = employeeId;
        FIO = fio;
        Age = age;
        Gender = gender;
        PhoneNumber = phoneNumber;
    }

    public int EmployeeId { get; set; }
    public string FIO { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
}