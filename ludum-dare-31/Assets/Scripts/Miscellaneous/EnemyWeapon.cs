using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour 
{
    private Camera gameCamera;

    private Quaternion lookRotation;
    
    private SpriteRenderer spriteRenderer;

    private Transform player;

    private PlayerController playerController;

    private Weapon weapon;

    private bool alive = false;

    private GameController gameController;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weapon = GetComponent<Weapon>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        weapon.cooldown = Random.Range(weapon.cooldown, weapon.cooldown + .15f);

        StartCoroutine(ActivateAfterTime());
    }
    
    private void Update()
    {
        if (gameController.started && alive && player && playerController.enabled)
        {
            Rotate();

            weapon.Shoot(false);
        }
    }
    
    public void Rotate()
    {
        Vector3 aimDirection = (player.transform.position - transform.position).normalized;

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg));
        lookRotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10);

        float angle = lookRotation.eulerAngles.z;
        
        if (angle > 45 && angle < 135)
        {
            spriteRenderer.sortingLayerName = "BelowCharacters";
        }
        else 
        {
            spriteRenderer.sortingLayerName = "AboveCharacters";
        }
        
        if (angle > 90 && angle < 270)
        {
            transform.localScale = new Vector3(1f, -1f, 1f);
            transform.parent.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else 
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        
        transform.rotation = lookRotation;
    }

    private IEnumerator ActivateAfterTime()
    {
        yield return new WaitForSeconds(.25f);
        alive = true;
    }
}
