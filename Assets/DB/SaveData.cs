using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SQLite4Unity3d;

public class SaveData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int SpellCasts { get; set; }
    public int Money { get; set; }
    public int Kills { get; set; }

    public static string SetIntList(List<int> intList)
    {
        var str = string.Join(",", intList.Select(x => x.ToString()));
        return str;
    }

    public static List<int> GetIntList(string str)
    {
        if(str == null ||str == string.Empty)
        {
            return new List<int>();
        }
        return str.Split(',').Select(int.Parse).ToList();
    }

    public override string ToString()
    {
        string str = "";
        str += Id.ToString();
        str += "\n";
        str += Name.ToString();
        str += "\n";
        str += Email.ToString();
        str += "\n";
        str += SpellCasts.ToString();
        str += "\n";
        str += Money.ToString();
        str += "\n";
        str += Kills.ToString();
        str += "\n";
        return str;
    }
}
