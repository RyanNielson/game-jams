using UnityEngine;
using System.Collections;

public class EnemyStabby : MonoBehaviour 
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

    private void FixedUpdate()
    {
        if (gameController.started && alive && player && playerController.enabled)
        {
            Vector3 movementDirection = player.position - transform.position;
            
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg));
            
            transform.localScale = new Vector3(rotation.eulerAngles.z > 90f && rotation.eulerAngles.z < 270f ? -1 : 1, transform.localScale.y, transform.localScale.z);
            
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
                damageable.Damage(1, collision.transform.position);
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
