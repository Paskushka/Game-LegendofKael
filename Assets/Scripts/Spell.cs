using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    float flyTimer;
    [SerializeField] bool enemyBullet;
    public LayerMask whatIsSolid;

    private void Start()
    {
        flyTimer = lifetime;
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<EnemyAI>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                hitInfo.collider.GetComponent<Player>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up*speed*Time.deltaTime);
        flyTimer -= Time.deltaTime;
        if(flyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
