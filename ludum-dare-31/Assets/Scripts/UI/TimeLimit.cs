using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeLimit : MonoBehaviour 
{
    private Text text;
    
    void Awake()
    {
        text = GetComponent<Text>();
    }
    
    void Update () 
    {
        text.text = GameData.timeRemaining.ToString("F");
    }
}
