using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    private Animator animator;

    public Sprite fireSprite;

    public float maxHealth = 50;
    public float health;

    bool onFire = false;
    int fireSpreadCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Burn();
        Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitByFireball(collision);
    }

    void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Burn()
    {
        if (onFire)
        {
            TakeDamage(player.burnDamage * Time.deltaTime);
        }
    }

    void HitByFireball(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            catchFire(collision);
            TakeDamage(player.fireballDamage);
        }
    }

    void TakeDamage(float damageValue)
    {
        health -= damageValue;
    }

    void catchFire(Collider2D collision)
    {
        onFire = true;
        fireSpreadCount = player.maxFireSpreadTimes;
        animator.SetTrigger("OnFire");
        gameObject.GetComponent<SpriteRenderer>().sprite = fireSprite;
    }
}
