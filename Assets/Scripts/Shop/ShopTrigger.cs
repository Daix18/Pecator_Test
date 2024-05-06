using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    public GameObject shopUI;
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby)
        {
            visualCue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                shopUI.SetActive(true); // Alternar la visibilidad de la tienda
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            isPlayerNearby = false;
            shopUI.SetActive(false); // Ocultar la tienda al salir del área de activación
        }
    }
}
