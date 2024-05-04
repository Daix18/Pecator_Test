using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hambre_Boss_Controller : MonoBehaviour
{
    public static Hambre_Boss_Controller THIS;

    [Header("Fundamental Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool facingRight = true;
    bool hasCalledMoveStopWalls = false;
    public Transform player;
    public GameObject carlos;
    public GameObject finishPoints;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform wallChecker;
    [SerializeField] private Transform stopwallRight;
    [SerializeField] private Transform stopwallLeft;
    [SerializeField] private Image fillImage;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private Vector3 wallBoxDimensions;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private LayerMask stopWall;
    [SerializeField] private bool onGround;
    [SerializeField] private bool onWall;
    private Vector2 direccion;

    [Header("Vida")]
    [SerializeField] private float life;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDamage;

    [Header("Dash Settings")]
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float cooldownDuration = 4f;
    [SerializeField] private float throwMagnitude = 10f;
    public float attackRange = 3f;
    [SerializeField] private bool cooldown = false;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool canDash;
    private Vector2 dashingDir;

    [Header("Stun settings")]
    [SerializeField] private int wallHitCount = 0;
    [SerializeField] private int maxWallHits = 3;
    [SerializeField] private float stunDuration = 2f;
    [SerializeField] private bool stun;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Cooldown", cooldown);
        animator.SetBool("Stunned", stun);
        animator.SetBool("Dashing", isDashing);

        onGround = Physics2D.OverlapBox(groundChecker.position, dimensionesCaja, 0f, queEsSuelo);
        onWall = Physics2D.OverlapBox(wallChecker.position, wallBoxDimensions, 0f, stopWall);

        if (onGround)
        {
            rb.mass = 100f;
        }
        else
        {
            rb.mass = 1f;
        }

        if (onWall && wallHitCount <= maxWallHits && !stun && !cooldown)
        {
            rb.velocity *= 0.5f;

            rb.velocity = Vector2.zero;

            wallHitCount++;

            StartCoroutine(Sequence());
        }

        if (wallHitCount >= maxWallHits)
        {
            StartCoroutine(Stun());
        }

        if (wallHitCount == maxWallHits - 1 && !hasCalledMoveStopWalls)
        {
            StartCoroutine(MoveStopWalls());
            hasCalledMoveStopWalls = true;
        }

        if (!stun)
        {
            canDash = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            canDash = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (life <= 0)
        {
            Death();
        }

        if (GameController.THIS.hambreBossKilled)
        {
            Destroy(gameObject);
            GameController.THIS.bossCanvas.SetActive(false);
        }
    }

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
        }
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

    private void Death()
    {
        Destroy(gameObject);

        carlos.SetActive(true);
        finishPoints.SetActive(true);

        GameController.THIS.hambreBossKilled = true;
        GameController.THIS.bossCanvas.SetActive(false);
    }

    public void Dash()
    {
        if (canDash)
        {
            dashingDir = new Vector2(direccion.x, direccion.y);
            isDashing = true;

            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }

            if (isDashing)
            {
                // Establecer la velocidad basada en la escala local x del objeto y la potencia de dash
                rb.velocity = dashingDir.normalized * dashingPower;
            }
        }
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

    public void TakeDamage(float damage)
    {
        life -= damage;
        fillImage.fillAmount = life / 100f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackController.position, attackRadius);
        Gizmos.DrawWireCube(groundChecker.position, dimensionesCaja);
        Gizmos.DrawWireCube(wallChecker.position, wallBoxDimensions);
    }

    //En esta función, hacemos la comprobación de que si hemos chocado contra el player, lo mandamos hacia arriba.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Verifica que se encontró el Rigidbody
            if (playerRb != null)
            {
                // Aplica una fuerza hacia arriba al Rigidbody del jugador
                playerRb.AddForce(Vector2.up * throwMagnitude, ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator Sequence()
    {
        LookAtPlayer();
        yield return new WaitForSeconds(waitTime);
        Dash();
    }

    IEnumerator Stun()
    {
        Debug.Log("Stunned");
        rb.velocity = Vector2.zero;
        stun = true;
        cooldown = true;
        yield return new WaitForSeconds(stunDuration);
        stun = false;
        wallHitCount = 0;
        yield return new WaitForSeconds(cooldownDuration);
        cooldown = false;
    }

    IEnumerator MoveStopWalls()
    {
        yield return new WaitForSeconds(1.3f);

        // Guardamos las posiciones originales antes de mover los objetos
        Vector2 originalLeftPosition = stopwallLeft.position;
        Vector2 originalRightPosition = stopwallRight.position;

        stopwallLeft.position = new Vector2(0f, stopwallLeft.position.y);

        stopwallRight.position = new Vector2(23f, stopwallRight.position.y);

        // Espera un momento para mantener las posiciones en sus nuevos lugares
        yield return new WaitForSeconds(1f);

        // Restauramos las posiciones originales de stopwallLeft y stopwallRight
        stopwallLeft.position = originalLeftPosition;
        stopwallRight.position = originalRightPosition;

        hasCalledMoveStopWalls = false; // Restablece la bandera para permitir futuras llamadas        
    }
}
