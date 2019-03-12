using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ParallaxScoller : MonoBehaviour 
{
    private Track track;

    public float scaling = 1;
	
    private Vector3 startingPosition;

    void Awake()
    {
        track = transform.root.GetComponent<Track>();// GameObject.FindGameObjectWithTag("Track").GetComponent<Player>();
    }

    void Start()
    {
        startingPosition = transform.position;
    }

	void Update () 
    {
        transform.DOMoveX(startingPosition.x - track.distanceTravelled * scaling, .5f);
	}
}
