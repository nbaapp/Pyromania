using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;
    public GameObject nevergreen;

    public float spawnDelay;
    private float spawnTimer = 0;
    public float deadZoneRadius = 20;
    public float minSpwanPoint = 20;
    public float maxSpawnPoint = 50;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        UpdateSpawnRate();
        SpawnEnemy();
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

    void SpawnEnemy()
    {
        Vector3 spawnPoint;
        float xPoint;
        float yPoint;

        do
        {
            xPoint = Random.Range(player.transform.position.x + minSpwanPoint, player.transform.position.x + maxSpawnPoint);
            yPoint = Random.Range(player.transform.position.y + minSpwanPoint, player.transform.position.y + maxSpawnPoint);
            spawnPoint = new Vector3(xPoint, yPoint, 0);
        } while (Vector3.Distance(spawnPoint, player.transform.position) < deadZoneRadius);

        GameObject enemy = Instantiate(nevergreen, spawnPoint, Quaternion.identity);
        enemy.transform.parent = gameObject.transform;
    }
}
