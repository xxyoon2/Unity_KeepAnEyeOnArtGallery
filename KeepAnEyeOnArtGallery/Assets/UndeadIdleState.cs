using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadIdleState : StateMachineBehaviour
{
    Undead Undead;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("시작");
        Undead = animator.GetComponent<Undead>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("준비");
        if (Undead.IsFindEnemy())
        {
            Debug.Log("발견했습니당^^!");
            animator.SetBool("isIdle", false);
            animator.SetBool("isRun", true);
            //Undead.ChangeState(EnemyState.Run);
            Undead.state = EnemyState.Run;
        }

        animator.SetBool("isIdle", false);
        //animator.SetBool("isWalk", true);
        Debug.Log($"{Undead.state}");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Undead.state = EnemyState.Walk;
        Debug.Log($"{Undead.state}");
    }
}
