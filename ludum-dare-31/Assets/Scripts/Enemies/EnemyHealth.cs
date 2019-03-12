using UnityEngine;
using System.Collections;

public class EnemyHealth : Health 
{
    public GameObject explosion;

	public override void OnDead(Vector3 killerPosition)
    {
        GameData.enemiesRemaining--;

        if (GetComponent<EnemyStabby>())
        {
            GetComponent<EnemyStabby>().enabled = false;
        }
        else if (GetComponent<EnemyShooty>())
        {
            GetComponent<EnemyShooty>().enabled = false;
            GetComponentInChildren<EnemyWeapon>().enabled = false;
            GetComponentInChildren<Weapon>().enabled = false;
        }
        else 
        {
            GetComponentInChildren<EnemyWeapon>().enabled = false;
        }

        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}