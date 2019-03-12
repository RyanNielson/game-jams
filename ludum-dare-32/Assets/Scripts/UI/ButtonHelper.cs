using UnityEngine;
using System.Collections;

public class ButtonHelper : MonoBehaviour 
{
    public void Load1PlayerRace()
    {
        GameController.switchKeys = true;
        GameController.TrackDistance = 150;
        GameController.NumberOfPlayers = 1;
        LoadRaceScene();
    }

    public void Load2PlayerRace()
    {
        GameController.switchKeys = true;
        GameController.TrackDistance = 150;
        GameController.NumberOfPlayers = 2;
        LoadRaceScene();
    }

    public void Load3PlayerRace()
    {
        GameController.switchKeys = true;
        GameController.TrackDistance = 150;
        GameController.NumberOfPlayers = 3;
        LoadRaceScene();
    }

    public void Load4PlayerRace()
    {
        GameController.switchKeys = true;
        GameController.TrackDistance = 150;
        GameController.NumberOfPlayers = 4;
        LoadRaceScene();
    }

    public void Load1PlayerMarathon()
    {
        GameController.switchKeys = false;
        GameController.TrackDistance = 1000;
        GameController.NumberOfPlayers = 1;
        LoadRaceScene();
    }

    public void Load2PlayerMarathon()
    {
        GameController.switchKeys = false;
        GameController.TrackDistance = 1000;
        GameController.NumberOfPlayers = 2;
        LoadRaceScene();
    }

    public void Load3PlayerMarathon()
    {
        GameController.switchKeys = false;
        GameController.TrackDistance = 1000;
        GameController.NumberOfPlayers = 3;
        LoadRaceScene();
    }

    public void Load4PlayerMarathon()
    {
        GameController.switchKeys = false;
        GameController.TrackDistance = 1000;
        GameController.NumberOfPlayers = 4;
        LoadRaceScene();
    }

    public void RestartScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadScene(string scene)
    {
        Application.LoadLevel(scene);
    }

	public void LoadRaceScene()
    {
        LoadScene("Race");
    }

    public void ExitGame()
    {
        if (Application.isEditor)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        else
        {
            Application.Quit();
        }
    }

    public void PlaySound(AudioClip clickClip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource)
        {
            audioSource.PlayOneShot(clickClip);
        }
    }
}
