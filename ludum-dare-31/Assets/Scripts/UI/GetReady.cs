using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetReady : MonoBehaviour 
{
    private Text text;

    private Camera gameCamera;

    private GameController gameController;

	// Use this for initialization
	void Awake () 
    {
        text = GetComponent<Text>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameCamera = Camera.main;

        if (GameData.currentLevel != 1)
        {
            text.enabled = false;
        }
	}

    void Start()
    {
        StartCoroutine(SwitchToMurderMode());
    }

    private IEnumerator SwitchToMurderMode()
    {
        yield return new WaitForSeconds(1.6f);
        text.color = Color.red;
        text.text = "MURDER!!!";
        Go.to(gameCamera.transform, .4f, new GoTweenConfig().shake(new Vector3(.5f, .5f, 0f), GoShakeType.Position, 1, true));

        StartCoroutine(StartPlaying());
    }

    private IEnumerator StartPlaying()
    {
        yield return new WaitForSeconds(.5f);
        text.enabled = false;
        gameController.started = true;
    }
}
