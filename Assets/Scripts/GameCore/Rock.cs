using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] float movementSpeed = 0.5f;
    [SerializeField] float rotationsPerMinute = 10.0f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateContinuously();
    }

    private void Move()
    {
        rb.velocity = new Vector2(0, movementSpeed);
    }

    private void RotateContinuously()
    {
        transform.Rotate(0f, 0f, 6.0f * rotationsPerMinute * Time.deltaTime);
    }

    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }
}
