using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rock Wave Config")]
public class RockWaveConfig : ScriptableObject
{

    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject[] availableSpawnAreas;
    [SerializeField] int maxSpawnNum;
    [SerializeField] float startWaveDistance;
    [SerializeField] float endWaveDistance;
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
    public float GetStartWaveDistance() { return startWaveDistance; }

    public float GetEndWaveDistance() { return endWaveDistance; }

    public float GetMinSpeed() { return minSpeed; }

    public float GetMaxSpeed() { return maxSpeed; }

}
