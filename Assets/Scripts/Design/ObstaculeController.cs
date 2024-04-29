using System.Collections;
using UnityEngine;

public class ObstaculeController : MonoBehaviour
{
    public int damageAmount = 100;
    private bool playerDetected;
    private bool obstacleResetting; // Bandera para verificar si el obst�culo se est� reiniciando
    private Vector2 initialPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        rb.gravityScale = 0f;
    }

    public void ActivateObstacle()
    {
        if (!obstacleResetting) // Verifica si el obst�culo no se est� reiniciando actualmente
        {
            playerDetected = true;
            rb.gravityScale = 2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCollider") && !obstacleResetting) // Verifica si el obst�culo no se est� reiniciando actualmente
        {
            StartCoroutine(ResetObstacleAfterDelay());
            AttackController.THIS.TakeDamage(damageAmount);
            Debug.Log("oliwis");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
    }

    IEnumerator ResetObstacleAfterDelay()
    {
        obstacleResetting = true; // Marca que el obst�culo se est� reiniciando
        yield return new WaitForSeconds(6f);
        ResetObstacle();
        obstacleResetting = false; // Marca que el reinicio del obst�culo ha terminado
    }

    private void ResetObstacle()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        transform.position = initialPosition;
        gameObject.SetActive(true);
        playerDetected = false;
    }
}
