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
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                //Debug.Log($"{hit.transform.gameObject}");
                GameManager.Instance.UpdateRayTarget(hit.transform.gameObject);

            }
        }
    }
}
