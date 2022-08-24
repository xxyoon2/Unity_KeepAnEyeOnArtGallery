using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    float rotationX;
    float rotationY;
    public float rotSpeed = 200f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        rotationX += rotSpeed * y * Time.deltaTime;
        rotationY += rotSpeed * x * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -30f, 30f);

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0f);
    }
}
