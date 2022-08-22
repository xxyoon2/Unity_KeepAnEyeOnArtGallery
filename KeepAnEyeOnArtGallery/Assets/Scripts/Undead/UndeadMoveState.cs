using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadMoveState : StateMachineBehaviour
{
    Undead Undead;
    Vector3 dir, targetPos;
    Transform UndeadTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Undead = animator.GetComponent<Undead>();

        UndeadTransform = animator.transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Undead.IsFindEnemy())
        {
            animator.SetBool(AnimID.FindEnemy, true);
            targetPos = Undead.Target.transform.position;
            targetPos.y = 0f;
            
        }
        else
        {
            animator.SetBool(AnimID.FindEnemy, false);
            targetPos = UndeadTransform.position + new Vector3(Random.Range(-5f, 6f), 0f, Random.Range(-2f, 5f));
        }

        dir = targetPos - UndeadTransform.position;

        if (dir.sqrMagnitude <= 0.2f)
        {
            animator.SetTrigger(AnimID.Idle);
            return;
        }

        Movement();
    }

    void Movement()
    {
        var targetRotation = Quaternion.LookRotation(targetPos - UndeadTransform.position, Vector3.up);
        UndeadTransform.rotation = Quaternion.Slerp(UndeadTransform.rotation, targetRotation, Undead.rotationSpeed * Time.deltaTime);

        UndeadTransform.position += UndeadTransform.forward * Undead.moveSpeed * Time.deltaTime;
    }

    /*
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    */
}
