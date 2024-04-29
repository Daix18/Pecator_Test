using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUnlocked : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovimientoJugador.THIS.dashUnlocked = true;

        Destroy(gameObject);
    }
}
