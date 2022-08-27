using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera PlayerCamera;
    //public GameObject Player;
    public GameObject CCTVPanel;

    private Camera[] _cameras;
    private bool _isPlayerEnter = false;
    private bool _isCCTVOn = false;

    private PlayerMovement _player;
    private PlayerController _controller;


    void Start()
    {
    }

    void Update()
    {
        if(_isPlayerEnter)
        {
            if (_controller.CanCCTVOn)
            {
                _isCCTVOn = !_isCCTVOn;
            }

            if (_isCCTVOn)
            {
                _player.ChangePlayerState(PlayerState.IDLE);
                CCTVPanel.SetActive(true);
                //ShowCCTV();
            }
            else
            {
                _player.ChangePlayerState(PlayerState.MOVE);
                CCTVPanel.SetActive(false);
            }
        }
    }

    public void ShowCCTV()
    {
        PlayerCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerEnter = true;
            _player = other.GetComponent<PlayerMovement>();
            _controller = other.GetComponent<PlayerController>();
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