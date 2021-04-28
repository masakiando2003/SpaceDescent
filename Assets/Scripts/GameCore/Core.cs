using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] float fallDownFactor = 0.01f;

    Rigidbody2D rb;
    CircleCollider2D cc;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
        StopFalling();
    }

    private void Initialization()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = fallDownFactor;
        cc = GetComponent<CircleCollider2D>();
    }

    public void StopFalling()
    {
        if (rb == null) { return; }
        rb.simulated = false;
    }

    public void DisableCollider()
    {
        cc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachToPlayer()
    {
        StopFalling();
        DisableCollider();
        Player player = FindObjectOfType<Player>();
        transform.SetParent(player.transform);
    }

    public void StartFalling()
    {
        if (rb == null) { return; }
        rb.simulated = true;
    }
}
