using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera PlayerCamera;

    private Camera[] _cameras;
    private int _cameraEnabled = 0;
    private int _cameraIndex = 4;
    private bool _isPlayerEnter = false;
    private bool _isCCTVOn = false;
    private PlayerMovement _player;
    private PlayerController _controller;


    void Start()
    {
        _cameras = new Camera[_cameraIndex];
        for (int i = 0; i < _cameraIndex; ++i)
        {
            _cameras[i] = transform.GetChild(i).GetComponent<Camera>();
        }

        _player = GetComponent<PlayerMovement>();
        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(_isPlayerEnter)
        {
            //Debug.Log($"{_controller.CanCCTVOn}");
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isCCTVOn = !_isCCTVOn;
            }

            if (_isCCTVOn)
            {
                _player.ChangePlayerState(PlayerState.IDLE);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    _cameraEnabled = (_cameraEnabled - 1) % _cameraIndex;
                    if (_cameraEnabled < 0)
                    {
                        _cameraEnabled = _cameraIndex - 1;
                    }
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    _cameraEnabled = (_cameraEnabled + 1) % _cameraIndex;
                }
                ShowCCTV();
            }
            else
            {
                PlayerCamera.enabled = true;
                _player.ChangePlayerState(PlayerState.MOVE);
            }
        }
    }

    public void ShowCCTV()
    {
        PlayerCamera.enabled = false;
        for (int i = 0; i < _cameraIndex; ++i)
        {
            _cameras[i].enabled = false;
        }
        _cameras[_cameraEnabled].enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerEnter = true;
            _player = other.GetComponent<PlayerMovement>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerEnter = false;
            _isCCTVOn = false;
        }
    }
}