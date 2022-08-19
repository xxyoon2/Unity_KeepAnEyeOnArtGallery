using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadWalkState : StateMachineBehaviour
{
    Undead Undead;
    Vector3 dir, targetPos;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Undead = animator.GetComponent<Undead>();
        targetPos = animator.transform.position + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-2f, 4f));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Undead.IsFindEnemy())
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", true);
            Undead.state = EnemyState.Run;
            //Undead.ChangeState(EnemyState.Run);
        }

        dir = targetPos - animator.transform.position;
        //dir.y = 0f;

        if (dir.sqrMagnitude <= 0.2f)
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isIdle", true);
            Undead.state = EnemyState.Idle;
            //Undead.ChangeState(EnemyState.Idle);
            return;
        }

        var targetRotation = Quaternion.LookRotation(targetPos - animator.transform.position, Vector3.up);
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, targetRotation, Undead.rotationSpeed * Time.deltaTime);

        animator.transform.position += animator.transform.forward * Undead.moveSpeed * Time.deltaTime;
    }

    /*
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    */
}
