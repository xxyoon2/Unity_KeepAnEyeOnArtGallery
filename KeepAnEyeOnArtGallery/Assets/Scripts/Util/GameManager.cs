using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehavior<GameManager>
{



    // CCTV 관련
    public UnityEvent<int> ShowCamInfo = new UnityEvent<int>();

    // UI 관련
    public UnityEvent NotifyTextChange = new UnityEvent();

    // Undead 관련
    public UnityEvent<int> SpawnUndead = new UnityEvent<int>();


    public bool IsPlayerWatchingCCTV = false;

    private float _elapsedTime;
    private int _undeadCooltime = 40;
    
    
    public int SpawnRoom;
    private bool _startCountdown = false;
    private int _anomalyCooltime = 30;

    public GameObject UndeadPrefab;
    public GameObject[] UndeadSpawners;


    // 오브젝트 관련 - 움직임
    public UnityEvent<GameObject> ChangeObjectPosition = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> ChangeObjectRotation = new UnityEvent<GameObject>();
    //오브젝트 관련 - 고침
    public UnityEvent<GameObject> AnomalyFix = new UnityEvent<GameObject>();


    public GameObject MoveableObjects;
    private GameObject[] _rooms;
    private GameObject[] _objects;

    private _roomCount = 3;
    
    void Start()
    {
        // RoomA, RoomB, RoomC 들고옴
        _rooms = new GameObject[_roomCount];
        for (int i = 0; i < _roomCount; ++i)
        {
            _rooms[i] = MoveableObjects.transform.GetChild(i).gameObject;
        }

        /*
        int spawnerCount = UndeadSpawners.Length;
        Debug.Log($"{UndeadSpawners.Length}");
        for (int i = 0; i < spawnerCount; ++i)
        {
            GameObject undead = Instantiate<GameObject>(UndeadPrefab);
            UndeadSpawners[i].GetComponent<UndeadSpawner>().Init(undead);
            Debug.Log("우가!!!!");
        }
        */
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _anomalyCooltime)
        {
            _elapsedTime = 0f;
            UpdateAnomaly();
        }

        /*
        if (!_startCountdown && _elapsedTime > _anomalyCooltime)
        {
            _startCountdown = true;

            CanUpdateAnomaly.Invoke();
        }

        if (_startCountdown && _elapsedTime >= _undeadCooltime)
        {
            _elapsedTime = 0f;
            SpawnUndead.Invoke(SpawnRoom);
            _startCountdown = false;
        }
        */
    }

    private void UpdateAnomaly()
    {
        GameObject targetObj = SelectRandomObj();

        switch (Random.Range(0, 2))
        {
             case 0:
                ChangeObjectPosition.Invoke(targetObj);
                break;
            case 1:
                ChangeObjectRotation.Invoke(targetObj);
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


    public void UpdateRayTarget(GameObject target)
    {
        AnomalyFix.Invoke(target);
    }

    public void UpdateNotifyText()
    {
        NotifyTextChange.Invoke();
    }
    
    public void CameraIndexTest(int index)
    {
        ShowCamInfo.Invoke(index);
    }
    
    private void spawnUndead()
    {
        // TO DO : 만약 모든 스포너가 활성화 되어 있다면?

        int randomIndex = Random.Range(0, UndeadSpawners.Length);
        while (true)
        {
            if (UndeadSpawners[randomIndex].GetComponent<UndeadSpawner>().IsActive == false)
            {
                break;
            }

            randomIndex = Random.Range(0, UndeadSpawners.Length);
        }

        UndeadSpawners[randomIndex].GetComponent<UndeadSpawner>().Spawn();
    }

}
