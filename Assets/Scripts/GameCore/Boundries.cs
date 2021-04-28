using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundries : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collisionObj)
    {
        if(collisionObj.gameObject.tag == "Core" || collisionObj.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        Destroy(collisionObj.gameObject);
    }
}
