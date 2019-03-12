using UnityEngine;
using System.Collections;

public class FlipTowardsMouse : MonoBehaviour 
{
    private Camera viewCamera;

    void Awake()
    {
        viewCamera = GameObject.FindGameObjectWithTag("ViewCamera").GetComponent<Camera>();
    }

	void Update () 
    {
        Vector3 mouseWorldPosition = viewCamera.ScreenToWorldPoint(Input.mousePosition);

        if (mouseWorldPosition.x < transform.position.x) 
        {
            transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
        } 
        else 
        {
            transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
        }
	}
}
