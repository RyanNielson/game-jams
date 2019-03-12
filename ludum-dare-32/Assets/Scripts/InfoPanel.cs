using UnityEngine;
using System.Collections;

public class InfoPanel : MonoBehaviour 
{
    [SerializeField]
    private GameObject mainMenuPanel;
	
    public void Show()
    {
        mainMenuPanel.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
