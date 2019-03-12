using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sign : MonoBehaviour 
{
    public string text;

    [SerializeField]
    private Text uiText;

	void Start() 
    {
        uiText.text = text;
	}
}
