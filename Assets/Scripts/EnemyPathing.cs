using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] EnemyWaveConfig ufoWaveConfig;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpped = 2f;

    int wayPointIndex;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        waypoints = ufoWaveConfig.GetWaypoints();
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
        if (wayPointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[wayPointIndex].transform.position;
            var movementThisFrame = moveSpped * Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                if (gameObject.tag == "Enemy" &&
                    waypoints[wayPointIndex].gameObject.tag == "ShootingPosition")
                {
                    if(ufoWaveConfig == null) { return; }
                    int direction = ufoWaveConfig.GetDirection();
                    float configuredProjectTileSpeed = ufoWaveConfig.GetEnemyProjectTieSpeed();
                    GameObject configuredProjectTile = ufoWaveConfig.GetProjectTilePrefab();
                    gameObject.GetComponent<UFO>().Fire(configuredProjectTile, direction, configuredProjectTileSpeed);
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
