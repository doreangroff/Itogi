namespace Itogi.Entities;

public class Department
{
    public Department(int departmnetId, string departmentName)
    {
        DepartmnetId = departmnetId;
        DepartmentName = departmentName;
    }

    public int DepartmnetId { get; set; }
    public string DepartmentName { get; set; }
}