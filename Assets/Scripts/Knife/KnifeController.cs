using System.Collections;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    private Transform player;
    public Transform teleportPosition;
    private Rigidbody2D rb;
    private bool teleported = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Busca al jugador en la escena y obtiene su transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diana") && !teleported)
        {
            // Si colisiona con una "Diana" y no se ha teletransportado antes, detener el cuchillo y teletransportar al jugador
            StartCoroutine(TeleportPlayer());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo")) 
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

            Destroy(gameObject);
        }
    }

    IEnumerator TeleportPlayer()
    {
        // Detener el movimiento del cuchillo
        rb.velocity = Vector2.zero;
        // Desactivar el Rigidbody del cuchillo
        rb.simulated = false;

        // Esperar un breve momento antes de teletransportar al jugador
        yield return new WaitForSeconds(0.01f);

        // Teletransportar al jugador a la posición del cuchillo
        player.position = teleportPosition.position;

        //Indicamos que se ha teletransportado el jugador
        teleported = true;

        //Reactivamos el Rigidbody
        rb.simulated = true;

        teleported = false;

        Destroy(gameObject);
    }
}
