using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] EnemyWaveConfig ufoWaveConfig;
    [SerializeField] List<Transform> waypoints;

    int wayPointIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Initialization();
    }

    public void Initialization(int randomedPathIndex)
    {
        waypoints = ufoWaveConfig.GetWaypoints(randomedPathIndex);
        wayPointIndex = 0;
        transform.position = waypoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndShoot();
    }

    private void MoveAndShoot()
    {
        if(waypoints == null) { return; }
        if (wayPointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[wayPointIndex].transform.position;
            float minMoveSpeed = ufoWaveConfig.GetMinEnemyMovementSpeed();
            float maxMoveSpeed = ufoWaveConfig.GetMaxEnemyMovementSpeed();
            var movementThisFrame = Random.Range(minMoveSpeed, maxMoveSpeed) * Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                if (gameObject.tag == "Enemy" &&
                    waypoints[wayPointIndex].gameObject.tag == "ShootingPosition")
                {
                    if(ufoWaveConfig == null) { return; }
                    GameObject configuredProjectTile = ufoWaveConfig.GetProjectTilePrefab();
                    gameObject.GetComponent<UFO>().Fire(configuredProjectTile);
                }
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
