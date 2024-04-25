using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionObjeto : MonoBehaviour
{
    public TextHistoria textHistoria; // Referencia al script TextHistoria

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            // Llama al método MostrarOcultarHistoria del script TextHistoria
            textHistoria.MostrarOcultarHistoria();
        }
    }
}