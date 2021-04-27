using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] float coreRemainingDistance = 10f;
    [SerializeField] float spawnRockRandomTime = 100f, spawnRockTimeFactor = 50f;
    [SerializeField] float minRockRandomSpeed = 10f, maxRockRandomSpeed = 30f;
    [SerializeField] float ufoWave1Distance = 900f, ufoWave2Distance = 650f, ufoWave3Distance = 300f;
    [SerializeField] int maxRandomRockNums = 3;
    [SerializeField] int maxRocksAvailable = 3;
    [SerializeField] GameObject player;
    [SerializeField] GameObject rockPrefab;
    [SerializeField] Transform[] rocksSpawnPoint;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas winCanvas;
    [SerializeField] Text remainingDistanceText;

    float spawnRockTime;

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
        spawnRockTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.Start)
        {
            UpdatePlayerRemainingDistance();
            CheckRemainingDistance();
            RandomToSpawnRocks();
        }
    }

    private void RandomToSpawnRocks()
    {
        if(rockPrefab == null || FindObjectsOfType<Rock>().Length >= maxRocksAvailable) { return; }
        spawnRockTime = Random.Range(0f, spawnRockRandomTime);
        if(spawnRockTime >= spawnRockTimeFactor)
        {
            int randomRockNums = Random.Range(1, maxRandomRockNums);
            int randomSpawnPointIndex = Random.Range(0, rocksSpawnPoint.Length);
            float randomRockSpeed = Random.Range(minRockRandomSpeed, maxRockRandomSpeed);
            GameObject rock = Instantiate(rockPrefab, 
                rocksSpawnPoint[randomSpawnPointIndex].transform.position, 
                Quaternion.identity);
            rock.GetComponent<Rock>().SetMovementSpeed(randomRockSpeed);
            if(GameObject.Find("Obstacles") != null)
            {
                rock.transform.SetParent(GameObject.Find("Obstacles").transform);
            }
        }
    }

    private void CheckRemainingDistance()
    {
        if (Mathf.Floor(coreRemainingDistance) <= ufoWave1Distance)
        {

        }
        else if (Mathf.Floor(coreRemainingDistance) <= ufoWave2Distance)
        {

        }
        else if (Mathf.Floor(coreRemainingDistance) <= ufoWave3Distance)
        {

        }
        else if(Mathf.Floor(coreRemainingDistance) <= 0.0f)
        {
            FindObjectOfType<Core>().StartFalling();
        }
    }

    private void UpdatePlayerRemainingDistance()
    {
        if(remainingDistanceText == null) { return; }
        coreRemainingDistance -= (coreRemainingDistance * Time.deltaTime) >= 0.0f 
            ? Time.deltaTime 
            : 0.0f;
        remainingDistanceText.text = Mathf.FloorToInt(coreRemainingDistance).ToString() + " km";
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
