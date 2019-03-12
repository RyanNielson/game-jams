using UnityEngine;
using System.Collections;

public class PositionOnMouse : MonoBehaviour 
{
    private Camera gameCamera;
    
    private void Awake()
    {
        Screen.showCursor = false;
        gameCamera = Camera.main;
    }

    void Update()
    {
        float targetAspect = 320f / 240f;
        
        float screenWidthFactor = 320f / Screen.width;
        float screenHeightFactor = 240f / Screen.height;
        
        //  Get mouse position and scale because we use RenderTextures.
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.Scale(new Vector3(screenWidthFactor, screenHeightFactor, 0));
        Vector3 mouseWorldPosition = gameCamera.ScreenToWorldPoint(mousePosition);
        
        mouseWorldPosition.Scale (new Vector3(targetAspect, 1f, 0));

        transform.position = mouseWorldPosition;
    }
}
