using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip hitRockSE;
    [SerializeField] AudioClip hitLaserSE;
    AudioSource audioSource;

    private void Start()
    {
        if(GetComponent<AudioSource>() == null) { return; }
        audioSource = GetComponent<AudioSource>();
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
                case "Obstacles":
                case "Enemy":
                    if(hitRockSE != null)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(hitRockSE);
                    }
                    Destroy(collisionObj.gameObject);
                    gameObject.GetComponent<Player>().Die();
                    FindObjectOfType<GameManager>().GameOver();
                    break;
                case "Laser":
                    if (hitLaserSE != null)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(hitLaserSE);
                    }
                    Destroy(collisionObj.gameObject);
                    gameObject.GetComponent<Player>().Die();
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
        gameObject.GetComponent<Animator>().SetTrigger("StuckTrigger");
        gameObject.GetComponent<Player>().SlowDownSpeed();
        yield return new WaitForSeconds(gameObject.GetComponent<Player>().GetSlowDownTimer());
        gameObject.GetComponent<Player>().ResetToNormalSpeed();
        enemy.GetComponent<StrangeScientist>().ReleaseAttach();
        enemy.GetComponent<StrangeScientist>().StartMovement();
    }
}
