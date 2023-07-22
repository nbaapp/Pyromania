using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    private Player player;
    private UIManager UI;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        UI = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    public void LevelUpHealth()
    {
        player.healthUpgrades++;
        player.maxHealth += player.healthPerLevel;
        player.health += player.healthPerLevel;
        UI.SetHealth(player.health, player.maxHealth);
        UI.LevelUpScreenOff();
    }

    public void LevelUpAttackRate()
    {
        player.attackRateUpgrades++;
        player.shootRateModifier += player.attackRatePerLevel;
        player.UpdateShootRate();
        UI.LevelUpScreenOff();
    }

    public void LevelUpDamage()
    {
        player.attackDamageUpgrades++;
        player.fireballDamage += player.attackdamagePerLevel;
        UI.LevelUpScreenOff();
    }
    public void LevelUpBurn()
    {
        player.burnDamageUpgrades++;
        player.burnDamage += player.burnDamagePerLevel;
        UI.LevelUpScreenOff();
    }
    public void LevelUpSpread()
    {
        player.fireSpreadTimesUpgrades++;
        player.maxFireSpreadTimes += player.fireSpreadTimesPerLevel;
        UI.LevelUpScreenOff();
    }

    public void LevelUpAttack()
    {
        player.attackUpgrades++;
        switch (player.attackLevel)
        {
            case AttackLevel.LV1:
                player.attackLevel = AttackLevel.LV2;
                break;
            case AttackLevel.LV2:
                player.attackLevel = AttackLevel.LV3;
                break;
            case AttackLevel.LV3:
                player.attackLevel = AttackLevel.LV4;
                break;
            case AttackLevel.LV4:
                player.attackLevel = AttackLevel.LV5;
                break;
            case AttackLevel.LV5:
                player.attackLevel = AttackLevel.LV5;
                break;
        }
    }
}
