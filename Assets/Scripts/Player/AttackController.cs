using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private Image fillImage;

    [Header("Attack Settings")]
    [SerializeField] private float health;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    private Animator animator;
    [SerializeField] private float initialHealth = 100f;    

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = initialHealth;
    }

    private void Update()
    {
        Debug.Log(health);
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;

        }

        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0)
        {
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        fillImage.fillAmount = health / 100f;


        if (health <= 0)
        {
            health = 0; // Asegurarse de que la vida no sea negativa
            RespawnPlayer();
        }
    }
    private void RespawnPlayer()
    {
        RespawnSystem respawnSystem = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnSystem>();
        if (respawnSystem != null)
        {
            transform.position = respawnSystem.GetLastSpawnPoint();
            health = initialHealth;
            fillImage.fillAmount = 1f;
        }
    }
    public float GetCurrentHealth()
    {
        return health;
    }
    private void Golpe()
    {
        //animator.SetTrigger("Golpe");

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            }
        }
    }
    public void ResetHealth()
    {
        health = initialHealth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
