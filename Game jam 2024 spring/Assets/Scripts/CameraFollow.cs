using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Drag your player GameObject here
    public float smoothTime = 0.3f; // Adjust this to control the smoothness of the camera follow

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the target position with an offset if needed
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Use SmoothDamp to smoothly move the camera towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
