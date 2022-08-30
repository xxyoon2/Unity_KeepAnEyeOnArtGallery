using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixObject : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.AnomalyFix.RemoveListener(FindTargetInList);
        GameManager.Instance.AnomalyFix.AddListener(FindTargetInList);
    }

    private void FindTargetInList(int index)
    {
        if (GameManager.Instance.Objects[index].ModifiedOption == 0)
        {
            Vector3 fixedPos = GameManager.Instance.Objects[index].Name.transform.position;
            fixedPos.y -= 1;
            GameManager.Instance.Objects[index].Name.transform.position = fixedPos;

            GameManager.Instance.ActiveObjectCount--;

            Debug.Log($"{GameManager.Instance.Objects[index].Name} 와우");

            return;
        }

        if (GameManager.Instance.Objects[index].ModifiedOption == 1)
        {
            GameManager.Instance.Objects[index].Name.transform.rotation *= Quaternion.Euler(0, 0, -20);

            GameManager.Instance.ActiveObjectCount--;
            
            Debug.Log($"{GameManager.Instance.Objects[index].Name} 웨우");

            return;
        }
    }

    /*
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
                GameManager.Instance.ActiveObjectCount++;
                Debug.Log($"{hitObj.name} 와우");
                return;
            }
        }

        for (int j = _move.ModifiedObjectsRot.Count - 1; j >= 0; j--)
        {
            if (_move.ModifiedObjectsRot[j].name == hitObj.name)
            {
                _move.ModifiedObjectsRot.Remove(hitObj);
                hitObj.transform.rotation *= Quaternion.Euler(0, 0, -20);
                GameManager.Instance.ActiveObjectCount++;
                Debug.Log($"{hitObj.name} 웨우");
                return;
            }
        }

        Debug.Log($"{hitObj.name} 안고쳐짐");
    }
    */
}
