using System.Collections;
using UnityEngine;

public class ObstaculeController : MonoBehaviour
{
    public int damageAmount = 1; // Cantidad de da�o que causa al jugador
    public string playerTag = "Player"; // Etiqueta del jugador
    private bool playerDetected = false; // Variable para almacenar si el jugador ha sido detectado
    private Vector2 initialPosition; // Posici�n inicial del obst�culo
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
        // Si colisiona con el jugador, aplica da�o y resetea el obst�culo
        if (collision.gameObject.CompareTag(playerTag))
        {
            AttackController.THIS.TakeDamage(damageAmount); // Aplica da�o al jugador
            StartCoroutine(ResetObstacle()); // Resetea el obst�culo despu�s de causar da�o
        }
        // Si colisiona con el suelo, resetea el obst�culo
        else if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(ResetObstacle()); // Resetea el obst�culo si colisiona con el suelo
        }
    }

    IEnumerator ResetObstacle()
    {
        rb.velocity = Vector2.zero; // Detiene el movimiento del obst�culo
        rb.gravityScale = 0f; // Desactiva la gravedad
        gameObject.SetActive(false); // Desactiva el obst�culo
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        transform.position = initialPosition; // Devuelve el obst�culo a su posici�n inicial
        gameObject.SetActive(true); // Activa el obst�culo nuevamente
        playerDetected = false; // Reinicia la detecci�n del jugador
    }
}
