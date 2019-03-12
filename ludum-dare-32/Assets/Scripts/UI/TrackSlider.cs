using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrackSlider : MonoBehaviour 
{
    [SerializeField]
	private Track track;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = track.distanceTravelled / GameController.TrackDistance;
    }
}
