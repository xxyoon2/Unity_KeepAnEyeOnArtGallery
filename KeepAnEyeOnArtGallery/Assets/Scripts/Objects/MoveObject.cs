using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : MonoBehaviour
{
    private GameObject[] _moveableObjects;
    private int _roomCount = 3;
    int _changeCount = 0;

    void Awake()
    {
        _moveableObjects = new GameObject[_roomCount];
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
        _changeCount++;
        //Debug.Log($"{_moveableObjects[childNum].name} �� {moveableObjectsInThisRoom}�� ������Ʈ �߿��� ��������. ���� ������� ��:{_changeCount}");
        int indexNum = Random.Range(0, moveableObjectsInThisRoom);
        ChangeObject(childNum, indexNum);
    }

    private void ChangeObject(int childNum, int indexNum)
    {

    }
}
