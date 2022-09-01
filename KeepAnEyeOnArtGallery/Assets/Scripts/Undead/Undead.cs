using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead : MonoBehaviour
{
    // 적 탐지 관련
    public GameObject Target;
    public Camera Eye;

    private PlayerMovement _player;
    private Animator _animator;


    // 이동관련
    public Vector3 DestinationPos;
    public float _moveSpeed = 2f;
    public float rotationSpeed = 2f;


    Plane[] eyePlanes;
    public bool IsFindEnemy = false;

    void Start()
    {
        _player = Target.GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        Eye = transform.GetComponentInChildren<Camera>();

        StartCoroutine(UpdateDestination());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetTrigger(AnimID.Attack);

            IsFindEnemy = true;
            GameManager.Instance.OnGameEnd();
        }
    }

    IEnumerator UpdateDestination()
    {
        while(true)
        {
            // 플레이어 찾았는지 판단
            if (_player._isPlayerInSaveZone)
            {
                IsFindEnemy = false;
            }
            else
            {
                // 오브젝트가 활성화되어있지 않다면 false 반환
                //if (!Target.activeSelf) _isFindEnemy = false;
                // 타겟 경계를 생성
                // 여기서 널 레퍼런스가 뜸 >> 해결
                Bounds targetBounds = Target.GetComponentInChildren<MeshRenderer>().bounds;

                // 카메라에서 프러스텀 평면 생성
                // 각 평면은 프러스텀의 벽 한 면을 나타내는 것
                Plane[] eyePlanes = GeometryUtility.CalculateFrustumPlanes(Eye);
                // 프러스텀 평면 안에 해당 오브젝트가 있는지 검사
                IsFindEnemy = GeometryUtility.TestPlanesAABB(eyePlanes, targetBounds);

            }

            // 목적지 설정
            if (IsFindEnemy)
            {
                DestinationPos = Target.transform.position;
                _animator.SetBool(AnimID.FindEnemy, true);

                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                DestinationPos = transform.position + new Vector3(Random.Range(-5f, 6f), 0f, Random.Range(-2f, 5f));
                _animator.SetBool(AnimID.FindEnemy, false);

                yield return new WaitForSeconds(2f);
            }

        }
        
    }
}
