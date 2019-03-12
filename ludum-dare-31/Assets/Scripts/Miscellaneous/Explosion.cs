using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
    public void DestroyAfterPlaying()
    {
        Destroy(gameObject);
    }
}
