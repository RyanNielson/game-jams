using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ViewBlitter : MonoBehaviour 
{
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture view = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().targetTexture;
        view.filterMode = FilterMode.Point;
        Graphics.Blit(view, dest);
    }
}
