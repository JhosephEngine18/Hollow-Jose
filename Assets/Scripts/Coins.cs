using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private Animator animator;
    private Collider2D _collider;
    void Start()
    {
        animator =  GetComponent<Animator>();
        animator.Play("CoinAnimation");
        _collider = GetComponent<Collider2D>();
    }
    

    void OnCoinCollected()
    {
        animator.Play("CoinCollected");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCoinCollected();
            Destroy(gameObject, 0.5f);
            _collider.enabled = false;
        }
    }
}
