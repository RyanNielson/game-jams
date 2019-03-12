using UnityEngine;
using System.Collections;

public abstract class Damageable : MonoBehaviour 
{
    abstract public void Damage(int damage, Vector3 attackerPosition);
}