using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour 
{
	void Start () 
    {
        StartCoroutine(Flash());
	}

    private IEnumerator Flash()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
}
