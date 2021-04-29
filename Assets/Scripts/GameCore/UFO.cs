using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField] GameObject projectTile;
    [SerializeField] Transform[] projectTileSpawnTransform;
    [SerializeField] float[] rotationAngles;
    [SerializeField] Vector2[] projectTileVelocity;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweeenShots = 1f;
    [SerializeField] float projectTileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweeenShots);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire(GameObject configuredProfectTile)
    {
        if(configuredProfectTile == null || projectTileSpawnTransform.Length <= 0) { return; }
        for(int i = 0; i < projectTileSpawnTransform.Length; i++)
        {
            float rotationAngle = rotationAngles[i];
         
            GameObject laser = Instantiate(
                projectTile,
                projectTileSpawnTransform[i].position,
                Quaternion.Euler(0f, 0f, rotationAngle)) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectTileVelocity[i].x, projectTileVelocity[i].y);
            if (GameObject.Find("Obstacles") != null)
            {
                laser.transform.SetParent(GameObject.Find("Obstacles").transform);
            }
        }
    }
}
