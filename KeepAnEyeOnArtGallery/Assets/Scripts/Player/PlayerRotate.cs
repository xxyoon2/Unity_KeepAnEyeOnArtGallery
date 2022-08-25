using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    float rotationX;
    float rotationY;
    public float rotSpeed = 200f;

    private PlayerController _controller;

    RaycastHit hit;
    Ray ray;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        hit = new RaycastHit();

        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        rotationX += rotSpeed * y * Time.deltaTime;
        rotationY += rotSpeed * x * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -30f, 30f);

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0f);

        if (_controller.CanInteract)
        {
            /*
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Debug.DrawRay(ray.origin, RaycastHit.point, Color green);
                Debug.Log($"{hit.transform.gameObject}");
            }
            */
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Debug.DrawRay(ray.origin, hit.point, Color.green);
                Debug.Log($"{hit.transform.gameObject}");
            }
            
        }

        
    }
}
