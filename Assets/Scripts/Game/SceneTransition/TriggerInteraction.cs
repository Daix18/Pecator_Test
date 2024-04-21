using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public GameObject Player { get; set; }
    public bool CanInteract { get; set; }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("PlayerCollider");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player && CanInteract)
        {
            Interact();
            CanInteract = false; // Desactiva la interacción después de la primera vez
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CanInteract = true;
    }

    public virtual void Interact()
    {
        // Aquí puedes implementar la lógica específica de la interacción
    }
}
