using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private PlayerController _controller;

    RaycastHit hit;

    private GameObject _hitObject;
    private GameObject _prevHitObject;
    Ray ray;

    void Start()
    {
        hit = new RaycastHit();
        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {   
        _prevHitObject = _hitObject;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);

        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            _hitObject = hit.transform.gameObject;

            if (_hitObject.tag == "InteractObject")
            {
                _hitObject.GetComponent<Outline>().enabled = true;
            }
        }

        if (_prevHitObject != null && _prevHitObject != _hitObject)
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
