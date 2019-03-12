using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentLevel : MonoBehaviour 
{
    private Text text;

	void Awake() 
    {
        text = GetComponent<Text>();
	}

	void Update() 
    {
        text.text = "LEVEL " + GameData.currentLevel;
	}
}
