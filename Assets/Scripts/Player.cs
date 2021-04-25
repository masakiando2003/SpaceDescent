using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float slowDownFactor = 0.75f;
    [SerializeField] float slowDownTime = 3f;
    [SerializeField] SpriteRenderer playerSprite;

    float xMin, xMax, yMin, yMax;

    bool isAlive, isSlowDownState;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        isAlive = true;
        isSlowDownState = false;
        SetUpMoveBoundries();
    }

    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) { return; }

        HandlePlayerMovementInput();
    }

    private void HandlePlayerMovementInput()
    {
        float movementFactor = 1f;
        if(isSlowDownState) { movementFactor = slowDownFactor; }
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed * movementFactor;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed * movementFactor;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    public void SlowDownSpeed()
    {
        isSlowDownState = true;
    }

    public void ResetToNormalSpeed()
    {
        isSlowDownState = false;
    }

    public void Die()
    {
        isAlive = false;
        if(playerSprite != null)
        {
            playerSprite.enabled = false;
        }
    }

    public float GetSlowDownTimer()
    {
        return slowDownTime;
    }
}
