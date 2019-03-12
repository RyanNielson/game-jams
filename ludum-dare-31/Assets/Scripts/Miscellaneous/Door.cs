using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
    public bool open = false;

    private Animator[] animators;

    private AudioSource audioSource;

	void Awake() 
    {
        animators = GetComponentsInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () 
    {
        if (!open && GameData.enemiesRemaining <= 0)
        {
            open = true;

            foreach (Animator animator in animators)
            {
                animator.SetTrigger("Open");
            }

            audioSource.Play();
        }
	}

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (open && collider.tag == "Player")
        {
            GameData.currentLevel++;
            //LevelManager.Reload();
            Application.LoadLevel("Game");
        }
    }
}
