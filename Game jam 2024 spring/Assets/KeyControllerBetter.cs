using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControllerBetter : MonoBehaviour
{
    public DoorController controller;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("doooorus");
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("open door?");
        // Check if the collider collided with has a specific tag (you can use other criteria)
        if (collision.CompareTag("Player"))
        {
            // Call your function when colliding with a collider with the specified tag
            controller.OpenDoor();
        }
    }
}
