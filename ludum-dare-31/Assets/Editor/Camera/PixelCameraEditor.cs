using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PixelCamera))]
public class PixelCameraEditor : Editor
{
    private PixelCamera pixelCamera;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate")) 
        {
            pixelCamera = (PixelCamera)target;

            RemoveExistingPixelCameraItems();

            CreatePixelCameraItems();
        }
    }

    private void RemoveExistingPixelCameraItems()
    {
        if (pixelCamera.viewRenderTexture) 
        {
            DestroyImmediate(pixelCamera.viewRenderTexture);
        }

        if (pixelCamera.viewQuad) 
        {
            DestroyImmediate(pixelCamera.viewQuad);
        }

        if (pixelCamera.viewCamera) 
        {
            GameObject.DestroyImmediate(pixelCamera.viewCamera.gameObject);
        }
    }

    private void CreateRenderTexture()
    {
        pixelCamera.viewRenderTexture = new RenderTexture(pixelCamera.widthInPixels, pixelCamera.heightInPixels, 24);
        pixelCamera.viewRenderTexture.name = "ViewRenderTexture";
        pixelCamera.viewRenderTexture.filterMode = FilterMode.Point;
        pixelCamera.camera.targetTexture = pixelCamera.viewRenderTexture;
    }

    private void CreateViewCamera()
    {
        Camera viewCamera = (new GameObject("ViewCamera", typeof(Camera))).GetComponent<Camera>();

        viewCamera.transform.position = new Vector3 (10000, 10000, 10000);
        viewCamera.name = "ViewCamera";
        viewCamera.orthographic = true;
        viewCamera.orthographicSize = 0.5f;
        viewCamera.aspect = pixelCamera.aspect;
        viewCamera.backgroundColor = Color.black;

        pixelCamera.viewCamera = viewCamera;
    }

    private void CreateViewPlane()
    {
        GameObject viewQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Material material = new Material(Shader.Find("Unlit/Texture"));

        viewQuad.AddComponent<Letterboxer>();
        DestroyImmediate(viewQuad.GetComponent<MeshCollider>());

        viewQuad.name = "ViewQuad";
        viewQuad.transform.parent = pixelCamera.viewCamera.transform;
        viewQuad.transform.localPosition = Vector3.forward;
        viewQuad.transform.localScale = new Vector3(pixelCamera.aspect, 1f, 1f);

        material.mainTexture = pixelCamera.viewRenderTexture;
        viewQuad.renderer.material = material;

        pixelCamera.viewQuad = viewQuad;
    }

    private void CreatePixelCameraItems()
    {
        pixelCamera.aspect = (float)pixelCamera.widthInPixels / (float)pixelCamera.heightInPixels;

        CreateRenderTexture();

        CreateViewCamera();

        CreateViewPlane();
    }
}
