using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UndeadMoveState : StateMachineBehaviour
{
    // 필요한 컴포넌트
    Undead Undead;
    Transform UndeadTransform;
    NavMeshAgent navMeshAgent;

    Vector3 dir, targetPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Undead = animator.GetComponent<Undead>();
        navMeshAgent = animator.GetComponent<NavMeshAgent>();
        
        // 언데드는 멈추지 않아요
        navMeshAgent.isStopped = false;

        UndeadTransform = animator.transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent.destination = Undead.DestinationPos;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent.isStopped = true;
    }
}
