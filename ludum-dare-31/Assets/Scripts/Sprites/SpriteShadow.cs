using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteShadow : MonoBehaviour 
{
    public float minFlipped = 90f;
    public float maxFlipped = 270f;
    
    public bool flipBetweenAngles = false;
    
    [RangeAttribute(0, 359f)]
    public float angle;
    
    public Color tint = new Color(0, 0, 0, .66f);
    
    public Vector2 size = new Vector2(1f, 1f);
    
    public Vector2 offset = Vector2.zero;
    
    public bool useParentsSprite = true;

    public Sprite replacementSprite;
    
    private SpriteRenderer spriteRenderer = null;
    
    private SpriteRenderer parentSpriteRenderer;
    
    void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.black;
    }
    
    void Update() 
    {
        SetSprite();
        
        spriteRenderer.color = tint;
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        transform.localPosition = offset;
        
        if (transform.rotation.eulerAngles.z > minFlipped && transform.rotation.eulerAngles.z < maxFlipped)
        {
            transform.localScale = new Vector3(-1 * size.x, size.y, transform.localScale.z);
        }
        else 
        {
            transform.localScale = new Vector3(size.x, size.y, transform.localScale.z);
        }
    }
    
    private void SetSprite()
    {
        if (useParentsSprite)
        {
            spriteRenderer.sprite = parentSpriteRenderer.sprite;
        }
        else 
        {
            spriteRenderer.sprite = replacementSprite;
        }
    }
}