using UnityEngine;
using System.Collections;

public class DamageableEnemy : Damageable 
{
    private Health health;

    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public override void Damage(int damage, Vector3 attackerPosition)
    {
        health.SubtractHealth(damage, attackerPosition);
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.05f);
        spriteRenderer.color = Color.white;
    }
}