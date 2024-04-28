using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public static RespawnSystem THIS;

    private Vector2 lastSpawnPoint;

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
        }
    }

    private void Update()
    {
        Debug.Log(lastSpawnPoint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            Debug.Log("Checkpoint Actualizado!");

            lastSpawnPoint = transform.position;
        }
    }

    public Vector2 GetLastSpawnPoint()
    {
        return lastSpawnPoint;
    }

}
