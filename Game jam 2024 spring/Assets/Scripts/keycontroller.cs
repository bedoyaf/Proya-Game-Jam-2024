using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public DoorController controller;

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("doooorus");
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
