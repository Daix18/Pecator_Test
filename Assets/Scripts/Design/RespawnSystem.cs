using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    private Vector2 lastSpawnPoint; // Variable para almacenar la posici�n del �ltimo "spawn point"

    private void Start()
    {
        lastSpawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Actualizar la posici�n del �ltimo "spawn point" al pasar por el spawn point
            lastSpawnPoint = transform.position;
        }
    }

    public Vector2 GetLastSpawnPoint()
    {
        return lastSpawnPoint;
    }
}
