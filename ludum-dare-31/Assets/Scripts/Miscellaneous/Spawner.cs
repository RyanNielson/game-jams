using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
    public GameObject item;

    public bool spawnAtStart = true;

    public float minimumSpawnTime = .5f;

    public float maximimSpawnTime = 2f;

    public int spawnCount = -1;

	// Use this for initialization
	void Start () 
    {
	    if (spawnAtStart)
        {
            Spawn();
        }

        StartCoroutine(WaitAndSpawn());
	}

    private float DetermineNextSpawnTime()
    {
        return Random.Range (minimumSpawnTime, maximimSpawnTime);
    }

    private void Spawn()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1f);

        if (collider == null)
        {
            Instantiate(item, transform.position, Quaternion.identity);

            spawnCount--;
        }
    }

    private IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(DetermineNextSpawnTime());

        if (spawnCount != 0)
        {
            Spawn();

            StartCoroutine(WaitAndSpawn());
        }
    }
}
