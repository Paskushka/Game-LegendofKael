using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float movingSpeed = 5f;
    //public HealthBar healthBar;
    private Rigidbody2D rb;
    private Vector3 targetPosition;
    private bool ismoving = false;
    public float health = 10f;
    public GameObject panel;
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //healthBar.SetHealth(health);
        //healthBar.maxHealth = health;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetPosition();
        }

        if (ismoving)
        {
            Move();
        }

        if (health <= 0)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        ismoving = true;
    }
    private void Move()
    {
        transform.position =Vector3.MoveTowards(transform.position, targetPosition, movingSpeed*Time.deltaTime);
        if(transform.position == targetPosition)
        {
            ismoving = false;
        } 
        
    }
    /*private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        inputVector = inputVector.normalized;

        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));
    }*/

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        // healthBar.SetHealth(health);
    }
}
