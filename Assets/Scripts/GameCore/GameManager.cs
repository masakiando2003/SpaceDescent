using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] float startCountDownTime = 3.9f, hideCountDownTime = 1f, readyToStartSeconds = 2f;
    [SerializeField] float speedUpFactor = 1.5f, normalSpeedFactor = 1f;
    [SerializeField] float coreRemainingDistance = 10f;
    [SerializeField] string startGameText = "GO !";
    [SerializeField] GameObject player;
    [SerializeField] GameObject blackHole;
    [SerializeField] Transform[] coreSpawnPoints;
    [SerializeField] Canvas gameOverCanvas, winCanvas, fadeInCanvas, countDownCavnas, coreAppearDistanceCanvas;
    [SerializeField] Text remainingDistanceText, countDownTimerText;
    [SerializeField] RockWaveConfig[] rockWaveConfigs;
    [SerializeField] EnemyWaveConfig[] enemyWaveConfigs;
    [SerializeField] ScientistsWaveConfig[] scientistsWaveConfigs;

    float countDownTimer;
    bool coreStartFalling;

    enum GameState
    {
        Prepare,
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
        gameState = GameState.Ready;
        Time.timeScale = 1;
        if (gameOverCanvas != null)
        {
            gameOverCanvas.enabled = false;
        }
        if (winCanvas != null)
        {
            winCanvas.enabled = false;
        }
        if(blackHole != null)
        {
            blackHole.SetActive(false);
        }
        if(countDownCavnas != null)
        {
            countDownCavnas.enabled = false;
        }
        if(coreAppearDistanceCanvas != null)
        {
            coreAppearDistanceCanvas.enabled = false;
        }
        countDownTimerText.enabled = false;
        countDownTimer = startCountDownTime;
        remainingDistanceText.text = Mathf.Floor(coreRemainingDistance).ToString() + " km";
        coreStartFalling = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameState();

    }

    private void CheckGameState()
    {
        switch (gameState)
        {
            case GameState.Prepare:
                StartCoroutine(FadeInGamePlay());
                break;
            case GameState.Ready:
                EnableCanvas();
                StartToCountDown();
                break;
            case GameState.Start:
                if (countDownTimerText.isActiveAndEnabled)
                {
                    HideCountDownText();
                }
                UpdatePlayerRemainingDistance();
                CheckCoreRemainingDistance();
                SpawnRocks();
                SpawnScientists();
                SpawnUFOs();
                break;
        }
    }

    private void EnableCanvas()
    {
        if (countDownCavnas != null)
        {
            countDownCavnas.enabled = true;
        }
        if (coreAppearDistanceCanvas != null)
        {
            coreAppearDistanceCanvas.enabled = true;
        }
    }

    private void StartToCountDown()
    {
        if (countDownTimerText == null)
        {
            ChangeGameState(GameState.Start);
            return;
        }
        countDownTimerText.enabled = true;
        countDownTimer -= Time.deltaTime;
        CountDown(countDownTimer);
    }

    private IEnumerator FadeInGamePlay()
    {
        yield return new WaitForSeconds(readyToStartSeconds);
        if(fadeInCanvas != null)
        {
            fadeInCanvas.enabled = false;
        }
        if(countDownCavnas != null)
        {
            countDownCavnas.enabled = true;
        }
        ChangeGameState(GameState.Ready);
    }


    private void CountDown(float countDownTimer)
    {
        ShowCountTimeText(countDownTimer);
        if(countDownTimer <= 0.0f)
        {
            Invoke("HideCountDownText", hideCountDownTime);
            ChangeGameState(GameState.Start);
        }
    }

    private static void ChangeGameState(GameState state)
    {
        gameState = state;
    }

    private void ShowCountTimeText(float countDownTimer)
    {
        int remainingSeconds = Mathf.FloorToInt(countDownTimer);
        countDownTimerText.text = (remainingSeconds > 0) ? remainingSeconds.ToString() : startGameText;
    }

    private void HideCountDownText()
    {
        countDownTimerText.enabled = false;
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
            float startWaveDistance = rockWaveConfigs[i].GetStartWaveDistance();
            float endWaveDistance = rockWaveConfigs[i].GetEndWaveDistance();

            if (Mathf.Floor(coreRemainingDistance) >= endWaveDistance && Mathf.Floor(coreRemainingDistance) <= startWaveDistance)
            {
                int randomRockNums = Random.Range(1, rockWaveConfigs[i].GetMaxSpawnNum()+1);
                int previousSpawnIndex = 0; 
                int randomSpawnPointIndex;
                for (int j = 0; j < randomRockNums; j++)
                {
                    float randomRockSpeed = Random.Range(rockWaveConfigs[i].GetMinSpeed(), rockWaveConfigs[i].GetMaxSpeed());
                    randomSpawnPointIndex = Random.Range(0, rockWaveConfigs[i].GetRockSpawnPoints().Count);
                    // Prevent to spawn in same place
                    while (previousSpawnIndex == randomSpawnPointIndex)
                    {
                        randomSpawnPointIndex = Random.Range(0, rockWaveConfigs[i].GetRockSpawnPoints().Count);
                    }
                    previousSpawnIndex = randomSpawnPointIndex;
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
            float startWaveDistance = scientistsWaveConfigs[i].GetStartWaveDistance();
            float endWaveDistance = scientistsWaveConfigs[i].GetEndWaveDistance();

            if (Mathf.Floor(coreRemainingDistance) >= endWaveDistance && Mathf.Floor(coreRemainingDistance) <= startWaveDistance)
            {
                int randomScientistsNum = Random.Range(1, scientistsWaveConfigs[i].GetMaxSpawnNum()+1);
                int previousSpawnIndex = 0;
                int randomSpawnPointIndex;
                for (int j = 0; j < randomScientistsNum; j++)
                {
                    randomSpawnPointIndex = Random.Range(0, scientistsWaveConfigs[i].GetScientistSpawnPoints().Count);
                    // Prevent to spawn in same place
                    while (previousSpawnIndex == randomSpawnPointIndex)
                    {
                        randomSpawnPointIndex = Random.Range(0, scientistsWaveConfigs[i].GetScientistSpawnPoints().Count);
                    }
                    previousSpawnIndex = randomSpawnPointIndex;
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
            float startWaveDistance = enemyWaveConfigs[i].GetStartWaveDistance();
            float endWaveDistance = enemyWaveConfigs[i].GetEndWaveDistance();

            if (Mathf.Floor(coreRemainingDistance) >= endWaveDistance && Mathf.Floor(coreRemainingDistance) <= startWaveDistance)
            {
                int randomUFONums = Random.Range(1, enemyWaveConfigs[i].GetMaxSpawnNum()+1);
                int previousSpawnIndex = 0;
                int randomSpawnPointIndex;
                for (int j = 0; j < randomUFONums; j++)
                {
                    randomSpawnPointIndex = Random.Range(0, enemyWaveConfigs[i].GetPathPrefabsCount());
                    // Prevent to spawn in same place
                    while (previousSpawnIndex == randomSpawnPointIndex)
                    {
                        randomSpawnPointIndex = Random.Range(0, enemyWaveConfigs[i].GetPathPrefabsCount());
                    }
                    previousSpawnIndex = randomSpawnPointIndex;
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
            if (!coreStartFalling)
            {
                if (coreSpawnPoints.Length > 0)
                {
                    int randomSpawnPointIndex = Random.Range(1, coreSpawnPoints.Length);
                    FindObjectOfType<Core>().transform.position = coreSpawnPoints[randomSpawnPointIndex].transform.position;
                }
                FindObjectOfType<Core>().StartFalling();
                if (blackHole != null)
                {
                    blackHole.SetActive(true);
                }
                coreStartFalling = true;
            }
        }
    }

    private void UpdatePlayerRemainingDistance()
    {
        if(remainingDistanceText == null) { return; }
        coreRemainingDistance -= Time.deltaTime;
        if (Mathf.Floor(coreRemainingDistance) >= 0)
        {
            remainingDistanceText.text = Mathf.Floor(coreRemainingDistance).ToString() + " km";
        }
    }

    public void GameSpeedUp()
    {
        Time.timeScale = speedUpFactor;
    }

    public void GameSpeedNormal()
    {
        Time.timeScale = normalSpeedFactor;
    }

    public void GameOver()
    {
        gameOverCanvas.enabled = true;
        gameState = GameState.GameOver;
        Time.timeScale = 0;
    }

    public void Win()
    {
        winCanvas.enabled = true;
        player.GetComponent<Player>().enabled = false;
        player.GetComponent<CollisionHandler>().enabled = false;
        gameState = GameState.Win;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Stage");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
