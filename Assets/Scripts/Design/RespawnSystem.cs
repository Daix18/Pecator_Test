using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public static RespawnSystem THIS;

    [HideInInspector] public Vector3 lastSpawnPoint;

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
}
