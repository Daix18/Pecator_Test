using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHistoria : MonoBehaviour
{
    public Text historiaText; // Referencia al objeto de texto donde mostrar la historia
    public string historia; // La historia que deseas mostrar

    private bool mostrarHistoria = false;
    private bool dentroTrigger = false;

    void Update()
    {
        // Verificar si se presionó la tecla 'E' y si el jugador está dentro del trigger
        if (Input.GetKeyDown(KeyCode.E) && dentroTrigger)
        {
            mostrarHistoria = !mostrarHistoria; // Alternar el estado de mostrar historia
            MostrarOcultarHistoria();
        }
    }

    // Método para mostrar u ocultar la historia en el objeto de texto UI
    void MostrarOcultarHistoria()
    {
        if (mostrarHistoria)
        {
            historiaText.text = historia; // Mostrar la historia
            historiaText.gameObject.SetActive(true); // Activar el objeto de texto
        }
        else
        {
            historiaText.gameObject.SetActive(false); // Ocultar el objeto de texto
        }
    }

    // Método para detectar cuando el jugador entra o sale del trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TriggerObjeto"))
        {
            dentroTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TriggerObjeto"))
        {
            dentroTrigger = false;
            mostrarHistoria = false; // Si sale del trigger, ocultar la historia
            MostrarOcultarHistoria();
        }
    }
}
