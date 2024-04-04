using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Vector3 offset;
    public Slider slider;
    public float maxHealth;

    public void SetHealth(float health)
    {
        slider.gameObject.SetActive(health <= maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
    }

    private void Update()
    {
        slider.transform.position=Camera.main.WorldToScreenPoint(transform.parent.position+offset);
    }
}
