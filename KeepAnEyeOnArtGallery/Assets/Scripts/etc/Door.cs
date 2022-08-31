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
        if (_isPlayerHere)
        {
            if (Player.GetComponent<PlayerController>().CanCCTVOn)
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
