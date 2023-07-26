using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    Player player;
    UIManager UI;

    public float healthAmount = 50;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        UI = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            player.health += healthAmount;
            if (player.health > player.maxHealth)
            {
                player.health = player.maxHealth;
            }
            UI.SetHealth(player.health, player.maxHealth);
            Destroy(gameObject);
        }
    }
}
