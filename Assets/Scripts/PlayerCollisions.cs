using System;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public static event Action<bool> Colliding;

    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if(collision.gameObject.CompareTag(GameReferences.Tags.Cave))
        {
            if (Colliding != null) Colliding(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameReferences.Tags.Cave))
        {
            if (Colliding != null) Colliding(false);
        }
    }


}
