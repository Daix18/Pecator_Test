using UnityEngine;
using UnityEngine.UI;

public class Mapas : MonoBehaviour
{
    public static Mapas Instance;

    [SerializeField] private Image mapImage1;
    [SerializeField] private Image mapImage2;

    private int currentMapID = -1; // -1 indica que no hay ning�n mapa mostr�ndose
    private bool playerInShop = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destruir el objeto si ya existe una instancia
        }
    }

    // M�todo para mostrar u ocultar el mapa seg�n el ID recibido
    public void ToggleMap(int mapID)
    {
        // Si el jugador est� en la tienda, no mostrar el mapa
        if (playerInShop)
        {
            return;
        }

        // Si el ID es igual al mapa actualmente mostrado, ocultarlo
        if (mapID == currentMapID)
        {
            HideMap();
        }
        else
        {
            // Comprobar si el mapa est� comprado y si es el prioritario
            if (mapID == 1 || (mapID == 2 && GameController.THIS.PuntosTotales >= 2))
            {
                ShowMap(mapID);
            }
        }
    }

    // M�todo para mostrar el mapa especificado
    private void ShowMap(int mapID)
    {
        Time.timeScale = 0f; // Pausar el juego mientras se muestra el mapa
        currentMapID = mapID;

        // Mostrar el mapa correspondiente seg�n el ID
        if (mapID == 1)
        {
            mapImage1.enabled = true;
            mapImage2.enabled = false;
        }
        else if (mapID == 2)
        {
            mapImage1.enabled = false;
            mapImage2.enabled = true;
        }
    }

    // M�todo para ocultar el mapa actualmente mostrado
    private void HideMap()
    {
        Time.timeScale = 1f; // Reanudar el juego cuando se oculta el mapa
        currentMapID = -1; // Restablecer el ID del mapa actual
        mapImage1.enabled = false;
        mapImage2.enabled = false;
    }

    // M�todo para indicar si el jugador est� en la tienda
    public void SetPlayerInShop(bool value)
    {
        playerInShop = value;
    }
}
