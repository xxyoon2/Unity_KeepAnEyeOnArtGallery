using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    public float moveSpeed = 5f;    // 이동 속도
    private Vector3 moveForce;  // 이동 힘

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        /*
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir =  Vector3.right * h + Vector3.forward * v;

        dir = Camera.main.transform.TransformDirection(dir);

        dir.Normalize();

        transform.position += dir * moveSpeed * Time.deltaTime;
        */

        characterController.Move(moveForce * Time.deltaTime);
    }

    public void MoveTo(Vector3 diraction)
    {
        //diraction = transform.rotation * new Vector3(diraction.x, 0, diraction.z);

        //moveForce = new Vector3(diraction.x * moveSpeed, moveForce.y, diraction.z * moveSpeed);
    }
}
