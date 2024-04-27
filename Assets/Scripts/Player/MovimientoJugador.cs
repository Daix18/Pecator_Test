using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    public static MovimientoJugador THIS;

    private Controls controles;
    private Rigidbody2D rb;
    private TrailRenderer tr;
    private Animator animator;
    private Transform oldParent;

    [Header("Movimiento")]
    [SerializeField] private float speedMovement;
    [SerializeField] private float speedGroundMovement;
    [SerializeField] private float speedAirMovement;

    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento;
    private float normalGravity;
    private Vector3 velocidad = Vector3.zero;
    private Vector2 direccion;
    [HideInInspector] public bool mirandoDerecha = true;
    [SerializeField] private Rigidbody2D rbPlatform;
    [SerializeField] private bool onPlatform;

    [Header("Camera Settings")]
    [SerializeField] private GameObject _camera;
    private CameFollowController _cameFollowController;

    [Header("Salto")]
    [SerializeField] private int _jumpsLeft;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float jumpingForce;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool onGround;
    private bool isJumping = false;
    private float jumpBufferCounter;

    [Header("Wall Slide Settings")]
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private bool onWall;
    [SerializeField] private bool wallSliding;

    [Header("Wall Jump Settings")]
    [SerializeField] private float jumpForceWallX;
    [SerializeField] private float jumpForceWallY;
    [SerializeField] private float wallJumpTime;
    [SerializeField] private bool wallJumping;

    [Header("SaltoPared")]
    [SerializeField] private Transform wallChecker;
    [SerializeField] private Vector3 wallBoxDimensions;

    [Header("Dash Settings")]
    [SerializeField] private int maxDashes = 1;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float dashGravity;
    [SerializeField] private int _dashesLeft;
    [SerializeField] private bool isDashing;
    private float waitTime;
    private Vector2 dashingDir;
    private bool canDash = true;

    [Header("Knife Mechanic Settings")]
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private Transform lanzamientoPosicion;
    [SerializeField] private float fuerzaLanzamiento = 10f;

    [Header("Coyote Time")]
    [Range(0.01f, 0.5f)][SerializeField] private float coyoteTime;
    [Range(0.01f, 0.5f)][SerializeField] private float jumpInputBufferTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        _cameFollowController = _camera.GetComponent<CameFollowController>();
        _jumpsLeft = maxJumps;
        _dashesLeft = maxDashes;
        normalGravity = rb.gravityScale;
    }

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
        }

        controles = new();
    }

    private void Update()
    {
        waitTime += Time.deltaTime;

        if (onGround && rb.velocity.y <= 0)
        {
            _jumpsLeft = maxJumps;
            _dashesLeft = maxDashes;
            isJumping = false;
        }        

        //Si el jugador est� cayendo, se multiplica la gravedad y se le resta 1, para que este proporcionada a la gravedad.
        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //En el caso de que el jugador est� ascendiendo y no se presiona el salto, el salto es m�s suave.
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;                       
        }

        if (!onGround && onWall && direccion.x != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (!onGround)
        {
            AttackController.THIS.canAttack = false;
        }
        else
        {
            AttackController.THIS.canAttack = true;
        }

        if (AttackController.THIS.attacking)
        {
            rb.velocity = Vector2.zero;
        }

        //Si no ha hecho un wall jump, est� pegado en una pared y est� haciendo un wall Slide, se hace un wall Jump.
        if (!wallJumping && onWall && wallSliding)
        {
            if (Input.GetButtonDown("Jump"))
            {
                //Salto en pared
                WallJump();
            }
        }
    }

    private void FixedUpdate()
    {
        onGround = Physics2D.OverlapBox(groundChecker.position, dimensionesCaja, 0f, queEsSuelo);

        onWall = Physics2D.OverlapBox(wallChecker.position, wallBoxDimensions, 0f, queEsSuelo);

        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("wallSliding", wallSliding);
        animator.SetBool("isDashing", isDashing);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("onPlatform", onPlatform);

        if (onGround)
            speedMovement = speedGroundMovement;
        else
            speedMovement = speedAirMovement;

        //Se aplica el moviemiento del jugador, en terminos de velocidad.
        if (!wallJumping && !isDashing)
        {
            Vector3 velocidadObjetivo = new Vector2(direccion.x * speedMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);
        }

        if (!wallJumping && wallSliding)
        {
            wallJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }

    private void Move()
    {
        direccion = controles.Player.Mover.ReadValue<Vector2>();

        if (direccion.x > 0 && !mirandoDerecha)
        {
            Debug.Log("Flipeo");
            Flip();
        }
        else if (direccion.x < 0 && mirandoDerecha)
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (_jumpsLeft > 0)
        {
            rb.velocity = new Vector2(0f, jumpingForce);
            _jumpsLeft -= 1;
            isJumping = true;
            Debug.Log("Salto");
        }
    }
    private void Flip()
    {
        if (mirandoDerecha)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            mirandoDerecha = !mirandoDerecha;
            _cameFollowController.CallTurn();
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            mirandoDerecha = !mirandoDerecha;
            _cameFollowController.CallTurn();
        }
    }

    private void WallJump()
    {
        onWall = false;
        rb.velocity = new Vector2(-direccion.x * jumpForceWallX, jumpForceWallY);
        StartCoroutine(WallJumpChange());
    }

    public void Dash()
    {
        canDash = false;
        isDashing = true;
        tr.emitting = true;
        isJumping  = false;
        rb.gravityScale = dashGravity;
        dashingDir = new Vector2(direccion.x, 0);

        float direccionDashX = transform.rotation.eulerAngles.y > 0 ? -1f : 1f;

        if (dashingDir == Vector2.zero)
        {
            dashingDir = new Vector2(direccionDashX, 0);
        }


        //Esto es para hacer el dash de manera diagonal.
        // Obtener la dirección de entrada del jugador
        Vector2 direccionInput = new Vector2(direccion.x, direccion.y).normalized;

        // Definir la dirección del dash
        if (Mathf.Abs(direccionInput.x) != 0 && Mathf.Abs(direccionInput.y) != 0)
        {
            // Si ambas componentes de la dirección de entrada son diferentes de cero,
            // entonces la dirección del dash se define en la dirección de entrada
            dashingDir = direccionInput;
        }
        else if (Mathf.Abs(direccionInput.x) > Mathf.Abs(direccionInput.y))
        {
            // Si la magnitud de la componente x de la dirección de entrada es mayor que la de la componente y,
            // entonces la dirección del dash se define en la componente x de la dirección de entrada,
            // con la componente y igual a cero
            dashingDir = new Vector2(direccionInput.x, 0).normalized;
        }

        if (isDashing)
        {
            // Establecer la velocidad basada en la escala local x del objeto y la potencia de dash
            rb.velocity = dashingDir.normalized * dashingPower;
        }
    }
    void LanzarCuchillo()
    {
        GameObject projectile = Instantiate(knifePrefab, lanzamientoPosicion.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        // Asumiendo que el personaje mira hacia la derecha. Si no, necesitar�s ajustar la direcci�n bas�ndote en la orientaci�n del personaje.
        rb.velocity = new Vector2(transform.localScale.x * fuerzaLanzamiento, 0);
    }

    //Estas funciones son llamadas desde el componente de player input, el cual lo contiene el player.
    //Para verlas le damos a evet
    public void StartMove(InputAction.CallbackContext context)
    {
        Move();
    }

    public void StartJump(InputAction.CallbackContext context)
    {
        //Comprobaci�n para saltar que incluye el coyote jump.
        if (context.performed && !wallSliding)
        {
            //Salto normal
            Jump();
        }
    }

    public void StartDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            if (waitTime >= dashingCooldown)
            {
                waitTime = 0;
                Dash();
            }
        }
    }

    public void StartAttack(InputAction.CallbackContext context)
    {
        if (context.performed && AttackController.THIS.tiempoSiguienteAtaque <= 0)
        {
            Debug.Log("Golpe");
            AttackController.THIS.Golpe();
            AttackController.THIS.tiempoSiguienteAtaque = AttackController.THIS.tiempoEntreAtaques;
        }
    }

    public void StartKnife(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LanzarCuchillo();
        }
    }

    public void StopDash()
    {
        canDash = true;
        isDashing = false;
        tr.emitting = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = normalGravity;
    }

    //Cuando entremos en al escena, los controles se cargan
    private void OnEnable()
    {
        controles.Enable();
        controles.Player.Mover.performed += ctx => direccion = ctx.ReadValue<Vector2>(); // Asignar el valor de la direcci�n del movimiento
        controles.Player.Mover.canceled += ctx => direccion = Vector2.zero; // Limpiar la direcci�n del movimiento cuando se detiene
    }

    //Cuando salgamos de la escena, los controles se desactivan
    private void OnDisable()
    {
        controles.Disable();
        controles.Player.Mover.performed -= ctx => direccion = ctx.ReadValue<Vector2>(); // Quitar el listener
        controles.Player.Mover.canceled -= ctx => direccion = Vector2.zero; // Quitar el listener
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundChecker.position, dimensionesCaja);
        Gizmos.DrawWireCube(wallChecker.position, wallBoxDimensions);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MovingPlatform")
        {
            DontDestroyOnLoad(collision.gameObject);
            oldParent = transform.parent;
            transform.parent = collision.transform;
            rbPlatform = collision.GetComponent<Rigidbody2D>(); // Obtener referencia al Rigidbody de la plataforma
            onPlatform = true; // Establecer la bandera enPlataformaMovil a true
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MovingPlatform")
        {
            if (onPlatform && rbPlatform != null)
            {
                Vector2 movimientoPlataforma = rbPlatform.velocity * Time.fixedDeltaTime;
                transform.position += (Vector3)movimientoPlataforma;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MovingPlatform")
        {
            collision.transform.parent = null;
            GameObject platforms = GameObject.Find("Platforms");
            Transform temp = transform.parent;
            transform.parent = oldParent;
            temp.parent = PersistController.THIS.transform.parent;
            collision.transform.parent = platforms.transform;
            onPlatform = false; 
        }
    }

    //Corrutinas:

    //Corrutina del cambio de WallJump
    IEnumerator WallJumpChange()
    {
        wallJumping = true;
        yield return new WaitForSeconds(wallJumpTime);
        wallJumping = false;
        //canMoveSideways = true;
    }
}