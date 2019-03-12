using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    public CanvasGroup aboutPanel;

    public CanvasGroup mainMenuPanel;

    public void Play()
    {
        Application.LoadLevel("Game");
    }

    public void About()
    {
        mainMenuPanel.alpha = 0;
        mainMenuPanel.blocksRaycasts = false;
        aboutPanel.alpha = 1;
        aboutPanel.blocksRaycasts = true;
    }

    public void ShowMainMenu()
    {
        aboutPanel.alpha = 0;
        aboutPanel.blocksRaycasts = false;
        mainMenuPanel.alpha = 1;
        mainMenuPanel.blocksRaycasts = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
