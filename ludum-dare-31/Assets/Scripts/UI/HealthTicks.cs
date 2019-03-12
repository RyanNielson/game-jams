using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthTicks : MonoBehaviour 
{
    private Health playerHealth;

    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tick = transform.GetChild(i);
            Image image = tick.GetComponent<Image>();
            Outline outline = tick.GetComponent<Outline>();

            if (i < playerHealth.health)
            {
                image.color = Color.red;
                outline.effectColor = Color.red;
            }
            else 
            {
                image.color = Color.black;
                outline.effectColor = Color.white;
            }
        }
	}
}
