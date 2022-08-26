using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public GameObject UndeadEye;
    public bool isPlayerInHere = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInHere = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInHere = false;
        }
    }
}
