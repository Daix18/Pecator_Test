using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public int damageAmount = 100; // Cantidad de da�o que causa al jugador
    public LayerMask playerLayer; // Capa que identifica al jugador
    private Vector2 initialPosition; // Posici�n inicial del obst�culo
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void Update()
    {
        // Lanza un rayo hacia abajo desde la posici�n del obst�culo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer);

        // Si el rayo golpea al jugador, activa la ca�da del obst�culo
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            rb.gravityScale = 1f; // Activa la gravedad para que el obst�culo caiga
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (playerHealth != null)
            {
                AttackController.THIS.TakeDamage(damageAmount) // Aplica da�o al jugador
            }
            StartCoroutine(ResetObstacle()); // Resetea el obst�culo despu�s de causar da�o
        }
    }

    IEnumerator ResetObstacle()
    {
        rb.velocity = Vector2.zero; // Detiene cualquier movimiento del obst�culo
        rb.gravityScale = 0f; // Desactiva la gravedad
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); // Cambia la transparencia para un efecto de "fade"
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        transform.position = initialPosition; // Devuelve el obst�culo a su posici�n inicial
        GetComponent<SpriteRenderer>().color = Color.white; // Restaura la transparencia
    }
}
