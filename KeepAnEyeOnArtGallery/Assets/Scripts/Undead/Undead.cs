using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead : MonoBehaviour
{
    private static GameObject s_player = null;
    private static MeshRenderer s_playerMeshRenderer = null;

    // 적 탐지 관련
    private Camera _eye;
    private PlayerMovement _playerMovement;
    private Animator _animator;


    // 이동관련
    public Vector3 DestinationPos;
    public float _moveSpeed = 2f;
    public float rotationSpeed = 2f;


    Plane[] eyePlanes;
    public bool IsFindEnemy = false;

    private void Awake()
    {
        if (s_player == null)
        {
            s_player = GameObject.FindGameObjectWithTag("Player");
            s_playerMeshRenderer = s_player.GetComponentInChildren<MeshRenderer>();
        }

        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _playerMovement = s_player.GetComponent<PlayerMovement>();
        _eye = GetComponentInChildren<Camera>();

        StartCoroutine(UpdateDestination());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IsFindEnemy = true;
            _animator.SetTrigger(AnimID.Attack);

            GameManager.Instance.OnGameEnd();
        }
    }

    IEnumerator UpdateDestination()
    {
        while(true)
        {
            // 플레이어 찾았는지 판단
            if (_playerMovement.InSafeZone)
            {
                IsFindEnemy = false;
            }
            else
            {
                // 오브젝트가 활성화되어있지 않다면 false 반환
                //if (!Target.activeSelf) _isFindEnemy = false;
                // 타겟 경계를 생성
                // 여기서 널 레퍼런스가 뜸 >> 해결
                Bounds targetBounds = s_playerMeshRenderer.bounds;

                // 카메라에서 프러스텀 평면 생성
                // 각 평면은 프러스텀의 벽 한 면을 나타내는 것
                Plane[] eyePlanes = GeometryUtility.CalculateFrustumPlanes(_eye);
                // 프러스텀 평면 안에 해당 오브젝트가 있는지 검사
                IsFindEnemy = GeometryUtility.TestPlanesAABB(eyePlanes, targetBounds);
            }

            // 목적지 설정
            if (IsFindEnemy)
            {
                DestinationPos = s_player.transform.position;
                _animator.SetBool(AnimID.FindEnemy, true);
                Debug.Log("찾았어용^^");
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                DestinationPos = transform.position + new Vector3(Random.Range(-5f, 6f), 0f, Random.Range(-2f, 5f));
                _animator.SetBool(AnimID.FindEnemy, false);
                Debug.Log("못찾겠어ㅇㅛㅇ");
                yield return new WaitForSeconds(2f);
            }

        }
        
    }
}
