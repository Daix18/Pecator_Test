using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GuerraBossController : MonoBehaviour
{    
    public static GuerraBossController THIS;

    [Header("Fundamental Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool facingRight = true;
    public Transform player;
    public float attackRange = 3f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform leftAttackPosition;
    [SerializeField] private Transform rightAttackPosition;
    [SerializeField] private Image fillImage;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private bool onGround;

    [Header("Vida")]
    [SerializeField] private float life;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDamage;
    [SerializeField] private List<SpecialAttack> arrowRains = new List<SpecialAttack>();

    [Header("Arrow Rain Settings")]
    public Transform fatherArrowRain;
    [SerializeField] private float cooldownDuration = 4f;
    public float minXLimit = -5f;
    public float maxXLimit = 5f;
    [SerializeField] private bool cooldown = false;

    [Header("Stun settings")]
    [SerializeField] private float stunDuration = 2f;
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
        float distancePlayer = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("playerDistance", distancePlayer);
        animator.SetFloat("xVelocity", rb.velocity.x);
        animator.SetBool("Cooldown", cooldown);
        animator.SetBool("Stunned", stun);

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

    public void TakeDamage(float damage)
    {
        life -= damage;
        fillImage.fillAmount = life / 100f;
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

    public void ArrowRain()
    {
        rb.velocity = Vector2.zero;

        // Llamar a ArrowRain en todas las instancias de SpecialAttack almacenadas en la lista
        foreach (SpecialAttack specialAttack in arrowRains)
        {
            StartCoroutine(specialAttack.ArrowRain());
        }
    }

    void MoveBeforeAttack()
    {
        float distanceToLeftPosition = Vector2.Distance(transform.position, leftAttackPosition.position);
        float distanceToRightPosition = Vector2.Distance(transform.position, rightAttackPosition.position);

        Transform targetPosition;

        // Seleccionar la posición objetivo más cercana
        if (distanceToLeftPosition < distanceToRightPosition)
        {
            targetPosition = leftAttackPosition;
        }
        else if (distanceToLeftPosition > distanceToRightPosition)
        {
            targetPosition = rightAttackPosition;
        }
        else
        {
            // Si las distancias son iguales, lanzar un dado (random) para decidir qué posición tomar
            if (Random.Range(0, 2) == 0)
            {
                targetPosition = leftAttackPosition;
            }
            else
            {
                targetPosition = rightAttackPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackController.position, attackRadius);
        Gizmos.DrawWireCube(groundChecker.position, dimensionesCaja);
    }

    public IEnumerator Stun()
    {
        Debug.Log("Stunned");
        rb.velocity = Vector2.zero;
        stun = true;
        cooldown = true;
        yield return new WaitForSeconds(stunDuration);
        stun = false;        
        yield return new WaitForSeconds(cooldownDuration);
        cooldown = false;
    }

}
