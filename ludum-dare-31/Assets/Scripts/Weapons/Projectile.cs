using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
        rigidbody2D.velocity = transform.right * speed;

        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Damageable damageable = collider.gameObject.GetComponent<Damageable>();

        if (damageable)
        {
            damageable.Damage(1, transform.position);
        }

        Destroy(gameObject);
    }
}