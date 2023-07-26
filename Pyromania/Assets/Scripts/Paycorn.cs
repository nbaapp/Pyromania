using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paycorn : MonoBehaviour
{
    private Rigidbody2D rb;

    public Vector2 moveDirection;
    public float moveSpeed = 50;

    public float lifetime = 5;
    private float lifeTimer = 0;

    public float damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KillTimer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }

    void KillTimer()
    {
        if (lifeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
        lifeTimer += Time.deltaTime;
    }

    void Move()
    {
        rb.velocity = moveDirection * moveSpeed;
    }
}
