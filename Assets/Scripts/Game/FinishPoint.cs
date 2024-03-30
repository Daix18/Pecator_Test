using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    //
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.THIS.NextLevel(sceneName);
        }
    }
}
