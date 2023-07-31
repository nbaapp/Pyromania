using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;
    public GameObject[] enemies;
    public int[] enemySpawnChance;

    public float spawnDelay;
    private float spawnTimer = 0;
    public float deadZoneRadius = 20;
    public float minSpwanPoint = 20;
    public float maxSpawnPoint = 50;

    private float enemyHealthToAdd;
    public float healthPerLevel = 20;
    public float expMultiplierIncrease = 0.2f;
    private float enemyExpToAdd;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        UpdateSpawnRate();
        SpawnEnemy();
        enemyHealthToAdd = 0;
        enemyExpToAdd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer();
    }

    public void UpdateSpawnRate()
    {
        spawnDelay = 1.0f/(player.GetComponent<Player>().level);
    }

    void SpawnTimer()
    {
        if (spawnTimer >= spawnDelay)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
        spawnTimer += Time.deltaTime;
    }

    public void IncreaseEnemyHealthAndExp()
    {
        enemyHealthToAdd += healthPerLevel;
        enemyExpToAdd += expMultiplierIncrease;
    }

    void SpawnEnemy()
    {
        Vector3 spawnPoint;
        float xPoint;
        float yPoint;
        bool canSpawn;
        int spawnedEnemy = 0;

        do
        {
            int enemyChosen = Random.Range(0, enemies.Length);
            if (Random.Range(0, 100) <= enemySpawnChance[enemyChosen])
            {
                canSpawn = true;
                spawnedEnemy = enemyChosen;
            }
            else
            {
                canSpawn = false;
            }
        } while (!canSpawn);

        GameObject enemy = enemies[spawnedEnemy];

        do
        {
            xPoint = Random.Range(player.transform.position.x + minSpwanPoint, player.transform.position.x + maxSpawnPoint);
            yPoint = Random.Range(player.transform.position.y + minSpwanPoint, player.transform.position.y + maxSpawnPoint);
            spawnPoint = new Vector3(xPoint, yPoint, 0);
        } while (Vector3.Distance(spawnPoint, player.transform.position) < deadZoneRadius);

        GameObject thisEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        Enemy thisEnemyScript = thisEnemy.GetComponent<Enemy>();

        thisEnemyScript.maxHealth += enemyHealthToAdd;
        thisEnemyScript.health = thisEnemyScript.maxHealth;
        thisEnemyScript.expMultiplier += enemyExpToAdd;

        thisEnemy.transform.parent = gameObject.transform;
    }
}
