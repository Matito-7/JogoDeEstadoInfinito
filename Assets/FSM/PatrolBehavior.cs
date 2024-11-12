using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolBehavior : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private FSMAIController controller;
    private Animator anim;
    int currentIndex = -1;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponentInParent<FSMAIController>();
        agent = controller.agent;
        anim = controller.anim;
        agent.speed = 2;
        agent.isStopped = false;

        float lastDistance = Mathf.Infinity;
        for(int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++)
        {
            float distance = Vector3.Distance(agent.gameObject.transform.position, GameEnvironment.Singleton.Checkpoints[i].transform.position);
            if(distance < lastDistance)
            {
                currentIndex = i - 1;
                lastDistance = distance;
            }
        }
        anim.SetTrigger("isWalking");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.remainingDistance < 1)
        {
            currentIndex =
                currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1 ?
                0 :
                currentIndex + 1;
            agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
        }
        animator.SetBool("CanSeePlayer", controller.CanSeePlayer());
        animator.SetBool("IsPlayerBehind", controller.IsPlayerBehind());
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.ResetTrigger("IsWalking");
    }
}
