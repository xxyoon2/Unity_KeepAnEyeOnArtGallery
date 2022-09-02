using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE,
    MOVE,
    DIE
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerState _pState;
    [SerializeField] public float _moveSpeed = 5f;
    
    public bool InSafeZone { get; private set; } = true;

    private PlayerController _controller;
    private Rigidbody _rigidbody;


    void Start()
    {
        _pState = PlayerState.MOVE;

        _controller = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {   
        if (_pState == PlayerState.MOVE)
        {
            Move();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SafeZone")
        {
            InSafeZone = true;
            Debug.Log($"들어왔음 : {InSafeZone}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // layer 쓰세요.
        if (other.tag == "SafeZone")
        {
            InSafeZone = false;
            Debug.Log($"나감 : {InSafeZone}");
        }
    }

    private void Move()
    {
        Vector3 dir = Vector3.right * _controller.X + Vector3.forward * _controller.Z;

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0f;
        dir.Normalize();

        _rigidbody.MovePosition(transform.position + dir * _moveSpeed * Time.deltaTime);
    }

    public void ChangePlayerState(PlayerState state)
    {
        _pState = state;
        switch (state)
        {
            case PlayerState.IDLE : 
                _moveSpeed = 0f;
                _controller.rotSpeed = 0f;
                break;
            case PlayerState.MOVE : 
                _moveSpeed = 5f;
                _controller.rotSpeed = 200f;
                break;
            case PlayerState.DIE : 
                _moveSpeed = 0f;
                break; 
        }
    }
    
    public PlayerState WhatStats()
    {
        return _pState;
    }
}
