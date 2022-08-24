using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : MonoBehaviour
{
    public GameObject FixCheckerPrefab;
    public int FixCheckerMaxCount = 4;
    private GameObject[] _fixCheckers;
    private GameObject[] _moveableObjects;
    private int _roomCount = 3;
    int _changeCount = 0;

    void Awake()
    {
        _moveableObjects = new GameObject[_roomCount];
        _fixCheckers = new GameObject[FixCheckerMaxCount];
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
        Debug.Log($"{_moveableObjects[childNum].name} 의 {moveableObjectsInThisRoom}개 오브젝트 중{_moveableObjects[childNum].transform.GetChild(indexNum).gameObject.name}변경.");
        ChangeObject(childNum, indexNum);
    }

    private void ChangeObject(int childNum, int indexNum)
    {
        GameObject asdf = _moveableObjects[childNum].transform.GetChild(indexNum).gameObject;
        Vector3 changedPos = asdf.transform.position;
        changedPos.y += 3;
        asdf.transform.position = changedPos;
        _fixCheckers[_changeCount] = Instantiate(FixCheckerPrefab);
        _fixCheckers[_changeCount].transform.SetParent(_moveableObjects[childNum].transform, true);
        _changeCount++;
        Debug.Log($"현재 변경 사항: {_changeCount}");
    }
}
