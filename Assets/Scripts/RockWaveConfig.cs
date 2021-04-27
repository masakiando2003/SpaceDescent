using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rock Wave Config")]
public class RockWaveConfig : MonoBehaviour
{

    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject[] availableSpawnAreas;
    [SerializeField] int maxSpawnNum;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;


    public GameObject GetRockPrefab() { return rockPrefab; }

    public List<Transform> GetRockSpawnPoints()
    {
        var rockSpawnAreaPoints = new List<Transform>();
        foreach(GameObject availableSpawnArea in availableSpawnAreas)
        {
            rockSpawnAreaPoints.Add(availableSpawnArea.transform);
        }
        return rockSpawnAreaPoints;
    }

    public int GetMaxSpawnNum() { return maxSpawnNum; }

    public float GetMinSpeed() { return minSpeed; }

    public float GetMaxSpeed() { return maxSpeed; }

}
