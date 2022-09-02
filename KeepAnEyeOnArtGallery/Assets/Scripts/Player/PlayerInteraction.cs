using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
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

            if (_hitObject.tag == "InteractObject" || _hitObject.tag == "FixableObject")
            {
                if(_hitObject.GetComponent<Outline>() != null)
                {
                    _hitObject.GetComponent<Outline>().enabled = true;
                }
                FixNotifyText.SetActive(true);
                //GameManager.Instance.UpdateNotifyText(_hitObject.tag);
            }
            else
            {
                FixNotifyText.SetActive(false);
            }
        }

        if (_prevHitObject != null && _prevHitObject != _hitObject)
        {
            if (_prevHitObject.tag == "InteractObject" || _prevHitObject.tag == "FixableObject")
            {
                if (_prevHitObject.GetComponent<Outline>() != null)
                {
                    _prevHitObject.GetComponent<Outline>().enabled = false;
                }
            }
            GameManager.Instance.UpdateNotifyText(_hitObject.tag);
        }

        if (_controller.CanInteract && _hitObject.tag == "FixableObject")
        {
            StartCoroutine(FixingState());
        }
    }

    IEnumerator FixingState()
    {
        _movement.ChangePlayerState(PlayerState.IDLE);
        GameManager.Instance.UpdateNotifyText("Fixing");

        yield return new WaitForSeconds(3f);

        _movement.ChangePlayerState(PlayerState.MOVE);
        GameManager.Instance.UpdateRayTarget(hit.transform.gameObject);
    }
}
