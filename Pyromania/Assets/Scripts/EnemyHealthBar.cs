using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthBar.value = health / maxHealth;
    }
}
