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
    public EnemyState state = EnemyState.Idle;
    public EnemyState prevState = EnemyState.None;

    Animator _animator;

    // 이동관련
    public Vector3 targetPos;
    public float moveSpeed = 2f;
    public float rotationSpeed = 2f;

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

        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        switch(state)
        {
            //case EnemyState.Idle: UpdateIdle(); break;
            //case EnemyState.Walk: UpdateWalk(); break;
            case EnemyState.Run: UpdateRun(); break;
            case EnemyState.Attack: UpdateAttack(); break;
        }
    }

    #region UpdateDetail
    // 매 프레임마다 수행해야 하는 동작 (상태가 바뀔 때 마다)

    /*
    void UpdateIdle()
    {

    }
    */
    /*
    void UpdateWalk()
    {
        if (IsFindEnemy())
        {
            ChangeState(EnemyState.Run);
            Debug.Log("발견했습니당^^!");
            return;
        }

        // 목적지까지 이동하는 코드
        Vector3 dir = targetPos - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude <= 0.2f)
        {
            ChangeState(EnemyState.Idle);
            return;
        }

        var targetRotation = Quaternion.LookRotation(targetPos - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    */
    void UpdateRun()
    {
        // 목적지까지 이동하는 코드
        Vector3 dir = targetPos - transform.position;
        if (dir.magnitude <= 3.0f)
        {
            ChangeState(EnemyState.Attack);
            return;
        }

        var targetRotation = Quaternion.LookRotation(targetPos - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * 2f * Time.deltaTime);

        transform.position += transform.forward * moveSpeed * 2f * Time.deltaTime;
    }

    void UpdateAttack()
    {
        Debug.Log("공듀공격합니다><");
    }

    #endregion


    #region CoroutineDetail
   
   /*
    IEnumerator CoroutineIdle()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("대기 상태 시작");
        _animator.SetBool("isIdle", true);
        
        while (true)
        {
            yield return new WaitForSeconds(3f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
            ChangeState(EnemyState.Walk);
            yield break;            
        }
    }
    */
    IEnumerator CoroutineWalk()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("순찰 상태 시작");
        _animator.SetBool("isWalk", true);

        // 목적지 설정
        //targetPos = transform.position + new Vector3(Random.Range(-7f, 7f), 0f, Random.Range(-7f, 7f));

        while (true)
        {
            yield return new WaitForSeconds(10f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
            //ChangeState(EnemyState.Idle);
        }
    }
    IEnumerator CoroutineRun()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        _animator.SetBool("isRun", true);
        targetPos = Target.transform.position;
        targetPos.y = 0f;

        while (true)
        {
            yield return new WaitForSeconds(5f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
        }
    }

    IEnumerator CoroutineAttack()
    {
        _animator.SetBool("isAttack", true);
        
        while (true)
        {
            yield return new WaitForSeconds(1f);
            ChangeState(EnemyState.Idle);
        }
    }

    #endregion

    public void ChangeState(EnemyState nextState)
    {
        if (state == nextState) return;
        
        StopAllCoroutines();

        prevState = state;
        state = nextState;

        _animator.SetBool("isIdle", false);
        _animator.SetBool("isWalk", false);
        _animator.SetBool("isRun", false);
        _animator.SetBool("isAttack", false);

        switch (state)
        {
            //case EnemyState.Idle: StartCoroutine(CoroutineIdle()); break;
            case EnemyState.Walk: StartCoroutine(CoroutineWalk()); break;
            case EnemyState.Run: StartCoroutine(CoroutineRun()); break;
            case EnemyState.Attack: StartCoroutine(CoroutineAttack()); break;
        }
    }

    public bool IsFindEnemy()
    {
        // 오브젝트가 활성화되어있지 않다면 false 반환
        if (!Target.activeSelf) return false;

        // 타겟 경계를 생성
        // 여기서 널 레퍼런스가 뜸 >> 해결
        Bounds targetBounds = Target.GetComponentInChildren<MeshRenderer>().bounds;

        // 카메라에서 프러스텀 평면 생성
        // 각 평면은 프러스텀의 벽 한 면을 나타내는 것
        eyePlanes = GeometryUtility.CalculateFrustumPlanes(Eye);
        // 프러스텀 평면 안에 해당 오브젝으가 있는지 검사
        _isFindEnemy = GeometryUtility.TestPlanesAABB(eyePlanes, targetBounds);

        return _isFindEnemy;
    }
}
