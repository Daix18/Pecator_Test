using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerra_CaminarBehaviour : StateMachineBehaviour
{
    [SerializeField] private float speedMovement;

    Transform player;

    private GuerraBossController bossGuerra;

    private Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossGuerra = animator.GetComponent<GuerraBossController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = bossGuerra.rb;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Mantén la velocidad en el eje Y igual a la velocidad actual
        float currentYVelocity = rb.velocity.y;

        bossGuerra.LookAtPlayer();

        // Mantén la velocidad horizontal constante
        float currentXVelocity = rb.velocity.x;

        // Calcula el vector de movimiento hacia el jugador con una velocidad constante
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speedMovement * Time.fixedDeltaTime);

        // Establece la velocidad del jefe
        rb.velocity = new Vector2(currentXVelocity, currentYVelocity);

        // Mueve al jefe a la nueva posición
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= bossGuerra.attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        animator.ResetTrigger("Attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
