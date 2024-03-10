using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sr;
    Collider2D collider;
    public Sprite opendoorSprite;
    void Start()
    {
        sr= GetComponent<SpriteRenderer>();
        collider= GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        Debug.Log("opening door");
        collider.enabled= false;
        sr.sprite= opendoorSprite;
    }
}
