using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public static GameController THIS;

    [Header("Settings")]
    [Range(0f, 2f)][SerializeField] private float timeScale;

    [Header("Image Components.")]
    [SerializeField] private Image mapa;
    [SerializeField] private Image playerIcon;

    private void Awake()
    {
        THIS = this;
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
}
