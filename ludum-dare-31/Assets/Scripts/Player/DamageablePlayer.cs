using UnityEngine;
using System.Collections;

public class DamageablePlayer : Damageable 
{
	private Health health;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;
	
	private void Awake()
	{
		health = GetComponent<Health>();
        spriteRenderer = transform.FindChild("Sprite").GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
	}
	
    public override void Damage(int damage, Vector3 attackerPosition)
	{
        if (!health.invincible)
        {
            audioSource.Play();
            health.SubtractHealth(damage, attackerPosition);
            StartCoroutine(FlashRed());
            StartCoroutine(SetInvincibility());
        }
	}

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
        health.invincible = false;
    }

    private IEnumerator SetInvincibility()
    {
        health.invincible = true;
        yield return new WaitForSeconds(.5f);
        health.invincible = false;
    }
}