using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    private Animator animator;
    private EnemyHealthBar healthBar;
    public GameObject healthDrop;

    public float maxHealth = 50;
    public float health;
    public float attackDamage = 10;
    public float expValue;

    private bool willSpawnHealth;
    public float healthSpawnChance = 0.1f;

    public bool exposedToFire = false;
    public bool immuneToFire = false;
    public bool onFire = false;
    public int fireSpreadCount = 0;
    public float catchTime = 1;
    private float catchTimer = 0;
    public float burnDuration = 2;
    private float burnTimer = 0;
    public float immunityDuration = 0.25f;
    public float immunityTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();

        expValue = maxHealth / 5;

        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        WillSpawnHealth();
    }

    // Update is called once per frame
    void Update()
    {
        ImmunitySeconds();
        ExposedToFire();
        SpreadFire();
        HealthBarPatch();
        Burn();
        Death();
    }

    private void WillSpawnHealth()
    {
        if (Random.value <= healthSpawnChance)
        {
            willSpawnHealth = true;
        }
        else
        {
            willSpawnHealth = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitByFireball(collision);
    }

    void ExposedToFire()
    {
        if (exposedToFire && !onFire)
        {
            if (catchTimer >= catchTime)
            {
                CatchFire();
                catchTimer = 0;
            }
            catchTimer += Time.deltaTime;
        }
    }

    void SpreadFire()
    {
        if (onFire && fireSpreadCount < player.maxFireSpreadTimes)
        {
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), player.fireSpreadDistance);
            for (int i = 0; i < nearbyEnemies.Length; i++)
            {
                if (nearbyEnemies[i].gameObject.layer == 7)
                {
                    Enemy enemy = nearbyEnemies[i].gameObject.GetComponent<Enemy>();
                    if (!enemy.exposedToFire && !enemy.onFire && !enemy.immuneToFire)
                    {
                        enemy.exposedToFire = true;
                        enemy.fireSpreadCount = fireSpreadCount + 1;
                    }
                }
            }
        }
    }

    void HealthBarPatch()
    {
        if (healthBar.healthBar.maxValue != 1)
        {
            healthBar.healthBar.maxValue = 1;
        }
    }

    void Death()
    {
        if (health <= 0)
        {
            player.RaiseExp(expValue);
            if (willSpawnHealth)
            {
                Instantiate(healthDrop, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    void Burn()
    {
        if (onFire)
        {
            StopFire();
            TakeDamage(player.burnDamage * Time.deltaTime);
        }
    }

    void StopFire()
    {
        if (burnTimer >= burnDuration)
        {
            onFire = false;
            animator.SetTrigger("NotOnFire");
            burnTimer = 0;
            immuneToFire = true;
        }
        burnTimer += Time.deltaTime;
    }
    
    void ImmunitySeconds()
    {
        if (immuneToFire)
        {
            if (immunityTimer >= immunityDuration)
            {
                immuneToFire = false;
                immunityTimer = 0;
            }
            immunityTimer += Time.deltaTime;
        }
    }

    void HitByFireball(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            fireSpreadCount = 0;
            immuneToFire = false;
            immunityTimer = 0;
            CatchFire();
            TakeDamage(player.fireballDamage);
        }
    }

    void TakeDamage(float damageValue)
    {
        health -= damageValue;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    void CatchFire()
    {
        onFire = true;
        exposedToFire = false;
        animator.SetTrigger("OnFire");
    }
}
