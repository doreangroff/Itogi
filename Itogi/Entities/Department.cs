namespace Itogi.Entities;

public class Department
{
    public Department(int departmnetId, string departmentName)
    {
        Departmnet_id = departmnetId;
        Department_name = departmentName;
    }

    public int Departmnet_id { get; set; }
    public string Department_name { get; set; }
}