using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCounter : MonoBehaviour
{
    public int maximumActions = 1;
    public int actionsSoFar = 0;

    public AudioClip limitAudio;

    // public

    // public TextMesh text;

    public Text text;

    public bool triggered = false;

    AudioSource audioSource;

    void Start()
    {
        text.text = maximumActions.ToString();
        audioSource = GetComponent<AudioSource>();
        if (!ConfigManager.limitMoves)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Player player = GameObject.FindObjectOfType<Player>();

        // player.canMove = false;
        if (!player.hasWon && maximumActions - actionsSoFar <= 0 && !triggered)
        {
            // Player player = GameObject.FindObjectOfType<Player>();
            player.canMove = false;

            text.text = "PRESS R TO RESET";
            text.color = Color.red;
            triggered = true;
            audioSource.PlayOneShot(limitAudio);
        }
    }

    public void IncrementActions()
    {
        actionsSoFar++;

        actionsSoFar = Mathf.Clamp(actionsSoFar, 0, maximumActions);

        text.text = (maximumActions - actionsSoFar).ToString();
    }
}
