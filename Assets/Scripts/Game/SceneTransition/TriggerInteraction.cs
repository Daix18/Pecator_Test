using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public GameObject Player { get; set; }
    public bool CanInteract { get; set; }

    private float timerInteraction;
    private float interactionCooldown = 0.2f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("PlayerCollider");
        
        CanInteract = false;
    }

    void Update()
    {
        if (!CanInteract)
        {
            timerInteraction += Time.deltaTime;
            if (timerInteraction >= interactionCooldown)
            {
                CanInteract = true;
                timerInteraction = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player && CanInteract)
        {
            Interact();
            CanInteract = false;
        }

        if (timerInteraction > 0)
        {
            CanInteract = false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        CanInteract = true;
    }

    public virtual void Interact()
    {

    }
}
