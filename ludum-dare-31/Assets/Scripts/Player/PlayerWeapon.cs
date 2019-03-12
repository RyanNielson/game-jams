using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour 
{
    private Camera gameCamera;

    private Camera viewCamera;

    private Quaternion lookRotation;
    
    private SpriteRenderer spriteRenderer;

    private Weapon weapon;

    private GameController gameController;
    
    private void Awake()
    {
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        viewCamera = GameObject.FindGameObjectWithTag("ViewCamera").GetComponent<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weapon = GetComponent<Weapon>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    
    private void Update()
    {
        HandleInput();
        Rotate();
    }
    
    void HandleInput()
    {
        Vector3 mouseWorldPosition = viewCamera.ScreenToWorldPoint(Input.mousePosition);

        // Get the direction between this and the mouse.
        Vector3 direction = (mouseWorldPosition - transform.position).normalized;
        
        if (direction.x != 0f || direction.y != 0f)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
            lookRotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10);
        }

        if (gameController.started && Input.GetButton("Fire1"))
        {
            if (weapon.fireable)
            {
                Go.to(gameCamera.transform, .10f, new GoTweenConfig().shake(new Vector3(.25f, .25f, 0f), GoShakeType.Position, 1, true));
            }

            weapon.Shoot(true); 
        }
    }
    
    public void Rotate()
    {
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
        }
        else 
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        transform.rotation = lookRotation;
    }
}
