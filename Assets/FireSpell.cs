using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour
{
    public FireSpellType spellType;

    public GameObject bullet;
    public Transform shotPoint;

    public enum FireSpellType { Default, Enemy}

    private float timeBtwShots;
    public float startTimeBtwShots;
    public float offset;
    private Vector3 difference;
    private float rotz;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (spellType == FireSpellType.Default)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else if(spellType == FireSpellType.Enemy)
        {
            difference = player.transform.position - transform.position;
            rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotz + offset);

        if (timeBtwShots < 0)
        {
            if (Input.GetMouseButton(1) || spellType == FireSpellType.Enemy)
            {
                Instantiate(bullet, shotPoint.position, shotPoint.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots-=Time.deltaTime;
        }
    }
}
