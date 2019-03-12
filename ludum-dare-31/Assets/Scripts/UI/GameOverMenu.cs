using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour 
{
    public void MainMenu()
    {
        GameData.currentLevel = 1;

        GameObject backgroundMusicPlayer = GameObject.FindGameObjectWithTag("BackgroundMusicPlayer");
        if (backgroundMusicPlayer)
        {
            Destroy(backgroundMusicPlayer);
        }

        Application.LoadLevel("MainMenu");
    }

    public void Restart()
    {
        GameData.currentLevel = 1;
        
        GameObject backgroundMusicPlayer = GameObject.FindGameObjectWithTag("BackgroundMusicPlayer");
        if (backgroundMusicPlayer)
        {
            Destroy(backgroundMusicPlayer);
        }

        Application.LoadLevel("Game");
    }
}
