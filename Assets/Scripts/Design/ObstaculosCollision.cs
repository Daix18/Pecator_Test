using UnityEngine;

public class ObstaculosCollision : MonoBehaviour
{
    // Cantidad de da�o que recibe el jugador al colisionar
    private const float damageAmount = 200f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            other.GetComponent<AttackController>().TakeDamage(damageAmount);
        }
    }
}