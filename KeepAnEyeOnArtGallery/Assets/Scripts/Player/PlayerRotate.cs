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

    private GameObject _hitObject;
    private GameObject _prevHitObject;
    Ray ray;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        hit = new RaycastHit();

        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {   
        _prevHitObject = _hitObject;

        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        rotationX += rotSpeed * y * Time.deltaTime;
        rotationY += rotSpeed * x * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -30f, 30f);

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0f);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);

        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            _hitObject = hit.transform.gameObject;

            if (_hitObject.tag == "InteractObject")
            {
                _hitObject.GetComponent<Outline>().enabled = true;
            }
        }

        if (_hitObject != null && _hitObject != _prevHitObject)
        {
            if (_prevHitObject.tag == "InteractObject")
            {
                _prevHitObject.GetComponent<Outline>().enabled = false;
            }
        }
        
        //Debug.Log($"{_hitObject} {_prevHitObject}");

        if (_controller.CanInteract)
        {
            GameManager.Instance.UpdateRayTarget(hit.transform.gameObject);
        }
    }
}
