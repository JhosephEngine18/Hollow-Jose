using System;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private GameObject Coin;
    [SerializeField] private bool DropItem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            Destroy(gameObject, 0.1f);
        }
    }

    private void OnDestroy()
    {
        if (DropItem)
        {
            Instantiate(Coin, transform.position, Quaternion.identity);
        }
    }
}
