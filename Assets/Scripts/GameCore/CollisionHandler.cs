using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collisionObj)
    {
        if(gameObject.tag == "Player")
        {
            switch (collisionObj.gameObject.tag)
            {
                case "Core":
                    collisionObj.gameObject.GetComponent<Core>().AttachToPlayer();
                    FindObjectOfType<GameManager>().Win();
                    break;
                case "Enemy":
                case "Obstacles":
                    FindObjectOfType<GameManager>().GameOver();
                    break;
                case "Laser":
                    Destroy(collisionObj.gameObject);
                    FindObjectOfType<GameManager>().GameOver();
                    break;
                case "Scientists":
                    StartCoroutine(SlowDownPlayerSpeed(collisionObj.gameObject));
                    break;
            }
        }
    }

    private IEnumerator SlowDownPlayerSpeed(GameObject enemy)
    {
        enemy.GetComponent<StrangeScientist>().StopMovement();
        enemy.GetComponent<StrangeScientist>().AttachToPlayer();
        gameObject.GetComponent<Player>().SlowDownSpeed();
        yield return new WaitForSeconds(gameObject.GetComponent<Player>().GetSlowDownTimer());
        gameObject.GetComponent<Player>().ResetToNormalSpeed();
        enemy.GetComponent<StrangeScientist>().ReleaseAttach();
        enemy.GetComponent<StrangeScientist>().StartMovement();
    }
}
