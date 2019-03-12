using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BushesContainer : MonoBehaviour 
{
    [SerializeField]
    private Transform foregroundBushes;

    [SerializeField]
    private Transform backgroundBushes;

    private void Start()
    {
        if (name == "ForegroundBushesContainer")
        {
            for (int i = -32; i <= 1500; i += 32)
            {
                Transform newSign = Instantiate(foregroundBushes, new Vector2(i, transform.position.y), Quaternion.identity) as Transform;
                newSign.parent = transform;
            }
        }

        if (name == "BackgroundBushesContainer")
        {
            for (int i = -32; i <= 1500; i += 32)
            {
                Transform newSign = Instantiate(backgroundBushes, new Vector2(i - 0.5f, transform.position.y + 0.1f), Quaternion.identity) as Transform;
                newSign.parent = transform;
            }
        }
    }
}
