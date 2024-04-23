using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorObjeto : MonoBehaviour
{
    public ObstaculeController obstaculeController; // Referencia al script del objeto que cae

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto que entra es el jugador, activa el comportamiento del objeto que cae
        if (other.gameObject.CompareTag("PlayerCollider"))
        {           
            obstaculeController.ActivateObstacle();
        }
    }
}