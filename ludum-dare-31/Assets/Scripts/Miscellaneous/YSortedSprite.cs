using UnityEngine;
using System.Collections;

public class YSortedSprite : MonoBehaviour 
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate() 
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * -1000f);
    }
}
