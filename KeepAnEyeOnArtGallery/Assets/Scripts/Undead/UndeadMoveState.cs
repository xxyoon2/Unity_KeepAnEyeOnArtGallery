using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UndeadMoveState : StateMachineBehaviour
{
    Undead Undead;
    Vector3 dir, targetPos;
    Transform UndeadTransform;

    bool _isFindEnemy = false;

    NavMeshAgent navMeshAgent;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Undead = animator.GetComponent<Undead>();
        Transform target = Undead.Target.transform;
        navMeshAgent = animator.GetComponent<NavMeshAgent>();
        //navMeshAgent.destination = target.position;
        navMeshAgent.isStopped = false;

        UndeadTransform = animator.transform;
        Undead.StartCoroutine(UpdateDestination());
    }

    IEnumerator UpdateDestination()
    {
        while(true)
        {
            // 오브젝트가 활성화되어있지 않다면 false 반환
            //if (!Target.activeSelf) _isFindEnemy = false;
            // 타겟 경계를 생성
            // 여기서 널 레퍼런스가 뜸 >> 해결
            Bounds targetBounds = Undead.Target.GetComponentInChildren<MeshRenderer>().bounds;

            // 카메라에서 프러스텀 평면 생성
            // 각 평면은 프러스텀의 벽 한 면을 나타내는 것
            Plane[] eyePlanes = GeometryUtility.CalculateFrustumPlanes(Undead.Eye);
            // 프러스텀 평면 안에 해당 오브젝트가 있는지 검사
            _isFindEnemy = GeometryUtility.TestPlanesAABB(eyePlanes, targetBounds);

            if (_isFindEnemy)
            {
                navMeshAgent.destination = Undead.Target.transform.position;
                Undead.GetComponent<Animator>().SetBool(AnimID.FindEnemy, true);
            }
            else
            {
                navMeshAgent.destination = UndeadTransform.position + new Vector3(Random.Range(-5f, 6f), 0f, Random.Range(-2f, 5f));
                Undead.GetComponent<Animator>().SetBool(AnimID.FindEnemy, false);
            }

            yield return new WaitForSeconds(3f);
        }
        
    }
    /*
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
    */
    /*
    void Movement()
    {
        var targetRotation = Quaternion.LookRotation(targetPos - UndeadTransform.position, Vector3.up);
        UndeadTransform.rotation = Quaternion.Slerp(UndeadTransform.rotation, targetRotation, Undead.rotationSpeed * Time.deltaTime);
        UndeadTransform.position += UndeadTransform.forward * Undead.moveSpeed * Time.deltaTime;
    }
    */
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent.isStopped = true;
    }
    
}