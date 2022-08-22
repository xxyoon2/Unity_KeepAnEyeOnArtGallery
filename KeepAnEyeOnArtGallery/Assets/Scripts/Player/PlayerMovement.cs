using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    public float MoveSpeed = 5f;    // 이동 속도
    private bool _isDead = false;

    private PlayerController _controller;
    private Rigidbody _rigidbody;


    void Start()
    {
        _controller = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {    
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dead")
        {
            Die();
        }
    }

    private void Move()
    {
        Vector3 dir = Vector3.right * _controller.X + Vector3.forward * _controller.Z;

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0f;
        dir.Normalize();

        _rigidbody.MovePosition(transform.position + dir * MoveSpeed * Time.deltaTime);
    }

    private void Die() 
    {
        _isDead = true;
        _rigidbody.velocity = Vector3.zero;

        GameManager.Instance.EndGame();
    }
}
