using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueBehavior : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private FSMAIController controller;
    private Animator anim;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponentInParent<FSMAIController>();
        agent = controller.agent;
        anim = controller.anim;
        agent.speed = 5;
        agent.isStopped = false;
        anim.SetTrigger("isRunning");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(controller.player.position);
        if (agent.hasPath)
        {
            animator.SetBool("CanAttackPlayer", controller.CanAttackPlayer());
            animator.SetBool("CanSeePlayer", controller.CanSeePlayer());
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.ResetTrigger("isRunning");
    }

}
