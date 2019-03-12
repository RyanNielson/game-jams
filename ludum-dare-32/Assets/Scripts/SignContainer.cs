using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignContainer : MonoBehaviour 
{
    [SerializeField]
    private Transform sign;

    [SerializeField]
    private Transform castle;

	void Start () 
    {
        for (int i = 0; i < GameController.TrackDistance; i += 25)
        {
            Transform newSign = Instantiate(sign, new Vector2(i, transform.position.y), Quaternion.identity) as Transform;

            newSign.GetComponent<Sign>().text = i == 0 ? "GO!" : i.ToString() + "M";

            newSign.parent = transform;
        }

        Transform newCastle = Instantiate(castle, new Vector2(GameController.TrackDistance, transform.position.y), Quaternion.identity) as Transform;
        newCastle.parent = transform;
	}
}
