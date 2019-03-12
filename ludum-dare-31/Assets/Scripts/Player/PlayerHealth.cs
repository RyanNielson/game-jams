using UnityEngine;
using System.Collections;

public class PlayerHealth : Health 
{ 
    private GameController gameController;

    private Animator animator;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        animator = transform.FindChild("Sprite").GetComponent<Animator>();
    }

	public override void OnDead(Vector3 killerPosition)
    {
        gameController.gameOver = true;

        animator.SetTrigger("Dead");

        Destroy(transform.FindChild("Weapon").gameObject);
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponentInChildren<FlipTowardsMouse>().enabled = false;
        gameObject.collider2D.enabled = false;
        animator.transform.rotation = Quaternion.Euler(animator.transform.rotation.x, 0, animator.transform.rotation.z);

        StartCoroutine(ShowShadow());

        GameData.playerAnimatorController = null;
    }

    public void Die()
    {
        health = 0;
    }

    private IEnumerator ShowShadow()
    {
        yield return new WaitForSeconds(.5f);
        SpriteShadow spriteShadow = gameObject.GetComponentInChildren<SpriteShadow>();
        spriteShadow.size = new Vector2(2.1f, 2f);
        spriteShadow.offset = new Vector2(-.05f, -.75f);
    }
}