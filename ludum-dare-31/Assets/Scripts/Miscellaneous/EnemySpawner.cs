using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
    public GameObject[] enemies;

    public AnimationCurve spawnCurve;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        SpawnEnemies();
    }
    
    private void SpawnEnemies()
    {
        int spawnCurveValue = Mathf.FloorToInt(spawnCurve.Evaluate(GameData.currentLevel));
        int enemiesToSpawn = spawnCurveValue + Random.Range(0, 4);
        int enemiesSpawned = 0;

        while (enemiesSpawned != enemiesToSpawn)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-6f, 5f), 0);

            Collider2D collider = Physics2D.OverlapCircle(spawnPosition, 1f);

            float distanceFromPlayer = (player.position - spawnPosition).magnitude;
            
            if (collider == null && distanceFromPlayer > 3f)
            {
                GameObject enemy = (GameObject)Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition, Quaternion.identity);

                if (!enemy.GetComponent<Fire>())
                {
                    enemiesSpawned++;
                }
            }
        }

        //gameController.enemiesRemaining = enemiesSpawned;
        GameData.enemiesRemaining = enemiesSpawned;
    }
}
