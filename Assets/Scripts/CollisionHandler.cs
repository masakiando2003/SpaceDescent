using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collisionObj)
    {
        Debug.Log("Trigger Entered!");
        switch (collisionObj.gameObject.tag)
        {
            case "Enemy":
            case "Obstacles":
                FindObjectOfType<GameManager>().GameOver();
                break;
        }
    }
}
