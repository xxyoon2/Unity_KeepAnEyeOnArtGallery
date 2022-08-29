using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixObject : MonoBehaviour
{
    private MoveObject _move;
    void Start()
    {
        _move = GetComponent<MoveObject>();
        GameManager.Instance.AnomalyFix.RemoveListener(FindTargetInList);
        GameManager.Instance.AnomalyFix.AddListener(FindTargetInList);
    }

    private void FindTargetInList(GameObject hitObj)
    {
        for (int i = _move.ModifiedObjectsPos.Count - 1; i >= 0; i--)
        {
            if (_move.ModifiedObjectsPos[i].name == hitObj.name)
            {
                _move.ModifiedObjectsPos.Remove(hitObj);
                Vector3 fixedPos = hitObj.transform.position;
                fixedPos.y -= 1;
                hitObj.transform.position = fixedPos;
                _move.ChangeCount--;
                Debug.Log($"{hitObj.name}수정완");
                return;
            }
        }
        for (int j = _move.ModifiedObjectsRot.Count - 1; j >= 0; j--)
        {
            if (_move.ModifiedObjectsRot[j].name == hitObj.name)
            {
                Debug.Log("꺅!");
                _move.ModifiedObjectsRot.Remove(hitObj);
                hitObj.transform.rotation *= Quaternion.Euler(0, 0, -20);
                _move.ChangeCount--;
                Debug.Log($"{hitObj.name}수정완");
                return;
            }
        }
        Debug.Log($"{hitObj.name} 작품은 변한 점이 없습니다.");
    }
}
