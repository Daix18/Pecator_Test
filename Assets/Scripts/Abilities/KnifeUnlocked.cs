using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeUnlocked : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCollider"))
        {
            MovimientoJugador.THIS.knifeUnlocked = true;

            gameObject.SetActive(false);

            DialogueController.GetInstance().EnterDialogueMode(inkJson);
        }
    }
}
