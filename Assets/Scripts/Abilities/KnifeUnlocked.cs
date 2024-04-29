using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeUnlocked : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCollider"))
        {
            MovimientoJugador.THIS.knifeUnlocked = true;

            Destroy(gameObject);
        }
    }
}
