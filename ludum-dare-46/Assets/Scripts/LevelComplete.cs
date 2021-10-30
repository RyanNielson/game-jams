using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public string nextLevel = "";

    private int activatedCount = 0;

    private int requiredActivatedCount = 0;

    public Material activatedMaterial;
    public Material deactivatedMaterial;

    public AudioClip activatedSound;
    public AudioClip deactivatedSound;

    public AudioClip exitSound;

    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PushSwitch[] switches = GameObject.FindObjectsOfType<PushSwitch>();
        requiredActivatedCount = switches.Length;
        if (activatedCount < requiredActivatedCount)
        {
            GetComponent<MeshRenderer>().material = deactivatedMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = activatedMaterial;
        }

        foreach (PushSwitch swi in switches)
        {
            swi.attachedLevelComplete = this;

        }
    }

    public void Activate()
    {
        activatedCount++;

        if (activatedCount >= requiredActivatedCount)
        {
            GetComponent<MeshRenderer>().material = activatedMaterial;
            audioSource.PlayOneShot(activatedSound);
        }
    }

    public void Deactivate()
    {
        if (activatedCount >= requiredActivatedCount)
        {
            audioSource.PlayOneShot(deactivatedSound);
        }
        activatedCount--;
        GetComponent<MeshRenderer>().material = deactivatedMaterial;
    }

    public void CompleteLevel(Piece piece)
    {
        if (piece.canTriggerLevelComplete && activatedCount >= requiredActivatedCount)
        {
            Player player = GameObject.FindObjectOfType<Player>();
            player.canMove = false;
            player.hasWon = true;

            StartCoroutine(SwitchLevel());
        }
    }

    IEnumerator SwitchLevel()
    {
        audioSource.PlayOneShot(exitSound);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // SceneManager.GetActiveScene().buildIndex + 1;
        // SceneManager.LoadScene(nextLevel);

    }
}
