using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : MonoBehaviour
{
    public List<GameObject> ModifiedObjectsPos = new List<GameObject>();
    public List<GameObject> ModifiedObjectsRot = new List<GameObject>();

    private GameObject[] _moveableObjects;
    private int _roomCount = 3;
    public int ChangeCount = 0;
    private int _overlapCount = 0;

    void Awake()
    {
        _moveableObjects = new GameObject[_roomCount];
        for (int i = 0; i < _roomCount; ++i)
        {
            _moveableObjects[i] = transform.GetChild(i).gameObject;
        }
        GameManager.Instance.CanUpdateAnomaly.RemoveListener(UpdateAnomaly);
        GameManager.Instance.CanUpdateAnomaly.AddListener(UpdateAnomaly);
    }

    void Update()
    {
        
    }

    private GameObject SelectRandomObj()
    {
        GameObject result = null;
        while(result == null || ModifiedObjectsPos.Exists(x => x == result) || ModifiedObjectsRot.Exists(x => x == result))
        {
            Debug.Log($"{_overlapCount}");
            int childNum = Random.Range(0, 100) % 3;
            int moveableObjectsInThisRoom = _moveableObjects[childNum].transform.childCount;
            int indexNum = Random.Range(0, moveableObjectsInThisRoom);
            result = _moveableObjects[childNum].transform.GetChild(indexNum).gameObject;
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

    private void ChangeObjectPosition(GameObject targetObj)
    {
        Vector3 changedPos = targetObj.transform.position;
        changedPos.y += 1;
        targetObj.transform.position = changedPos;
        ModifiedObjectsPos.Add(targetObj);
        ChangeCount++;
        string last = ModifiedObjectsPos[ModifiedObjectsPos.Count - 1].name;
        Debug.Log($"위치 리스트에 들어감: {last}");
    }

    private void ChangeObjectRotation(GameObject targetObj)
    {
        targetObj.transform.rotation *= Quaternion.Euler(0, 0, 20);
        ModifiedObjectsRot.Add(targetObj);
        ChangeCount++;
        string last = ModifiedObjectsRot[ModifiedObjectsRot.Count - 1].name;
        Debug.Log($"회전 리스트에 들어감: {last}");
    }
}
