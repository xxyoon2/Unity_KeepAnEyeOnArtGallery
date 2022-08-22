using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead : MonoBehaviour
{
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

    public GameObject SafeZone;

    void Start()
    {
        _animator = GetComponent<Animator>();
        Eye = transform.GetComponentInChildren<Camera>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetTrigger(AnimID.Attack);
        }
    }

    #region UpdateDetail
    #endregion


    #region CoroutineDetail
    #endregion
    public bool IsFindEnemy()
    {
        // 오브젝트가 활성화되어있지 않다면 false 반환
        if (!Target.activeSelf) return false;
        if (SafeZone.GetComponent<SafeZone>().isPlayerInHere) return false;

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
