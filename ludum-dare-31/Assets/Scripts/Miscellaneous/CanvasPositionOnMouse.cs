using UnityEngine;
using System.Collections;

public class CanvasPositionOnMouse : MonoBehaviour 
{
    public Canvas canvas;

    private RectTransform canvasRectTransform;

    private RectTransform cursorRectTransform; 

	private void Awake()
	{
		Screen.showCursor = false;

        canvasRectTransform = canvas.GetComponent<RectTransform>();
        cursorRectTransform = GetComponent<RectTransform>();
	}

	void Update()
	{
        Vector2 localPoint = new Vector2();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, canvas.camera, out localPoint);

        cursorRectTransform.localPosition = new Vector3(Mathf.Clamp(localPoint.x, -canvasRectTransform.sizeDelta.x / 2, canvasRectTransform.sizeDelta.x / 2), Mathf.Clamp(localPoint.y, -canvasRectTransform.sizeDelta.y / 2, canvasRectTransform.sizeDelta.y / 2), 0);
	}
}
