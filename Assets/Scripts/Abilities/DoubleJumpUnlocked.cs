using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUnlocked : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCollider"))
        {
            MovimientoJugador.THIS.doubleJumpUnlocked = true;

            Destroy(gameObject);
        }
    }
}
