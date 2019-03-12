using UnityEngine;

[ExecuteInEditMode]
public class Letterboxer : MonoBehaviour
{
    void Start()
    {
        Letterbox();
    }

    void Update()
    {
        Letterbox();
    }

    void Letterbox()
    {
        // The desired aspect ratio
        float targetAspect = 320f / 240f;
        
        // Game window's current aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Current viewport height should be scaled by this amount
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f) 
        {
            // Add a letterbox.
            transform.localScale = new Vector3(targetAspect * scaleHeight, scaleHeight, transform.localScale.z);
        }
        else 
        {
            // Add a pillarbox.
            transform.localScale = new Vector3(targetAspect, 1f, transform.localScale.z);
        }
    }
}
