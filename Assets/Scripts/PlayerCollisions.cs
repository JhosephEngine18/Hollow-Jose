using System;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public static event Action<bool> Colliding;
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if(collision.tag == "Cave")
        {
            Colliding(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cave")
        {
            Colliding(false);
        }
    }
}
