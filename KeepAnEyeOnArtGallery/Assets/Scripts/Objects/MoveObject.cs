using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.ChangeObjectPosition.RemoveListener(ChangePosition);
        GameManager.Instance.ChangeObjectPosition.AddListener(ChangePosition);

        GameManager.Instance.ChangeObjectRotation.RemoveListener(ChangeRotation);
        GameManager.Instance.ChangeObjectRotation.AddListener(ChangeRotation);
    }

    private void ChangePosition(GameObject targetObj)
    {
        Vector3 changedPos = targetObj.transform.position;
        changedPos.y += 1;
        targetObj.transform.position = changedPos;
    }

    private void ChangeRotation(GameObject targetObj)
    {
        targetObj.transform.rotation *= Quaternion.Euler(0, 0, 20);
    }
}
