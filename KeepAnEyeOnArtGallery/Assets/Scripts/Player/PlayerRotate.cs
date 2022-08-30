using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private PlayerController _controller;
    private PlayerMovement _movement;


    private GameObject _hitObject;
    private GameObject _prevHitObject;

    public GameObject FixNotifyText;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        hit = new RaycastHit();
        _controller = GetComponent<PlayerController>();
        _movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {   
        Debug.Log($"{_movement.WhatStats()}");
        if (_movement.WhatStats() == PlayerState.MOVE)
        {
            ShotRay();
        }
    }

    private void ShotRay()
    {
        _prevHitObject = _hitObject;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            _hitObject = hit.transform.gameObject;

            if (_hitObject.tag == "InteractObject")
            {
                _hitObject.GetComponent<Outline>().enabled = true;
                FixNotifyText.SetActive(true);
            }
            else
            {
                FixNotifyText.SetActive(false);
            }
        }

        if (_prevHitObject != null && _prevHitObject != _hitObject)
        {
            if (_prevHitObject.tag == "InteractObject")
            {
                _prevHitObject.GetComponent<Outline>().enabled = false;
            }
        }

        if (_controller.CanInteract)
        {
            StartCoroutine(FixingState());
        }
    }

    IEnumerator FixingState()
    {
        _movement.ChangePlayerState(PlayerState.IDLE);
        GameManager.Instance.UpdateNotifyText();

        yield return new WaitForSeconds(3f);

        _movement.ChangePlayerState(PlayerState.MOVE);
        GameManager.Instance.UpdateRayTarget(hit.transform.gameObject);
        GameManager.Instance.UpdateNotifyText();
    }
}
