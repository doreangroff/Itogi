namespace Itogi.Entities;

public class Gender
{
    public Gender(int genderId, string genderName)
    {
        Gender_id = genderId;
        Gender_name = genderName;
    }

    public int Gender_id { get; set; }
    public string Gender_name { get; set; }
}