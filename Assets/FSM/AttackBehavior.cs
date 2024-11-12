using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private FSMAIController controller;
    private Animator anim;
    private AudioSource shoot;
    float rotationSpeed = 2.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponentInParent<FSMAIController>();
        agent = controller.agent;
        anim = controller.anim;
        shoot = controller.audioSource;
        agent.isStopped = true;
        shoot.Play();
        anim.SetTrigger("isShooting");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = controller.player.position - controller.gameObject.transform.position;
        direction.y = 0;
        controller.gameObject.transform.rotation = Quaternion.Slerp(controller.gameObject.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        animator.SetBool("CanAttackPlayer", controller.CanAttackPlayer());
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.ResetTrigger("isShooting");
        shoot.Stop();
    }
}
