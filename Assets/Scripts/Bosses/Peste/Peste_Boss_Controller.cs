using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Peste_Boss_Controller : MonoBehaviour
{
    public static Peste_Boss_Controller THIS;

    [Header("Fundamental Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    public Transform player;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Image fillImage;
    [SerializeField] private bool onGround;
    public bool facingRight = true;
    public GameObject carlos;
    public GameObject finishPoints;

    [Header("Vida")]
    [SerializeField] private float life;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDamage;
    public float attackRange = 3f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float cooldownTime = 1.5f;
    [SerializeField] private float horizontalJumpForce = 5f;
    [SerializeField] private bool cooldown = true;
    [SerializeField] private bool doubleJumped;

    [Header("Gas Ability")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float duration;

    [Header("Stun Settings")]
    [SerializeField] private bool stun;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("xVelocity", rb.velocity.x);
        animator.SetBool("Cooldown", cooldown);
        animator.SetBool("doubleJumped", doubleJumped);
        animator.SetBool("stunned", stun);

        onGround = Physics2D.OverlapBox(groundChecker.position, dimensionesCaja, 0f, queEsSuelo);

        if (onGround)
        {
            rb.mass = 1000f;
        }
        else
        {
            rb.mass = 1f;
        }

        if (life <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);

        //Reactivamos las puertas para poder salir de la escnea.
        carlos.SetActive(true);
        finishPoints.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        fillImage.fillAmount = life / 100f;
    }

    public void LookAtPlayer()
    {
        if (transform.position.x > player.position.x && facingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            facingRight = !facingRight;
        }
        else if (transform.position.x < player.position.x && !facingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            facingRight = !facingRight;
        }

        // Invertir la dirección de movimiento
        rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
    }

    public void JumpAndMoveForward()
    {
        // Calcular la dirección hacia la que se moverá el jefe (hacia el jugador)
        float moveDirection = player.position.x > transform.position.x ? 1f : -1f;

        // Aplicar una fuerza al jefe para realizar el salto
        rb.velocity = new Vector2(moveDirection * horizontalJumpForce, jumpForce);
    }

    public void SecondJump()
    {
        // Calcular la dirección hacia la que se moverá el jefe (hacia el jugador)
        float moveDirection = player.position.x > transform.position.x ? 1f : -1f;

        // Aplicar una fuerza al jefe para realizar el salto
        rb.velocity = new Vector2(moveDirection * horizontalJumpForce, jumpForce);
    }

    public void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(attackController.position, attackRadius);

        foreach (Collider2D collision in objects)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<AttackController>().TakeDamage(attackDamage);
            }
        }
    }

    public void Down()
    {
        // Detener el movimiento horizontal del jefe
        rb.velocity = Vector2.zero;

        rb.gravityScale = 100f;

        // Aplicar una fuerza hacia abajo para una caída rápida
        rb.AddForce(Vector2.down * jumpForce * fallMultiplier, ForceMode2D.Impulse);

        doubleJumped = true;
    }

    public void Stun(int number)
    {
        // Detener el movimiento horizontal del jefe
        rb.velocity = new Vector2(0f, rb.velocity.y);

        if (number == 1)
        {
            stun = true;
        }
        if (number == 0)
        {
            stun = false;
        }

        doubleJumped = false;
        StartCoroutine(CooldownChange());
    }

    IEnumerator CooldownChange()
    {
        cooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackController.position, attackRadius);
        Gizmos.DrawWireCube(groundChecker.position, dimensionesCaja);
    }
}