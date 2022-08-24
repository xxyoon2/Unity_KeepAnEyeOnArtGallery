using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadAttackState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Find("AttackRange").gameObject.SetActive(true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Find("AttackRange").gameObject.SetActive(false);
    }
}
