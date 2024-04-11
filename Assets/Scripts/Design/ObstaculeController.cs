using System.Collections;
using UnityEngine;

public class ObstaculeController : MonoBehaviour
{
    public int damageAmount = 1; // Cantidad de daño que causa al jugador
    public string playerTag = "Player"; // Etiqueta del jugador
    private bool playerDetected = false; // Variable para almacenar si el jugador ha sido detectado
    private Vector2 initialPosition; // Posición inicial del obstáculo
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        rb.gravityScale = 0f; // Desactiva la gravedad inicialmente
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto es el jugador, activa la gravedad del objeto
        if (other.CompareTag(playerTag))
        {
            playerDetected = true;
            rb.gravityScale = 1f; // Activa la gravedad del objeto
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con el jugador, aplica daño y resetea el obstáculo
        if (collision.gameObject.CompareTag(playerTag))
        {
            AttackController.THIS.TakeDamage(damageAmount); // Aplica daño al jugador
            StartCoroutine(ResetObstacle()); // Resetea el obstáculo después de causar daño
        }
        // Si colisiona con el suelo, resetea el obstáculo
        else if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(ResetObstacle()); // Resetea el obstáculo si colisiona con el suelo
        }
    }

    IEnumerator ResetObstacle()
    {
        rb.velocity = Vector2.zero; // Detiene el movimiento del obstáculo
        rb.gravityScale = 0f; // Desactiva la gravedad
        gameObject.SetActive(false); // Desactiva el obstáculo
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        transform.position = initialPosition; // Devuelve el obstáculo a su posición inicial
        gameObject.SetActive(true); // Activa el obstáculo nuevamente
        playerDetected = false; // Reinicia la detección del jugador
    }
}
