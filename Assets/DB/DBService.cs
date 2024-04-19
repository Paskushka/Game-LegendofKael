using SQLite4Unity3d;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;
using System.Text.RegularExpressions;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DBService
{
    private SQLiteConnection _connection;

    public DBService(string DatabaseName)
    {

#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);

    }

    public void CreateDB()
    {

        _connection.DropTable<SaveData>();
        _connection.CreateTable<SaveData>();

        var item = new SaveData
        {
            Name = "Kirill",
            Email = "pask@gmail.com",
            Password = "12345678q",
            SpellCasts = 0,
            Money = 0,
            Kills = 0,

        };

        _connection.Insert(item);

    }

    public IEnumerable<SaveData> GetData()
    {
        return _connection.Table<SaveData>();
    }

    public SaveData GetUserData(string email)
    {
        var existingUser = _connection.Table<SaveData>().Where(x => x.Email == email).FirstOrDefault();
        return existingUser;
    }
    public string ShowList()
    {
        IEnumerable<SaveData> list = GetData();
        string str = "";
        foreach (SaveData item in list)
        {
            str += item.ToString();
        }
        return str;
    }

    public string CreateSaveData(string name, string email, string password)
    {
        if (name == null || name == string.Empty)
        {
            return "Incorrect name!";
        }
        if (email == null || email == string.Empty || !IsValidEmail(email))
        {
            return "Incorrect email!";
        }
        if (password == null || password == string.Empty || !IsValidPassword(password))
        {
            return "Incorrect password!";
        }
        var existingUser = _connection.Table<SaveData>().Where(x => x.Email == email).FirstOrDefault();
        if (existingUser != null)
        {
            return "This email already exists!";
        }
        else
        {
            var saveData = new SaveData
            {
                Name = name,
                Email = email,
                Password = password,
                SpellCasts = TransitionData.SpellCasts,
                Money = TransitionData.Money,
                Kills = TransitionData.Kills,
            };
            _connection.Insert(saveData);
            return "User is Created!";
        }
    }

    public void CopySaveData(SaveData newData)
    {
        SaveData existingUser = _connection.Table<SaveData>().Where(x => x.Id == newData.Id).FirstOrDefault();

        if (existingUser != null)
        {
            existingUser = newData;

            _connection.Update(existingUser);

            Debug.Log("User info was updated");
        }
        else
        {
            Debug.Log("No user with such id");
        }
    }

    public string AuthenticateUser(string name, string email, string password)
    {
        if (name == null || name == string.Empty)
        {
            return "Incorrect name!";
        }
        if (email == null || email == string.Empty || !IsValidEmail(email))
        {
            return "Incorrect email!";
        }
        if (password == null || password == string.Empty || !IsValidPassword(password))
        {
            return "Incorrect password!";
        }

        SaveData user = _connection.Table<SaveData>().FirstOrDefault(x => x.Email == email);

        if (user == null)
        {
            return "No user with such email";
        }
        else
        {
            if (user.Name != name)
            {
                return "Incorrect user name";
            }
            else
            {
                if (user.Password != password)
                {
                    return "Incorrect password";
                }
                else
                {
                    return "Welcome"; 
                }
            }
        }
    }

    private bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_]+@[a-zA-Z0-9_]+\.[a-zA-Z0-9_]+$";
        return Regex.IsMatch(email, pattern);
    }

    private bool IsValidPassword(string password)
    {
        string pattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
        return Regex.IsMatch(password, pattern);
    }
}
