using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixObject : MonoBehaviour
{
    private GameObject _target;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        GameManager.Instance.AnomalyFix.RemoveListener(FindTargetInList);
        GameManager.Instance.AnomalyFix.AddListener(FindTargetInList);
    }

    private void FindTargetInList(GameObject target, int index)
    {
        _target = target;

        switch (GameManager.Instance.Objects[index].ModifiedOption)
        {
            case 0 :
                FixPosition();
                break;
            case 1 :
                FixRotation();
                break;
            default :
                break;
        }

        _audioSource.Play();
    }

    private void FixPosition()
    {
        Vector3 fixedPos = _target.transform.position;
        fixedPos.y -= 1;
        _target.transform.position = fixedPos;
    }

    private void FixRotation()
    {
        _target.transform.rotation *= Quaternion.Euler(0, 0, -20);
    }
}
