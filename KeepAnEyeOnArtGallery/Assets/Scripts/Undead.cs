using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    None,       // 
    Idle,       // 대기
    Walk,       // 순찰 patrol
    Run,        // 추적 trace
    Attack,     // 공격
} 

public class Undead : MonoBehaviour
{
    public EnemyState state;
    public EnemyState prevState = EnemyState.None;

    Animator _animator;

    // 이동관련
    Vector3 targetPos;
    float moveSpeed = 1f;
    float rotationSpeed = 1f;

    // 적 탐지 관련
    public GameObject Target;
    public Camera Eye;
    bool _isFindEnemy = false;
    Plane[] eyePlanes;

    // 공격 충돌 관련
    GameObject weaponCollider;

    void Start()
    {

        _animator = GetComponent<Animator>();
        Eye = transform.GetComponentInChildren<Camera>();

        /*
        SphereCollider[] sphereColliders = GetComponentsInChildren<SphereCollider>();

        foreach(var sphereCollider in sphereColliders)
        {
            if (sphereCollider.name == "WeaponCollider")
            {
                weaponCollider = sphereCollider.gameObject;
                break;
            }
        }
        weaponCollider.SetActive(false);
        */
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        switch(state)
        {
            case EnemyState.Idle: UpdateIdle(); break;
            case EnemyState.Walk: UpdateWalk(); break;
            case EnemyState.Run: UpdateRun(); break;
            //case EnemyState.Attack: UpdateAttack(); break;
        }
    }

    #region UpdateDetail
    // 매 프레임마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
    void UpdateIdle()
    {

    }
    
    void UpdateWalk()
    {
        if (IsFindEnemy())
        {
            ChangeState(EnemyState.Run);
            return;
        }

        // 목적지까지 이동하는 코드
        Vector3 dir = targetPos - transform.position;
        if (dir.sqrMagnitude <= 0.2f)
        {
            ChangeState(EnemyState.Idle);
            return;
        }

        var targetRotation = Quaternion.LookRotation(targetPos - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void UpdateRun()
    {
        // 목적지까지 이동하는 코드
        Vector3 dir = targetPos - transform.position;
        //Debug.Log("타겟거리 : " + dir.magnitude);
        /*
        if (dir.magnitude <= 2.0f)
        {
            ChangeState(EnemyState.Attack);
            return;
        }
        */

        var targetRotation = Quaternion.LookRotation(targetPos - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * 2f * Time.deltaTime);

        transform.position += transform.forward * moveSpeed * 2f * Time.deltaTime;
    }
    /*
    void UpdateAttack()
    {

    }
    */
    #endregion


    #region CoroutineDetail
    IEnumerator CoroutineIdle()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("대기 상태 시작");
        _animator.SetBool("isIdle", true);

        while (true)
        {
            yield return new WaitForSeconds(2f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
            yield break;            
        }
    }
    IEnumerator CoroutineWalk()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("순찰 상태 시작");
        _animator.SetBool("isWalk", true);

        // 목적지 설정
        targetPos = transform.position + new Vector3(Random.Range(-7f, 7f), 0f, Random.Range(-7f, 7f));

        while (true)
        {
            yield return new WaitForSeconds(10f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
            ChangeState(EnemyState.Idle);
        }
    }
    IEnumerator CoroutineRun()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("추적 상태 시작");
        _animator.SetBool("isRun", true);
        targetPos = Target.transform.position;

        while (true)
        {
            yield return new WaitForSeconds(5f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)

        }
    }


    /*
    IEnumerator CoroutineAttack()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        _animator.SetTrigger("isAttack");

        yield return new WaitForSeconds(1f);
        ChangeState(EnemyState.Idle);
        yield break;
    }
    */

    #endregion

    void ChangeState(EnemyState nextState)
    {
        if (prevState == nextState) return;

        StopAllCoroutines();

        prevState = state;
        state = nextState;
        _animator.SetBool("isIdle", false);
        _animator.SetBool("isWalk", false);
        _animator.SetBool("isRun", false);
        //_animator.SetBool("isAttack", false);

        switch (state)
        {
            case EnemyState.Idle: StartCoroutine(CoroutineIdle()); break;
            case EnemyState.Walk: StartCoroutine(CoroutineWalk()); break;
            case EnemyState.Run: StartCoroutine(CoroutineRun()); break;
            //case EnemyState.Attack: StartCoroutine(CoroutineAttack()); break;
        }
    }

    bool IsFindEnemy()
    {
        if (!Target.activeSelf) return false;

        Bounds targetBounds = Target.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
        eyePlanes = GeometryUtility.CalculateFrustumPlanes(Eye);
        _isFindEnemy = GeometryUtility.TestPlanesAABB(eyePlanes, targetBounds);

        return _isFindEnemy;
    }

    /*
    void OnAttack(AnimationEvent animationEvent)
    {
        Debug.Log("OnAttack() : " + animationEvent.intParameter);  
        if (animationEvent.intParameter == 1)
        {
            weaponCollider.SetActive(true);
        }
        else
        {
            weaponCollider.SetActive(false);
        }
    }
    */
}
