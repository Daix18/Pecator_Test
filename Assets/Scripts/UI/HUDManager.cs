using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI textoMonedas;

    private void Start()
    {
        // Actualiza el texto del HUD con la cantidad inicial de monedas al inicio del juego
        UpdateCoinsText();
    }

    private void Update()
    {
        // Actualiza el texto del HUD continuamente en cada fotograma
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        // Verifica si el texto de las monedas existe
        if (textoMonedas != null)
        {
            // Actualiza el texto del HUD con la cantidad actual de monedas del GameController
            textoMonedas.text = "Monedas: " + GameController.THIS.GetCantidadMonedas().ToString();
        }
    }
}
