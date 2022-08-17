using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerRotate _playerRotate;
    private PlayerMovement _playerMovement;

    public GameObject PlayerCamera;

    public float speed = 5f;
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        _playerMovement = GetComponent<PlayerMovement>();    
    }

    void Update()
    {
        UpdateMove();
    }

    private void UpdateRotate()
    {

    }

    private void UpdateMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * x + Vector3.forward * z;

        dir = PlayerCamera.transform.TransformDirection(dir);
        dir.y = 0;

        dir.Normalize();

        transform.position += dir * speed * Time.deltaTime;
        //_playerMovement.MoveTo(new Vector3(x, 0, z));
    }
}
