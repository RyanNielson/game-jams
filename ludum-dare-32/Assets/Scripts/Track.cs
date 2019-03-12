using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class Track : MonoBehaviour 
{
    [SerializeField]
    private float bobSpeed = 0.1f;

    [SerializeField]
    private Text leftInputText;

    [SerializeField]
    private Text rightInputText;

    [SerializeField]
    public float distanceTravelled = 0f;

    [SerializeField]
    private Transform signs;

    [SerializeField]
    public Text travelTimeText;

    private float travelTime = 0f;

    public Sprite largeSprite;
    public Sprite smallSprite;
    public Sprite sleepySprite;

    private string leftKey;
    private string rightKey;
    private string nextKey;

    private GameController gameController;

    private Transform playerTransform;

    private Transform playerCanvas;

    [NonSerialized]
    public bool moveable = true;

    public bool deactivated = false;

    [NonSerialized]
    public bool started = false;

    private AudioSource audioSource;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerTransform = transform.FindChild("Player");
        playerCanvas = transform.FindChild("Canvas");
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (deactivated)
        {
            transform.FindChild("Player").GetComponent<SpriteRenderer>().sprite = sleepySprite;
        }
        else
        {
            transform.FindChild("Player").GetComponent<SpriteRenderer>().sprite = largeSprite;
        }

        travelTimeText.text = travelTime.ToString("0.00");

        AssignKeys();
    }

    private IEnumerator ReassignKeys()
    {
        yield return new WaitForSeconds(6f);

        if (name == "Track1" && !gameController.allplayersFinished)
        {
            audioSource.Play();
        }
        AssignKeys();

        StartCoroutine(ReassignKeys());
    }

    public void AssignKeys()
    {
        bool rightKeyNext = nextKey == rightKey;

        gameController.ResetInputString(leftKey);
        gameController.ResetInputString(rightKey);

        leftKey = gameController.GetInputString();
        rightKey = gameController.GetInputString();

        leftInputText.text = leftKey;
        rightInputText.text = rightKey;

        if (rightKeyNext)
        {
            nextKey = rightKey;
            leftInputText.color = new Color(1f, 1f, 1f, 0.5f);
            rightInputText.color = new Color(1f, 1f, 1f, 1f);
            leftInputText.fontSize = 8;
            rightInputText.fontSize = 10;
        }
        else
        {
            nextKey = leftKey;
            leftInputText.color = new Color(1f, 1f, 1f, 1f);
            rightInputText.color = new Color(1f, 1f, 1f, 0.5f);
            leftInputText.fontSize = 10;
            rightInputText.fontSize = 8;
        }
    }

    public void Deactivate()
    {
        moveable = false;
        deactivated = true;
    }

    public void AllowMovement()
    {
        started = true;

        if (GameController.switchKeys)
        {
            StartCoroutine(ReassignKeys());
        }
    }

	void Update () 
    {
        if (moveable && started)
        {
            travelTime += Time.deltaTime;
            travelTimeText.text = travelTime.ToString("0.00");

            CheckKeys();
        }

        if (deactivated)
        {
            moveable = false;
            playerCanvas.gameObject.SetActive(false);
            signs.gameObject.SetActive(false);
        }
        
	}

    private void CheckKeys()
    {
        KeyCode nextKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), nextKey);

        if (Input.GetKeyUp(nextKeyCode) && moveable)
        {
            if (nextKey == leftKey)
            {
                playerTransform.DORotate(new Vector3(0, 0, 30f), bobSpeed);
                nextKey = rightKey;
                leftInputText.color = new Color(1f, 1f, 1f, 0.5f);
                rightInputText.color = new Color(1f, 1f, 1f, 1f);
                leftInputText.fontSize = 8;
                rightInputText.fontSize = 10;
            }
            else if (nextKey == rightKey)
            {
                playerTransform.DORotate(new Vector3(0, 0, -30f), bobSpeed);
                nextKey = leftKey;
                rightInputText.color = new Color(1f, 1f, 1f, 0.5f);
                leftInputText.color = new Color(1f, 1f, 1f, 1f);
                rightInputText.fontSize = 8;
                leftInputText.fontSize = 10;
            }

            distanceTravelled++;
        }

        if (distanceTravelled >= GameController.TrackDistance)
        {
            moveable = false;
            playerTransform.gameObject.SetActive(false);
            playerCanvas.gameObject.SetActive(false);
        }
    }
}
