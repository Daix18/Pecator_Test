using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public static GameController THIS;

    [Header("Settings")]
    [Range(0f, 2f)][SerializeField] private float timeScale;

    [Header("Image Components.")]
    public Image mapa;

    [Header("Boss Control")]
    public bool pesteBossKilled;
    public bool hambreBossKilled;
    public bool guerraBossKilled;

    public int PuntosTotales { get { return puntosTotales; } }
    public int cantidadMonedas = 0;
    private int puntosTotales;

    private bool pesteLoaded;
    private bool hambreLoaded;
    private bool guerraLoaded;

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

    private void Start()
    {
        // Establece la cantidad inicial de monedas a 0 al inicio del juego
        cantidadMonedas = 0;
    }

    // Método para obtener la cantidad de monedas
    public int GetCantidadMonedas()
    {
        return cantidadMonedas;
    }

    // Método para sumar puntos (en este caso, monedas)
    public void SumarPuntos(int puntos)
    {
        cantidadMonedas += puntos;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;

        if (Input.GetKeyDown(KeyCode.M))
        {
            //Se pausa el tiempo y se muestra el mapa.
            Time.timeScale = 0f;
            mapa.enabled = true;
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            //Se reanuda el tiempo y se desactiva el mapa.
            Time.timeScale = 1f;
            mapa.enabled = false;
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

        if (scene.name == "Boss_Hambre")
        {
            if (!hambreLoaded)
            {
                StartCoroutine(LoadHambreScene());
            }
        }

        if (scene.name == "Boss_Guerra")
        {
            if (!guerraLoaded)
            {
                StartCoroutine(LoadGuerraScene());
            }
        }
    }

    //Corrutinas:

    //Corrutina para cargar la escena del boss de la peste y desactivar ciertas cosas.
    IEnumerator LoadPesteScene()
    {
        yield return new WaitForSeconds(0.3f);
        Peste_Boss_Controller.THIS.carlos.SetActive(false);
        Peste_Boss_Controller.THIS.finishPoints.SetActive(false);
        pesteLoaded = true;
    }

    //Corrutina para cargar la escena del boss del hambre y desactivar ciertas cosas.
    IEnumerator LoadHambreScene()
    {
        yield return new WaitForSeconds(0.3f);
        Hambre_Boss_Controller.THIS.carlos.SetActive(false);
        Hambre_Boss_Controller.THIS.finishPoints.SetActive(false);
        hambreLoaded = true;
    }

    IEnumerator LoadGuerraScene()
    {
        yield return new WaitForSeconds(0.3f);
        GuerraBossController.THIS.carlos.SetActive(false);
        guerraLoaded = true;
    }
}
