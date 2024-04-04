using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{
    public float time;
    public Text timeDisplay;
    void Update()
    {
        timeDisplay.text = time.ToString();
        time += Time.deltaTime; 
    }
}
