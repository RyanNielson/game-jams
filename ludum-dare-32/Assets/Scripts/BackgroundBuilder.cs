using UnityEngine;
using System.Collections;

public class BackgroundBuilder : MonoBehaviour 
{
    [SerializeField]
    private Transform background;

	// Use this for initialization
	void Start () 
    {
	    for (int y = -8; y <= 16; y++)
        {
            for (int x = -15; x <= 15; x++)
            {
                Transform newBackground = Instantiate(background, new Vector2(x, y), Quaternion.identity) as Transform;

                newBackground.parent = transform;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
