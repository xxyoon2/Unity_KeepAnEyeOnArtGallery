using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadIdleState : StateMachineBehaviour
{
    Undead Undead;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Undead = animator.GetComponent<Undead>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger(AnimID.Move);
    }

    
}
