using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameController gameController;
    public TextMeshProUGUI puntos;

    // Update is called once per frame
    void Update()
    {
        puntos.text = gameController.PuntosTotales.ToString();
    }
}