using UnityEngine;
using System.Collections;

public class CrazyExplosionMachine : MonoBehaviour 
{
    public GameObject explosion;

    public AnimationCurve explosionSpawnCurve;

    private float explosionStartTime;

    private Camera gameCamera;

    private void Awake()
    {
        gameCamera = Camera.main;
    }

    void Start()
    {
        explosionStartTime = Time.timeSinceLevelLoad;
        StartCoroutine(SpawnExplosions());
    }

    private IEnumerator SpawnExplosions()
    {
        while (true)
        {
            yield return new WaitForSeconds(explosionSpawnCurve.Evaluate(explosionStartTime - Time.timeSinceLevelLoad));

            Vector3 spawnPosition = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-6f, 5f), 0);
            Instantiate(explosion, spawnPosition, Quaternion.identity);
            Go.to(gameCamera.transform, .20f, new GoTweenConfig().shake(new Vector3(.25f, .25f, 0f), GoShakeType.Position, 1, true));
        }
    }
}
