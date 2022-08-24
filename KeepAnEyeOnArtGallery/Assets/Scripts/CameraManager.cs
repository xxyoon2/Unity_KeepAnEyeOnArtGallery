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
    private PlayerMovement _movement;


    void Start()
    {
        _cameras = new Camera[_cameraIndex];
        for(int i = 0; i < _cameraIndex; ++i)
        {
            _cameras[i] = transform.GetChild(i).GetComponent<Camera>();
        }
    }

    void Update()
    {
        if(_isPlayerEnter && Input.GetKeyDown(KeyCode.E))
        {
            _isCCTVOn = !_isCCTVOn;
            Debug.Log("CCTV 상태변함");
        }
        if(_isCCTVOn)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                _cameraEnabled = (_cameraEnabled - 1) % _cameraIndex;
                if (_cameraEnabled < 0)
                {
                    _cameraEnabled = _cameraIndex - 1;
                }
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                _cameraEnabled = (_cameraEnabled + 1) % _cameraIndex;
            }
            ShowCCTV();
        }
        else
        {
            PlayerCamera.enabled = true;
            if(_movement != null) _movement.MoveSpeed = 15;
        }
    }

    public void ShowCCTV()
    {
        PlayerCamera.enabled = false;
        if (_movement != null) _movement.MoveSpeed = 0;
        for (int i = 0; i < _cameraIndex; ++i)
        {
            _cameras[i].enabled = false;
        }
        _cameras[_cameraEnabled].enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _isPlayerEnter = true;
            _movement = other.GetComponent<PlayerMovement>();
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
