using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float remainingDistance = 1001f;
    [SerializeField] float countDownFactor = 10f;
    [SerializeField] GameObject player;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas winCanvas;
    [SerializeField] Text remainingDistanceText;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        if(gameOverCanvas != null)
        {
            gameOverCanvas.enabled = false;
        }
        if (winCanvas != null)
        {
            winCanvas.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerRemainingDistance();
    }

    private void UpdatePlayerRemainingDistance()
    {
        if(remainingDistanceText == null) { return; }
        remainingDistance -= (remainingDistance * Time.deltaTime * countDownFactor) >= 0.0f 
            ? (remainingDistance * Time.deltaTime * countDownFactor) 
            : 0.0f;
        remainingDistanceText.text = Mathf.FloorToInt(remainingDistance).ToString() + " km";
    }

    public void GameOver()
    {
        player.GetComponent<Player>().Die();
        gameOverCanvas.enabled = true;
    }

    public void Win()
    {
        winCanvas.enabled = true;
    }
}
