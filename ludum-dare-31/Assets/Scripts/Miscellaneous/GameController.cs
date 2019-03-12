using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public bool gameOver = false;

    public CanvasGroup gameOverCanvasGroup;

    public CrazyExplosionMachine crazyExplosionMachine;

    public BackgroundMusicPlayer backgroundMusicPlayer;

    public AudioClip deathMusic;

    public AudioClip getReadyAudio;

    public AudioClip shotgunAudio;

    public bool started = false;

    private bool gameOverTriggered = false;

    private GameObject backgroundMusicPlayerObject;

    private AudioSource audioSource;

    void Awake()
    {
        GameData.timeRemaining = 12f;

        if (!GameObject.FindGameObjectWithTag("BackgroundMusicPlayer"))
        {
            Instantiate(backgroundMusicPlayer);
        }

        backgroundMusicPlayerObject = GameObject.FindGameObjectWithTag("BackgroundMusicPlayer");

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (GameData.currentLevel == 1)
        {
            started = false;
            audioSource.clip = getReadyAudio;
            audioSource.Play();
        }
        else 
        {
            started = true;
            audioSource.clip = shotgunAudio;
            audioSource.Play();
        }
    }

	void Update() 
    {
        if (!gameOver && started)
        {
            GameData.timeRemaining -= Time.deltaTime;

            if (GameData.timeRemaining <= 0)
            {
                GameData.timeRemaining = 0;

                Instantiate(crazyExplosionMachine);

                backgroundMusicPlayerObject.GetComponent<AudioSource>().Stop();

                gameOver = true;
            }
        }

        if (gameOver && !gameOverTriggered)
        {
            gameOverTriggered = true;
            OnGameOver();
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (GameObject.FindGameObjectWithTag("BackgroundMusicPlayer"))
                {
                    Destroy(GameObject.FindGameObjectWithTag("BackgroundMusicPlayer"));
                }

                GameData.currentLevel = 1;

                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}

    private void OnGameOver()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<PlayerHealth>().Die();

        if (GameData.timeRemaining > 0)
        {
            backgroundMusicPlayerObject.GetComponent<AudioSource>().Stop();
            backgroundMusicPlayerObject.GetComponent<AudioSource>().PlayOneShot(deathMusic, .1f);
        }

        StartCoroutine(ShowGameoverMenu());
    }

    private IEnumerator ShowGameoverMenu()
    {
        yield return new WaitForSeconds(1f);

        gameOverCanvasGroup.alpha = 1f;
        gameOverCanvasGroup.blocksRaycasts = true;
        gameOverCanvasGroup.interactable = true;
    }
}
