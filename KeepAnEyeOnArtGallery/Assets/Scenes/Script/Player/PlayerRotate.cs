using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    float rotationX;
    float rotationY;
    public float rotSpeed = 400f;

    void Start()
    {
        
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        rotationX += rotSpeed * y * Time.deltaTime;
        rotationY += rotSpeed * x * Time.deltaTime;

        //rotationY = transform.eulerAngles.y + rotationY;
        //rotationX += rotationX;

        rotationX = Mathf.Clamp(rotationX, -30f, 30f);

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0f);
    }
}
