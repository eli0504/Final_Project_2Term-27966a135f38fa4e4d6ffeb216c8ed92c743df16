using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability_boss_behaviour : StateMachineBehaviour
{
    [SerializeField] private GameObject ability;
    [SerializeField] private float offsetY; //ability pos
    private Finalboss finalboss;
    private Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        finalboss = animator.GetComponent<Finalboss>();
        player = finalboss.player; //player pos

        finalboss.LookAtPlayer();

        // Set the boss to attacking state
        finalboss.SetAttacking(true);

        //where have the ability to appear
        Vector2 appearPos = new Vector2(player.position.x, player.position.y + offsetY);

        Instantiate(ability, appearPos, Quaternion.identity);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set the boss to non-attacking state
        finalboss.SetAttacking(false);
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
