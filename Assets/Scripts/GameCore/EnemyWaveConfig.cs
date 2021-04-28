using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class EnemyWaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] pathPrefabs;
    [SerializeField] GameObject projectTilePrefab;
    [SerializeField] int maxSpawnNum;
    [SerializeField] float startWaveDistance;
    [SerializeField] float endWaveDistance;
    [SerializeField] float timeBetweenSpawns = 5f;
    [SerializeField] float minEnemyMoveSpeed = 1f;
    [SerializeField] float maxEnemyMoveSpeed = 1f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public GameObject GetProjectTilePrefab() { return projectTilePrefab; }
    public int GetPathPrefabsCount() { return pathPrefabs.Length; }

    public List<Transform> GetWaypoints(int pathIndex) 
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefabs[pathIndex].transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    public int GetMaxSpawnNum()
    {
        return maxSpawnNum;
    }
    public float GetStartWaveDistance() { return startWaveDistance; }
    public float GetEndWaveDistance() { return endWaveDistance; }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetMinEnemyMovementSpeed() { return minEnemyMoveSpeed; }
    public float GetMaxEnemyMovementSpeed() { return maxEnemyMoveSpeed; }
}
