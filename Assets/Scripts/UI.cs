using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public GameObject image1;
    public GameObject image2;
    public GameObject panel;
    private Player player;
    public TextMeshProUGUI endMoney;
    public TextMeshProUGUI endKills;
    public TextMeshProUGUI endCasts;

    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Test()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Debug.Log(player.GetMoney());
        text1.text = player.GetMoney().ToString();
        text2.text = player.GetKills().ToString();
    }
    public void endGame()
    {
        panel.SetActive(true);
        text1.text = "";
        text2.text = "";
        image1.SetActive(false);
        image2.SetActive(false);
        endMoney.text = player.GetMoney().ToString();
        endKills.text = player.GetKills().ToString();
        endCasts.text = player.GetSpellCasts().ToString();
        Time.timeScale = 0;
    }
    public void Save()
    {

    }
}
