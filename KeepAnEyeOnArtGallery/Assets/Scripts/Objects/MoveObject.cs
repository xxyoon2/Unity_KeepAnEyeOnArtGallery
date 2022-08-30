using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : MonoBehaviour
{
    public List<GameObject> ModifiedObjectsPos = new List<GameObject>();
    public List<GameObject> ModifiedObjectsRot = new List<GameObject>();

    private GameObject[] _moveableObjects;
    public int ChangeCount = 0;

    void Awake()
    {
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
        ModifiedObjectsPos.Add(targetObj);
        GameManager.Instance.ActiveObjectCount++;
        string last = ModifiedObjectsPos[ModifiedObjectsPos.Count - 1].name;
        Debug.Log($"위치 리스트에 들어감: {last}");
    }

    private void ChangeRotation(GameObject targetObj)
    {
        targetObj.transform.rotation *= Quaternion.Euler(0, 0, 20);
        ModifiedObjectsRot.Add(targetObj);
        GameManager.Instance.ActiveObjectCount++;
        string last = ModifiedObjectsRot[ModifiedObjectsRot.Count - 1].name;
        Debug.Log($"회전 리스트에 들어감: {last}");
    }

    
    /*
    void Awake()
    {
        // RoomA, RoomB, RoomC 오브젝트를 받아옴
        _moveableObjects = new GameObject[_roomCount];
        for (int i = 0; i < _roomCount; ++i)
        {
            _moveableObjects[i] = transform.GetChild(i).gameObject;
        }
        GameManager.Instance.CanUpdateAnomaly.RemoveListener(UpdateAnomaly);
        GameManager.Instance.CanUpdateAnomaly.AddListener(UpdateAnomaly);
    }

    private void UpdateAnomaly()
    {

        GameObject targetObj = SelectRandomObj();

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

    private GameObject SelectRandomObj()
    {
        GameObject result = null;
        while(result == null || ModifiedObjectsPos.Exists(x => x == result) || ModifiedObjectsRot.Exists(x => x == result))
        {
            int roomNum = Random.Range(0, 100) % 3; // 방 랜덤 뽑기
            int moveableObjectsInThisRoom = _moveableObjects[roomNum].transform.childCount; // 랜덤으로 뽑은 방의 자식오브젝트 숫자
            int indexNum = Random.Range(0, moveableObjectsInThisRoom);
            
            GameManager.Instance.SpawnRoom = roomNum;
            
            result = _moveableObjects[roomNum].transform.GetChild(indexNum).gameObject;

            _overlapCount++;
            if(_overlapCount >= 10)
            {
                _overlapCount = 0;
                return result;
            }
        }
        Debug.Log($"{result.name}변경.");

        return result;
    }

    */

}
