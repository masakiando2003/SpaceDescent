using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class EnemyWaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] GameObject projectTilePrefab;
    [SerializeField] [Range(-1,1)] int direction;
    [SerializeField] float playerRemainingDistanceToSpawn = 800f;
    [SerializeField] float timeBetweenSpawns = 5f;
    [SerializeField] float enemyMoveSpeed = 1f;
    [SerializeField] float enemyProjectTileSpeed = 10f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public GameObject GetProjectTilePrefab() { return projectTilePrefab; }

    public List<Transform> GetWaypoints() 
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints; 
    }

    public int GetDirection() { return direction; }

    public float GetPlayerRemainingDistanceToSpawn() { return playerRemainingDistanceToSpawn; }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetEnemyMovementSpeed() { return enemyMoveSpeed; }

    public float GetEnemyProjectTieSpeed() { return direction * enemyProjectTileSpeed; }
}
