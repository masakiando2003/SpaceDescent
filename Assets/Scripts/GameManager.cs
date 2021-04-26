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

    enum GameState
    {
        Ready,
        Start,
        Win,
        GameOver
    }

    private static GameState gameState;

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
        gameState = GameState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.Start)
        {
            UpdatePlayerRemainingDistance();
            CheckRemainingDistance();
        }
    }

    private void CheckRemainingDistance()
    {
        if(Mathf.Floor(remainingDistance) <= 0.0f)
        {
            Win();
        }
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
        gameState = GameState.GameOver;
    }

    public void Win()
    {
        winCanvas.enabled = true;
        player.GetComponent<Player>().enabled = false;
        player.GetComponent<CollisionHandler>().enabled = false;
        gameState = GameState.Win;
    }
}
