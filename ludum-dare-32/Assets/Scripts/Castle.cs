using UnityEngine;
using System.Collections;

public class Castle : MonoBehaviour
 {
    private Animator animator;

    private Track track;

    private AudioSource audioSource;

    private bool destroyed = false;

    [SerializeField]
    private AudioClip explosion;

	void Awake() 
    {
        animator = GetComponent<Animator>();
	    audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        track = transform.root.GetComponent<Track>();    
    }

	void Update () 
    {
        if (track.distanceTravelled >= GameController.TrackDistance && !destroyed)
        {
            animator.SetBool("Destroyed", true);
            audioSource.Play();
            audioSource.PlayOneShot(explosion);

            destroyed = true;
        }
	}
}
