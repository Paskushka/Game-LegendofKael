using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyDBTest : MonoBehaviour
{
    DBService ds;
    public TextMeshProUGUI textik,resultText;
    [SerializeField]
    TMP_InputField nameField,emailField,passwordField;

    private void Start()
    {
        ds = new DBService("test.db");
    }
    public void StartSync()
    {
        ds.CreateDB();
    }

    public void GetEveryone()
    {
        string str = ds.ShowList();
        textik.text = str;
    }

    public void OnExit()
    { 
        Application.Quit();
    }

    public void CreateUser()
    {
        resultText.text = ds.CreateSaveData(nameField.text,emailField.text,passwordField.text);
        if(resultText.text == "User is Created!")
        {
            var data = ds.GetUserData(emailField.text);
            TransitionData.Name = data.Name;
            TransitionData.Email = data.Email;
            TransitionData.Password = data.Password;
            TransitionData.ID = data.Id;
            TransitionData.SpellCasts = data.SpellCasts;
            TransitionData.Money = data.Money;
            TransitionData.Kills = data.Kills;
            SceneManager.LoadScene("Menu");
        }
        
    }
    public void LogIn()
    {
        string email = emailField.text;
        resultText.text = ds.AuthenticateUser(nameField.text, emailField.text, passwordField.text);
        if(resultText.text == "Welcome")
        {
            var data = ds.GetUserData(email);
            TransitionData.Name = data.Name;
            TransitionData.Email = data.Email;
            TransitionData.Password = data.Password;
            TransitionData.ID = data.Id;
            TransitionData.SpellCasts = data.SpellCasts;
            TransitionData.Money = data.Money;
            TransitionData.Kills = data.Kills;

            SceneManager.LoadScene("Menu");
        }
    }

    public void DBUpdate(SaveData item)
    {

        ds.CopySaveData(item);
    }

 

}
