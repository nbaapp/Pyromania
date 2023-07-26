using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaycornTree : MonoBehaviour
{
    public GameObject paycorn;
    private GameObject player;

    public float shootDelay = 1;
    private float shootTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (shootTimer >= shootDelay)
        {
            GameObject myPaycorn = Instantiate(paycorn, gameObject.transform);
            Vector2 direction = new Vector2(player.transform.position.x - gameObject.transform.position.x, player.transform.position.y - gameObject.transform.position.y).normalized;
            myPaycorn.GetComponent<Paycorn>().moveDirection = direction;
            shootTimer = 0;
        }
        shootTimer += Time.deltaTime;
    }
}
