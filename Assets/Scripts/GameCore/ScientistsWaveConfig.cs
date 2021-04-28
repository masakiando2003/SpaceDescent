using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scientists Wave Config")]
public class ScientistsWaveConfig : ScriptableObject
{
    [SerializeField] GameObject scientistsPrefab;
    [SerializeField] GameObject[] availableSpawnAreas;
    [SerializeField] int maxSpawnNum;
    [SerializeField] float startWaveDistance;
    [SerializeField] float endWaveDistance;
    [SerializeField] float minFallDownFactor;
    [SerializeField] float maxFallDownFactor;

    public GameObject GetScientistPrefab() { return scientistsPrefab; }

    public List<Transform> GetScientistSpawnPoints()
    {
        var scientistSpawnAreaPoints = new List<Transform>();
        foreach (GameObject availableSpawnArea in availableSpawnAreas)
        {
            scientistSpawnAreaPoints.Add(availableSpawnArea.transform);
        }
        return scientistSpawnAreaPoints;
    }

    public int GetMaxSpawnNum() { return maxSpawnNum; }

    public float GetEndWaveDistance() { return endWaveDistance; }

    public float GetMinFallDownFactor() { return minFallDownFactor; }
    public float GetMaxFallDownFactor() { return maxFallDownFactor; }
}
