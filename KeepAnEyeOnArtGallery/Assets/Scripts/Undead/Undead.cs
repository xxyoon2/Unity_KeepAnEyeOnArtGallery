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
    Plane[] eyePlanes;

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

}
