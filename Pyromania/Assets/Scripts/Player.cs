using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInputActions inputActions;
    Rigidbody2D rb;
    public GameObject fireball;

    public float fireballDamage = 10;
    public float burnDamage = 1;
    public int maxFireSpreadTimes = 3;

    public float moveSpeed = 10;

    private Vector2 aimDirection = new Vector2(1f, 0f);

    public float shootDelay = 0.5f;
    private float shootTimer = 0;
    public float shootOffset = 3;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ControllerAim();
        Shoot();
        Timers();
    }

    private void FixedUpdate()
    {
        Move();
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
    
    void ControllerAim()
    {
        Vector2 inputVector = inputActions.Player.Aim.ReadValue<Vector2>();
        if (inputVector != new Vector2 (0, 0) && inputVector != aimDirection)
        {
            aimDirection = inputVector;
        }
    }

    void Shoot()
    {
        GameObject myfireball;
        if (inputActions.Player.Shoot.IsPressed())
        {
            if (canShoot)
            {
                myfireball = Instantiate(fireball, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                myfireball.GetComponent<Fireball>().moveDirection = aimDirection;
                canShoot = false;
            }
        }
    }

    void Move()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        rb.velocity = inputVector.normalized * moveSpeed;
    }
}
