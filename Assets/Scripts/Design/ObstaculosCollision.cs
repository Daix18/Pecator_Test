using UnityEngine;

public class ObstaculosCollision : MonoBehaviour
{
    // Cantidad de daño que recibe el jugador al colisionar
    private const float damageAmount = 200f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            AttackController.THIS.TakeDamage(damageAmount);
        }
    }
}