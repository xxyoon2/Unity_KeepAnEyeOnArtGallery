using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject[] DoorWarpPosition;
    public GameObject Player;
    private bool _isInside = true;
    private bool _isPlayerHere = false;

    void Start()
    {
        _isInside = true;
    }

    void Update()
    {
        Debug.Log($"{_isInside} {_isPlayerHere}");
        if (_isPlayerHere)
        {
            if (Player.GetComponent<PlayerController>().CanInteract)
            {
                _isInside = !_isInside;
                int index = Convert.ToInt32(_isInside);
                Player.transform.position = DoorWarpPosition[index].transform.position;
            }
        }
    }

    private void Warp()
    {
        _isInside = !_isInside;
        int index = Convert.ToInt32(_isInside);
        Player.transform.position = DoorWarpPosition[index].transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("왜 안돼?");
            _isPlayerHere = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerHere = false;
        }
    }
}
