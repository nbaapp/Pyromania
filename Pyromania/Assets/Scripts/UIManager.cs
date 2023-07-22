using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Slider healthBar;
    private Slider expBar;
    private TextMeshProUGUI levelText;
    public GameObject levelUpScreen;

    private void Start()
    {
        healthBar = GameObject.Find("Player Health Bar").GetComponent<Slider>();
        expBar = GameObject.Find("EXP Bar").GetComponent<Slider>();
        levelText = GameObject.Find("Level Text").GetComponent<TextMeshProUGUI>();
    }

    public void SetHealth(float health, float maxHealth)
    {
        healthBar.value = health/maxHealth;
    }

    public void SetEXP(float exp, float maxExp)
    {
        expBar.value = exp / maxExp;
    }

    public void SetLevelText(int level)
    {
        levelText.text = "LV " + level;
    }

    public void LevelUpScreenOn()
    {
        levelUpScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void LevelUpScreenOff()
    {
        levelUpScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
