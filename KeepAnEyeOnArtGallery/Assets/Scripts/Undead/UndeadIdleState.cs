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
        // if (Undead.IsFindEnemy())
        // {
        //     animator.SetBool(AnimID.FindEnemy, true);
        // }
        
        animator.SetTrigger(AnimID.Move);
    }

    
}
