using UnityEngine;
using System.Collections;

public abstract class Health : MonoBehaviour 
{
	public int health = 100;

	public int maxHealth = 100;

    public bool invincible = false;

    private bool dead = false;

    private Vector3 lastAttackerPosition;
	
	public void AddHealth(int amount)
	{
		ModifyHealth(amount);
	}
	
	public void SubtractHealth(int amount, Vector3 attackerPosition)
	{
        if (!invincible)
        {
            ModifyHealth(-amount);
            lastAttackerPosition = attackerPosition;
        }
	}
	
    public void ModifyHealth(int amount)
	{
		health = Mathf.Clamp(health + amount, 0, maxHealth);
	}
	
	public bool Dead()
	{
		return health <= 0;
	}
	
	private void Update()
	{
		if (Dead() && !dead)
		{
			OnDead(lastAttackerPosition);
            dead = true;
		}
	}
	
	abstract public void OnDead(Vector3 killerPosition);
}