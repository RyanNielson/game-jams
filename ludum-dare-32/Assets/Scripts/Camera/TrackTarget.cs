using UnityEngine;
using System.Collections;

public class TrackTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float trackingSpeed = 1f;

    private void LateUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), trackingSpeed * Time.deltaTime);
        }
    }
}