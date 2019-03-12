using UnityEngine;
using System.Collections;

public class EnemyShooty : MonoBehaviour 
{
    private Transform player;

    private PlayerController playerController;

    public float speed = 5f;

    private Damageable damageableEnemy;

    private bool alive = false;

    private GameController gameController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();
        damageableEnemy = GetComponent<DamageableEnemy>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        StartCoroutine(ActivateAfterTime());
    }

    private void Update()
    {
        if (gameController.started && alive && player && playerController.enabled)
        {
            Vector3 movementDirection = player.position - transform.position;

            rigidbody2D.MovePosition(transform.position + movementDirection.normalized * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Projectile")
        {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            
            if (damageable)
            {
                damageable.Damage(1, transform.position);
            }

            if (collision.gameObject.tag != "Player")
            {
                damageableEnemy.Damage(10000, collision.transform.position);
            }
        }
    }

    private IEnumerator ActivateAfterTime()
    {
        yield return new WaitForSeconds(.25f);
        alive = true;
    }
}
