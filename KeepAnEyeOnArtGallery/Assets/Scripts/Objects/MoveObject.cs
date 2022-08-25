using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : MonoBehaviour
{
    public List<GameObject> ModifiedObjectsPos = new List<GameObject>();

    private GameObject[] _moveableObjects;
    private int _roomCount = 3;
    private int _changeCount = 0;

    void Awake()
    {
        _moveableObjects = new GameObject[_roomCount];
        //_fixCheckers = new GameObject[FixCheckerMaxCount];
        for (int i = 0; i < _roomCount; ++i)
        {
            _moveableObjects[i] = transform.GetChild(i).gameObject;
        }
        GameManager.Instance.CanUpdateAnomaly.AddListener(UpdateAnomaly);
    }

    void Update()
    {
        
    }

    private void UpdateAnomaly()
    {
        int childNum = Random.Range(0, 100) % 3;
        int moveableObjectsInThisRoom = _moveableObjects[childNum].transform.childCount;
        int indexNum = Random.Range(0, moveableObjectsInThisRoom);
        // Debug.Log($"{_moveableObjects[childNum].name} 의 {moveableObjectsInThisRoom}개 오브젝트 중{_moveableObjects[childNum].transform.GetChild(indexNum).gameObject.name}변경.");
        ChangeObjectPosition(childNum, indexNum);
    }

    private void ChangeObjectPosition(int childNum, int indexNum)
    {
        GameObject targetObj = _moveableObjects[childNum].transform.GetChild(indexNum).gameObject;
        Vector3 changedPos = targetObj.transform.position;
        changedPos.y += 2;
        targetObj.transform.position = changedPos;
        ModifiedObjectsPos.Add(targetObj);
        _changeCount++;
        //Debug.Log($"현재 변경 사항: {_changeCount}");
        //string last = ModifiedObjectsPos[ModifiedObjectsPos.Count - 1].name;
        //Debug.Log($"리스트에 들어감: {last}");
    }

    private void ChangeObjectRotation()
    {

    }
}
