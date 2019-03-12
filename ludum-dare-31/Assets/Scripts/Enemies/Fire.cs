using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour 
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Damageable damageable = collider.gameObject.GetComponent<Damageable>();

            if (damageable)
            {
                damageable.Damage(1, transform.position);
                collider2D.enabled = false;
                animator.SetBool("Alive", false);
            }
        }
    }
}
