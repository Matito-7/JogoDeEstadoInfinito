using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunawayBehavior : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private FSMAIController controller;
    private Animator anim;
    Transform safeLocation;
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponentInParent<FSMAIController>();
        agent = controller.agent;
        anim = controller.anim;
        safeLocation = GameEnvironment.Singleton.safeLocation;
        anim.SetTrigger("isRunning");
        agent.isStopped = false;
        agent.speed = 6;
        agent.SetDestination(safeLocation.position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.remainingDistance < 1)
        {
            animator.SetBool("IsInSafeHouse", true);
        }
        else
        {
            animator.SetBool("IsInSafeHouse", false);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.ResetTrigger("isRunning");
    }
}
