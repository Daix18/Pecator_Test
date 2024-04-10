using UnityEngine;

public class PersistController : MonoBehaviour
{
    public static PersistController THIS;

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
