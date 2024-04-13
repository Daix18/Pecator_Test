using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GuerraBossController : MonoBehaviour
{
    [Header("Fundamental Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool facingRight = true;
    public Transform player;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Image fillImage;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private bool onGround;
    private Vector2 direccion;

    [Header("Vida")]
    [SerializeField] private float life;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDamage;

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

    // Update is called once per frame
    void Update()
    {
        float distancePlayer = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("playerDistance", distancePlayer);        
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

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;

        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (!facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Invertir la dirección de movimiento
        rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
    }

    public void LookAtPlayer()
    {
        if (player.position.x > transform.position.x && !facingRight || (player.position.x < transform.position.x && facingRight))
        {
            Debug.Log("Flipeo");
            Flip();
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackController.position, attackRadius);
        Gizmos.DrawWireCube(groundChecker.position, dimensionesCaja);
    }
}
