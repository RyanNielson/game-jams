using UnityEngine;
using System.Collections;

public class MainMenuPanel : MonoBehaviour 
{
    [SerializeField]
    private GameObject infoPanel;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void Show()
    {
        infoPanel.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
