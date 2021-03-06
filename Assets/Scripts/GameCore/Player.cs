using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float normalFactor = 1f;
    [SerializeField] float boostFactor = 1.5f;
    [SerializeField] float slowDownFactor = 0.5f;
    [SerializeField] float slowDownTime = 3f;
    [SerializeField] SpriteRenderer playerSprite;

    float xMin, xMax, yMin, yMax;
    float padding = 0.5f;

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
        if (Input.GetAxis("Vertical") < 0)
        {
            movementFactor = boostFactor;
            FindObjectOfType<GameManager>().GameSpeedUp();
           FindObjectOfType<BackgroundScroller>().SetStage2ToTrue();
        }
        else
        {
            movementFactor = normalFactor;
            FindObjectOfType<GameManager>().GameSpeedNormal();
            FindObjectOfType<BackgroundScroller>().SetStage2ToFalse();
        }
        if (isSlowDownState) { movementFactor = slowDownFactor; }
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed * movementFactor;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed * movementFactor;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
   //     var LerpMaxYPos = Mathf.Lerp(yMax+bottompadding, yMax, 0.01f);
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
        if(GetComponent<BoxCollider2D>() != null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void Win()
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public float GetSlowDownTimer()
    {
        return slowDownTime;
    }
}
