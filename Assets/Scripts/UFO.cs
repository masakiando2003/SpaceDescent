using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField] GameObject projectTile;
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

    public void Fire(GameObject configuredProfectTile,  int direction, float configuredProjectTileSpeed)
    {
        if(configuredProfectTile == null) { return; }
        float rotationAngle = (direction == 1) ? 180f : 0f; 
        GameObject laser = Instantiate(
            projectTile,
            transform.position,
            Quaternion.Euler(0f, 0f, rotationAngle)) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, configuredProjectTileSpeed);
    }
}
