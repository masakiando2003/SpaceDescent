using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundries : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collisionObj)
    {
        Destroy(collisionObj.gameObject);
    }
}
