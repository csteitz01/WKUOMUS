using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            PlayerController.collectedAmount++;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
