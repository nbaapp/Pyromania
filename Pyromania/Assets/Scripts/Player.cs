using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackLevel {LV1, LV2, LV3, LV4, LV5}

public class Player : MonoBehaviour
{
    PlayerInputActions inputActions;
    Rigidbody2D rb;
    UIManager UI;
    Logic logic;
    EnemySpawner spawner;
    public GameObject fireball;
    private Camera myCamera;

    public AttackLevel attackLevel = AttackLevel.LV1;

    public int level = 1;
    public float expValue = 0;
    public float expToNextLevel = 100;

    public int attackUpgrades = 0;
    public int attackRateUpgrades = 0;
    public int healthUpgrades = 0;
    public int attackDamageUpgrades = 0;
    public int burnDamageUpgrades = 0;
    public int fireSpreadTimesUpgrades = 0;

    public float maxHealth = 100;
    public float health;
    public float fireballDamage = 10;
    public float burnDamage = 1;
    public int maxFireSpreadTimes = 3;
    public float fireSpreadDistance = 5;

    public float healthPerLevel = 50;
    public float attackRatePerLevel = 1;
    public float attackdamagePerLevel = 5;
    public float burnDamagePerLevel = 1;
    public int fireSpreadTimesPerLevel = 1;

    public float moveSpeed = 10;

    private Vector2 aimDirection = new Vector2(1f, 0f);

    private float shootDelay;
    public float shootRateModifier = 2;
    private float shootTimer = 0;
    public float shootOffset = 3;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("UI Manager").GetComponent<UIManager>();
        logic = GameObject.Find("Logic").GetComponent<Logic>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        spawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        health = maxHealth;
        UpdateShootRate();
        level = 1;
        expValue = 0;

        UI.SetHealth(health, maxHealth);
        UI.SetEXP(expValue, expToNextLevel);
        UI.SetLevelText(level);

    }

    // Update is called once per frame
    void Update()
    {
        MouseAim();
        ControllerAim();
        Shoot();
        Timers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitEnemy(collision);
        HitPaycorn(collision);
    }

    void HitPaycorn(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            TakeDamage(collision.gameObject.GetComponent<Paycorn>().damage);
        }
    }

    public void UpdateShootRate()
    {
        shootDelay = 1 / shootRateModifier;
    }

    void RaiseLevel()
    {
        level++;
        UI.SetLevelText(level);
        expToNextLevel *= 2;
        spawner.UpdateSpawnRate();
        if (level % 2 == 1)
        {
            logic.LevelUpAttack();
        }
        UI.LevelUpScreenOn();
    }

    public void RaiseExp(float expIncrease)
    {
        expValue += expIncrease;
        if (expValue >= expToNextLevel)
        {
            expValue -= expToNextLevel;
            RaiseLevel();
        }
        UI.SetEXP(expValue, expToNextLevel);
    }

    void HitEnemy(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.GetComponent<Enemy>() != null)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                TakeDamage(enemy.attackDamage);
            }
        }
    }

    void TakeDamage(float damageValue)
    {
        health -= damageValue;
        UI.SetHealth(health, maxHealth);
    }

    void Timers()
    {
        if (canShoot == false)
        {
            if(shootTimer >= shootDelay)
            {
                canShoot = true;
                shootTimer = 0;
            }
            shootTimer += Time.deltaTime;
        }
    }
    
    void MouseAim()
    {
        Vector2 inputVector = inputActions.Player.MouseAim.ReadValue<Vector2>();
        Vector3 mousePosition = myCamera.ScreenToWorldPoint(new Vector3(inputVector.x, inputVector.y, 0));
        aimDirection = new Vector3 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0).normalized;
    }

    void ControllerAim()
    {
        Vector2 inputVector = inputActions.Player.GamepadAim.ReadValue<Vector2>();
        if (inputVector != new Vector2 (0, 0) && inputVector != aimDirection)
        {
            aimDirection = inputVector.normalized;
        }
    }

    void Move()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        rb.velocity = inputVector.normalized * moveSpeed;
    }

    void Shoot()
    {
        GameObject fireball1, fireball2, fireball3, fireball4, fireball5, fireball6, fireball7, fireball8;
        Vector3 myPosition = new Vector3(transform.position.x, transform.position.y, 0);
        if (inputActions.Player.Shoot.IsPressed())
        {
            if (canShoot)
            {
                if (attackLevel == AttackLevel.LV1)
                {
                    fireball1 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball1.GetComponent<Fireball>().moveDirection = aimDirection;
                }
                else if (attackLevel == AttackLevel.LV2)
                {
                    fireball1 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball2 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball3 = Instantiate(fireball, myPosition, Quaternion.identity);

                    fireball1.GetComponent<Fireball>().moveDirection = aimDirection;
                    fireball2.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 45) * aimDirection;
                    fireball3.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, -45) * aimDirection;
                }
                else if (attackLevel == AttackLevel.LV3)
                {
                    fireball1 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball2 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball3 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball4 = Instantiate(fireball, myPosition, Quaternion.identity);

                    fireball1.GetComponent<Fireball>().moveDirection = aimDirection;
                    fireball2.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 45) * aimDirection;
                    fireball3.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, -45) * aimDirection;
                    fireball4.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 180) * aimDirection;
                }
                else if (attackLevel == AttackLevel.LV4)
                {
                    fireball1 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball2 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball3 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball4 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball5 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball6 = Instantiate(fireball, myPosition, Quaternion.identity);

                    fireball1.GetComponent<Fireball>().moveDirection = aimDirection;
                    fireball2.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 45) * aimDirection;
                    fireball3.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, -45) * aimDirection;
                    fireball4.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 180) * aimDirection;
                    fireball5.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 225) * aimDirection;
                    fireball6.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, -225) * aimDirection;
                }
                else if (attackLevel == AttackLevel.LV5)
                {
                    fireball1 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball2 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball3 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball4 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball5 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball6 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball7 = Instantiate(fireball, myPosition, Quaternion.identity);
                    fireball8 = Instantiate(fireball, myPosition, Quaternion.identity);

                    fireball1.GetComponent<Fireball>().moveDirection = aimDirection;
                    fireball2.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 45) * aimDirection;
                    fireball3.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, -45) * aimDirection;
                    fireball4.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 180) * aimDirection;
                    fireball5.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 225) * aimDirection;
                    fireball6.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, -225) * aimDirection;
                    fireball7.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, 90) * aimDirection;
                    fireball8.GetComponent<Fireball>().moveDirection = Quaternion.Euler(0, 0, -90) * aimDirection;
                }
                canShoot = false;
            }
        }
    }
}
