using System;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool isDetected= Physics2D.Raycast(transform.position, Vector2.down, 10f, LayerMask.GetMask("Player"));

        if (isDetected)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsTouchingLayers(LayerMask.GetMask("Floor")))
        {
            Destroy(gameObject);
        }
    }
    
    
}
