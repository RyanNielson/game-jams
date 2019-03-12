using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemiesRemaining : MonoBehaviour 
{
    //private GameController gameController;

    private Text text;


    void Awake()
    {
        //gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        text = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () 
    {
        text.text = "Remaining: " + GameData.enemiesRemaining;
	}
}
