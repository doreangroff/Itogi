namespace Itogi.Entities;

public class Gender
{
    public Gender(int genderId, string genderName)
    {
        GenderId = genderId;
        GenderName = genderName;
    }

    public int GenderId { get; set; }
    public string GenderName { get; set; }
}