using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public float cooldown = 1f;

    public Projectile projectile;

    public bool fireable = true;
    
    public Transform muzzleFlash;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot(bool player = false)
    {
        if (fireable)
        {
            Instantiate(projectile, transform.TransformPoint(transform.localPosition + new Vector3(1f, 0f, 0f)), transform.rotation);

            if (muzzleFlash)
            {
                if (GetComponent<EnemyWeapon>())
                {
                    Transform flash = Instantiate(muzzleFlash, transform.TransformPoint(transform.localPosition + new Vector3(1f, .1f, 0f)), transform.rotation) as Transform;
                    flash.parent = transform;
                }
                else 
                {
                    Transform flash = Instantiate(muzzleFlash, transform.TransformPoint(transform.localPosition + new Vector3(1.2f, .1f, 0f)), transform.rotation) as Transform;
                    flash.parent = transform;
                }
            }

            if (audioSource)
            {
                audioSource.PlayOneShot(audioSource.clip, player ? 1f : .1f);
            }

            fireable = false;
            StartCoroutine(Cooldown());
        }
    }
    
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        fireable = true;
    }
}