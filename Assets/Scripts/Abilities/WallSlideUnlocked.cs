using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideUnlocked : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovimientoJugador.THIS.wallSlideUnlocked = true;
        
        Destroy(gameObject);
    }
}
