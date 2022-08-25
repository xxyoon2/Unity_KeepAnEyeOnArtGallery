using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : MonoBehaviour
{
    public List<GameObject> ModifiedObjectsPos = new List<GameObject>();
    public List<GameObject> ModifiedObjectsRot = new List<GameObject>();

    private GameObject[] _moveableObjects;
    private int _roomCount = 3;
    private int _changeCount = 0;

    void Awake()
    {
        _moveableObjects = new GameObject[_roomCount];
        for (int i = 0; i < _roomCount; ++i)
        {
            _moveableObjects[i] = transform.GetChild(i).gameObject;
        }
        GameManager.Instance.CanUpdateAnomaly.RemoveListener(UpdateAnomaly);
        GameManager.Instance.CanUpdateAnomaly.AddListener(UpdateAnomaly);
        GameManager.Instance.AnomalyFix.RemoveListener(FindTargetInList);
        GameManager.Instance.AnomalyFix.AddListener(FindTargetInList);
    }

    void Update()
    {
        
    }

    private void UpdateAnomaly()
    {
        int childNum = Random.Range(0, 100) % 3;
        int moveableObjectsInThisRoom = _moveableObjects[childNum].transform.childCount;
        int indexNum =Random.Range(0, moveableObjectsInThisRoom);

        Debug.Log($"{_moveableObjects[childNum].name} 의 {moveableObjectsInThisRoom}개 오브젝트 중{_moveableObjects[childNum].transform.GetChild(indexNum).gameObject.name}변경.");

        GameObject targetObj = _moveableObjects[childNum].transform.GetChild(indexNum).gameObject;

        switch (Random.Range(0, 2))
        {
             case 0:
                ChangeObjectPosition(targetObj);
                break;
            case 1:
                ChangeObjectRotation(targetObj);
                break;
        }
    }

    private void ChangeObjectPosition(GameObject targetObj)
    {
        Vector3 changedPos = targetObj.transform.position;
        changedPos.y += 1;
        targetObj.transform.position = changedPos;
        ModifiedObjectsPos.Add(targetObj);
        _changeCount++;
        string last = ModifiedObjectsPos[ModifiedObjectsPos.Count - 1].name;
        Debug.Log($"위치 리스트에 들어감: {last}");
    }

    private void ChangeObjectRotation(GameObject targetObj)
    {
        Quaternion changedRot = targetObj.transform.rotation;
        changedRot.z += 80f;
        targetObj.transform.rotation = changedRot;
        ModifiedObjectsRot.Add(targetObj);
        _changeCount++;
        string last = ModifiedObjectsRot[ModifiedObjectsRot.Count - 1].name;
        Debug.Log($"회전 리스트에 들어감: {last}");
    }

    private void FindTargetInList(GameObject hitObj)
    {
        for(int i = ModifiedObjectsPos.Count - 1; i >= 0; i--)
        {
            if (ModifiedObjectsPos[i].name == hitObj.name)
            {
                ModifiedObjectsPos.Remove(hitObj);
                Vector3 fixedPos = hitObj.transform.position;
                fixedPos.y -= 1;
                hitObj.transform.position = fixedPos;
                _changeCount--;
                Debug.Log($"{hitObj.name}수정완");
                return;
            }
        }
        for (int j = ModifiedObjectsRot.Count - 1; j >= 0; j--)
        {
            if (ModifiedObjectsRot[j].name == hitObj.name)
            {
                Debug.Log("꺅!");
                ModifiedObjectsRot.Remove(hitObj);
                Quaternion changedRot = hitObj.transform.rotation;
                changedRot.z -= 80f;
                hitObj.transform.rotation = changedRot;
                _changeCount--;
                Debug.Log($"{hitObj.name}수정완");
                return;
            }
        }
        Debug.Log($"{hitObj.name} 작품은 변한 점이 없습니다.");
    }
}
