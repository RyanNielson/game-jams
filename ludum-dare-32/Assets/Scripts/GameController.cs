using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{
    private List<string> inputStrings;

    public static int TrackDistance = 150;

    public static int NumberOfPlayers = 2;

    public static bool switchKeys = true;

    private GameObject gameOverPanel;

    [SerializeField]
    public Sprite sleepyRat;

    public float startDelay = 2f;

    private AudioSource audioSource;

    public bool allplayersFinished = false;

    private void Awake()
    {
        Time.timeScale = 1;

        string[] tempInputArray = {
            "Q", "W", "E", "T", "Y", "U", "I", "O", "P", 
            "A", "S", "D", "F", "G", "H", "J", "K", "L",
            "Z", "X", "C", "V", "B", "N", "M"   
        };

        inputStrings = new List<string>(tempInputArray);

        for (int i = 0; i < 4; i++)
        {
            if (i >= GameController.NumberOfPlayers)
            {
                GameObject track = GameObject.FindGameObjectWithTag("Track" + (i + 1).ToString());
                track.GetComponent<Track>().Deactivate();

                GameObject trackSlider = GameObject.FindGameObjectWithTag("Track" + (i + 1).ToString() + "Slider");
                trackSlider.SetActive(false);

                track.GetComponent<Track>().travelTimeText.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        gameOverPanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void StartTracks()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < GameController.NumberOfPlayers)
            {
                GameObject track = GameObject.FindGameObjectWithTag("Track" + (i + 1).ToString());
                track.GetComponent<Track>().AllowMovement();
            }
        }
    }

    public void StartGame()
    {
        audioSource.Play();
        StartTracks();
    }

    public string GetInputString()
    {
        int randomIndex = Random.Range(0, inputStrings.Count);
        string randomInputString = inputStrings[randomIndex];

        inputStrings.RemoveAt(randomIndex);
        
        return randomInputString;
    }

    public void ResetInputString(string key)
    {
        if (key != null)
        {
            inputStrings.Add(key);
        }
    }

    private void Update()
    {
        bool allPlayersComplete = true;

        for (int i = 0; i < GameController.NumberOfPlayers; i++)
        {
            Track track = GameObject.FindGameObjectWithTag("Track" + (i + 1).ToString()).GetComponent<Track>();
            
            if (track.moveable)
            {
                allPlayersComplete = false;
            }
        }

        if (allPlayersComplete)
        {
            gameOverPanel.SetActive(true);
            allplayersFinished = true;
        }

        if (!allPlayersComplete && Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameOverPanel.activeSelf)
            {
                gameOverPanel.SetActive(false);
                Time.timeScale = 1;
            }
            else 
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (gameOverPanel.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}
