using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public int damageAmount = 100; // Cantidad de daño que causa al jugador
    public LayerMask playerLayer; // Capa que identifica al jugador
    private Vector2 initialPosition; // Posición inicial del obstáculo
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void Update()
    {
        // Lanza un rayo hacia abajo desde la posición del obstáculo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer);

        // Si el rayo golpea al jugador, activa la caída del obstáculo
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            rb.gravityScale = 1f; // Activa la gravedad para que el obstáculo caiga
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (playerHealth != null)
            {
                AttackController.THIS.TakeDamage(damageAmount) // Aplica daño al jugador
            }
            StartCoroutine(ResetObstacle()); // Resetea el obstáculo después de causar daño
        }
    }

    IEnumerator ResetObstacle()
    {
        rb.velocity = Vector2.zero; // Detiene cualquier movimiento del obstáculo
        rb.gravityScale = 0f; // Desactiva la gravedad
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); // Cambia la transparencia para un efecto de "fade"
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        transform.position = initialPosition; // Devuelve el obstáculo a su posición inicial
        GetComponent<SpriteRenderer>().color = Color.white; // Restaura la transparencia
    }
}
