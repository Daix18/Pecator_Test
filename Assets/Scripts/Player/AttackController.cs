using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    public static AttackController THIS;

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
    public bool attacking;
    [SerializeField] private float initialHealth = 100f;    

    private void Start()
    {
        animator = GetComponent<Animator>();        
        health = initialHealth;
    }

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
        }
    }

    private void Update()
    {        
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
    public void Golpe()
    {
        // Comprobar si el golpe ya está activo, si no, activarlo
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Golpe");
        }


        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            }
        }
    }

    // Método llamado desde un evento del Animator al finalizar el golpe
    public void FinalizarGolpe()
    {
        attacking = false;
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
