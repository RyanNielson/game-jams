using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PixelCamera : MonoBehaviour
{
    [SerializeField]
    public RenderTexture viewRenderTexture;

    [SerializeField]
    public Camera viewCamera;

    [SerializeField]
    public GameObject viewQuad;

    [SerializeField] 
    public int widthInPixels = 320;

    [SerializeField] 
    public int heightInPixels = 240;

    [SerializeField] 
    public float aspect = 1f;
}
