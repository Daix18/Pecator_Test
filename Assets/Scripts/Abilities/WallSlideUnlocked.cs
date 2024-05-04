using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideUnlocked : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovimientoJugador.THIS.wallSlideUnlocked = true;
        
        Destroy(gameObject);

        DialogueController.GetInstance().EnterDialogueMode(inkJson);
    }
}
