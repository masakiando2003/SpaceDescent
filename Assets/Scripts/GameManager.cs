using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] float coreRemainingDistance = 10f;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] enemyPathPrefabs;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas winCanvas;
    [SerializeField] Text remainingDistanceText;
    [SerializeField] RockWaveConfig[] rockWaveConfigs;
    [SerializeField] EnemyWaveConfig[] enemyWaveConfigs;
    [SerializeField] ScientistsWaveConfig[] scientistsWaveConfigs;

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
            CheckCoreRemainingDistance();
            SpawnRocks();
            SpawnScientists();
            SpawnUFOs();
        }
    }

    private void SpawnRocks()
    {
        for(int i = 0; i < rockWaveConfigs.Length; i++)
        {
            if (FindObjectsOfType<Rock>().Length >= rockWaveConfigs[i].GetMaxSpawnNum() ||
                rockWaveConfigs.Length <= 0 ||
                rockWaveConfigs[i].GetRockPrefab() == null)
            {
                return;
            }
            float coreRemainingThresold = rockWaveConfigs[i].GetCoreRemainingDistanceThresold();

            if (Mathf.Floor(coreRemainingDistance) >= coreRemainingThresold)
            {
                int randomRockNums = Random.Range(1, rockWaveConfigs[i].GetMaxSpawnNum());
                for(int j = 0; j < randomRockNums; j++)
                {
                    float randomRockSpeed = Random.Range(rockWaveConfigs[i].GetMinSpeed(), rockWaveConfigs[i].GetMaxSpeed());
                    int randomSpawnPointIndex = Random.Range(0, rockWaveConfigs[i].GetRockSpawnPoints().Count);
                    List<Transform> rockSpawnPositions = rockWaveConfigs[i].GetRockSpawnPoints();
                    GameObject rock = Instantiate(rockWaveConfigs[i].GetRockPrefab(),
                        rockSpawnPositions[randomSpawnPointIndex].transform.position,
                        Quaternion.identity);
                    rock.GetComponent<Rock>().SetMovementSpeed(randomRockSpeed);
                    if (GameObject.Find("Obstacles") != null)
                    {
                        rock.transform.SetParent(GameObject.Find("Obstacles").transform);
                    }
                }
            }
        }
    }

    public void SpawnScientists()
    {
        for (int i = 0; i < scientistsWaveConfigs.Length; i++)
        {
            if (FindObjectsOfType<StrangeScientist>().Length >= scientistsWaveConfigs[i].GetMaxSpawnNum() ||
                scientistsWaveConfigs.Length <= 0 ||
                scientistsWaveConfigs[i].GetScientistPrefab() == null)
            {
                return;
            }
            float coreRemainingThresold = scientistsWaveConfigs[i].GetCoreRemainingDistanceThresold();

            if (Mathf.Floor(coreRemainingDistance) >= coreRemainingThresold)
            {
                int randomScientistsNum = Random.Range(1, scientistsWaveConfigs[i].GetMaxSpawnNum());
                for (int j = 0; j < randomScientistsNum; j++)
                {
                    int randomSpawnPointIndex = Random.Range(0, scientistsWaveConfigs[i].GetScientistSpawnPoints().Count);
                    List<Transform> scientistSpawnPositions = scientistsWaveConfigs[i].GetScientistSpawnPoints();
                    GameObject scientist = Instantiate(scientistsWaveConfigs[i].GetScientistPrefab(),
                        scientistSpawnPositions[randomSpawnPointIndex].transform.position,
                        Quaternion.identity);
                    float randomFallFactor = Random.Range(scientistsWaveConfigs[i].GetMinFallDownFactor(), scientistsWaveConfigs[i].GetMaxFallDownFactor());
                    scientist.GetComponent<StrangeScientist>().SetFallDownFactor(randomFallFactor);
                    if (GameObject.Find("Strange_Scienists") != null)
                    {
                        scientist.transform.SetParent(GameObject.Find("Strange_Scienists").transform);
                    }
                }
            }
        }
    }
    private void SpawnUFOs()
    {
        for (int i = 0; i < enemyWaveConfigs.Length; i++)
        {
            if (FindObjectsOfType<UFO>().Length >= enemyWaveConfigs[i].GetMaxSpawnNum() ||
                enemyWaveConfigs.Length <= 0 ||
                enemyWaveConfigs[i].GetEnemyPrefab() == null)
            {
                return;
            }
            float coreRemainingThresold = enemyWaveConfigs[i].GetCoreRemainingDistanceThresold();

            if (Mathf.Floor(coreRemainingDistance) >= coreRemainingThresold)
            {
                int randomUFONums = Random.Range(1, enemyWaveConfigs[i].GetMaxSpawnNum());
                for(int j = 0; j < randomUFONums; j++)
                {
                    int randomSpawnPointIndex = Random.Range(0, enemyWaveConfigs[i].GetPathPrefabsCount());
                    List<Transform> rockSpawnPositions = enemyWaveConfigs[i].GetWaypoints(randomSpawnPointIndex);
                    GameObject ufo = Instantiate(enemyWaveConfigs[i].GetEnemyPrefab(),
                        rockSpawnPositions[randomSpawnPointIndex].transform.position,
                        Quaternion.identity);
                    ufo.GetComponent<EnemyPathing>().Initialization(randomSpawnPointIndex);
                    if (GameObject.Find("UFOs") != null)
                    {
                        ufo.transform.SetParent(GameObject.Find("UFOs").transform);
                    }
                }
            }
        }
    }

    private void CheckCoreRemainingDistance()
    {
        if(Mathf.Floor(coreRemainingDistance) <= 0.0f)
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
