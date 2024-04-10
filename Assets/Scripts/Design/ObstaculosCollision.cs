using UnityEngine;

public class ObstaculosCollision : MonoBehaviour
{
    // Cantidad de daño que recibe el jugador al colisionar
    private const float damageAmount = 200f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<AttackController>().TakeDamage(damageAmount);
        }
    }
}