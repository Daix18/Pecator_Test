using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUnlocked : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCollider"))
        {
            MovimientoJugador.THIS.doubleJumpUnlocked = true;

            Destroy(gameObject);

            DialogueController.GetInstance().EnterDialogueMode(inkJson);
        }
    }
}
