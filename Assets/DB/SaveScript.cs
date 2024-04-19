using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    SaveData saveData;
    [SerializeField]
    MyDBTest db;
    private Player player;
    private void Start()
    {
        //Invoke(nameof(Save),0.3f);
    }

    public void OnSave()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Debug.Log(TransitionData.Money);
        TransitionData.SpellCasts += player.GetSpellCasts();
        TransitionData.Money += player.GetMoney();
        TransitionData.Kills += player.GetKills();
        Debug.Log(TransitionData.Money);
        saveData = new SaveData
        {
            Id = TransitionData.ID,
            Name = TransitionData.Name,
            Email = TransitionData.Email,
            Password = TransitionData.Password,
            SpellCasts = TransitionData.SpellCasts,
            Money = TransitionData.Money,
            Kills = TransitionData.Kills,
        };
        db.DBUpdate(saveData);
        SceneManager.LoadScene("Menu");
    }
  
}
