using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextHistoria : MonoBehaviour
{
    public TextMeshProUGUI[] textos; // Referencia a los objetos de texto donde mostrar la historia
    public GameObject[] cuadrosDialogo; // Referencia a los cuadros de diálogo
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

    // Método para mostrar u ocultar la historia en los objetos de texto y cuadros de diálogo
    public void MostrarOcultarHistoria()
    {
        foreach (TextMeshProUGUI texto in textos)
        {
            if (mostrarHistoria)
            {
                texto.text = historia; // Mostrar la historia
            }
            else
            {
                texto.text = ""; // Ocultar el texto
            }
        }

        foreach (GameObject cuadroDialogo in cuadrosDialogo)
        {
            cuadroDialogo.SetActive(mostrarHistoria); // Mostrar u ocultar el cuadro de diálogo
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