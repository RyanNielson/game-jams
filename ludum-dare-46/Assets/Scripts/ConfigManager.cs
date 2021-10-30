using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public Toggle fullScreenToggle;
    public Toggle limitMovesToggle;

    public static bool limitMoves = true;

    // Start is called before the first frame update
    void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;
        limitMovesToggle.isOn = limitMoves;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleFullscreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void ToggleLimitMoves(bool value)
    {
        limitMoves = value;
    }
}
