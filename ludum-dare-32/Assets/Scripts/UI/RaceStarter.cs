using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaceStarter : MonoBehaviour 
{
    private AudioSource audioSource;

    private Text text;

    private GameController gameController;

    [SerializeField]
    private AudioClip audio3;

    [SerializeField]
    private AudioClip audio2;

    [SerializeField]
    private AudioClip audio1;

    [SerializeField]
    private AudioClip audioGo;

    [SerializeField]
    private float timeBetweenTicks = 0.25f;

	private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();	
        text = GetComponent<Text>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

    private void Start()
    {
        StartCoroutine(Go());
    }
	
    private IEnumerator Go()
    {
        text.text = "3";
        audioSource.PlayOneShot(audio3);
        yield return new WaitForSeconds(timeBetweenTicks);

        text.text = "2";
        audioSource.PlayOneShot(audio2);
        yield return new WaitForSeconds(timeBetweenTicks);

        text.text = "1";
        audioSource.PlayOneShot(audio1);
        yield return new WaitForSeconds(timeBetweenTicks);

        text.text = "GO!";
        audioSource.PlayOneShot(audioGo);
        yield return new WaitForSeconds(timeBetweenTicks);

        gameController.StartGame();

        yield return new WaitForSeconds(0.25f);
        text.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () 
    {
	
	}
}
