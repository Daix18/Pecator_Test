using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public static GameController THIS;

    [Header("Settings")]
    [Range(0f, 2f)][SerializeField] private float timeScale;

    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales;

    private bool pesteLoaded;

    // Image Components para los mapas
    public Image mapaSimple;
    public Image mapaComplejo;

    private void OnEnable()
    {
        // Suscribirse al evento SceneManager.sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desuscribirse del evento SceneManager.sceneLoaded para evitar fugas de memoria
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        THIS = this;
    }
    public int cantidadMonedas = 0;

    private void Start()
    {
        // Establece la cantidad inicial de monedas a 0 al inicio del juego
        cantidadMonedas = 0;
    }

    public int GetCantidadMonedas()
    {
        return cantidadMonedas;
    }

    public void SumarPuntos(int puntos)
    {
        cantidadMonedas += puntos;
    }

    void Update()
    {
        // Verificar si se presiona la tecla M
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Si el mapa simple está activo, se desactiva; si está desactivado, se activa
            mapaSimple.enabled = !mapaSimple.enabled;
            mapaComplejo.enabled = false; // Desactivar el mapa complejo al mostrar el simple

        }
    }

    public void SumarPuntosOtros(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log(puntosTotales);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Aquí puedes realizar acciones basadas en la escena cargada
        if (scene.name == "Boss_Peste")
        {
            if (!pesteLoaded)
            {
                StartCoroutine(LoadPesteScene());
            }
        }
        // Agrega más condiciones según las escenas que tengas y las acciones que desees realizar
    }

    //Corrutina para cargar la escena del boss de la peste y desactivar ciertas cosas.
    IEnumerator LoadPesteScene()
    {
        yield return new WaitForSeconds(0.3f);
        pesteLoaded = true;
    }
}