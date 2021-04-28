using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeScientist : MonoBehaviour
{
    [SerializeField] float fallDownFactor = 0.01f;
    [SerializeField] float disableColliderTime = 3f;

    Rigidbody2D rb;
    BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = fallDownFactor;
        bc = GetComponent<BoxCollider2D>();
    }

    public void StopMovement()
    {
        if (rb == null) { return; }
        rb.simulated = false;
    }
    public void StartMovement()
    {
        if (rb == null) { return; }
        rb.simulated = true;
    }

    public void AttachToPlayer()
    {
        Player player = FindObjectOfType<Player>();
        transform.SetParent(player.transform);
    }
    public void ReleaseAttach()
    {
        transform.SetParent(null);
        StartCoroutine(TemporaryDisableCollider());
    }

    private IEnumerator TemporaryDisableCollider()
    {
        bc.enabled = false;
        yield return new WaitForSeconds(disableColliderTime);
        bc.enabled = true;
    }

    public void SetFallDownFactor(float factor)
    {
        fallDownFactor = factor;
    }
}
