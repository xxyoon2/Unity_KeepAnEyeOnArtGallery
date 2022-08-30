using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct MoveableObject
{
    public GameObject Name;
    public bool IsActive;
    public int RoomNum;
    public bool IsUndeadLive;
}

public class GameManager : SingletonBehavior<GameManager>
{
    // 오브젝트 변화 주는 이벤트
    public UnityEvent<GameObject> ChangeObjectPosition = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> ChangeObjectRotation = new UnityEvent<GameObject>();
    
    // 오브젝트 고치는 이벤트
    public UnityEvent<GameObject> AnomalyFix = new UnityEvent<GameObject>();

    // 오브젝트 관리 관련
    public GameObject Showrooms;
    public MoveableObject[] Objects = new MoveableObject[18];

    public int ObjectTotalCount = 0;    // 오브젝트 개수
    public int ActiveObjectCount = 0;   // 활성화된 오브젝트 개수

    void Start()
    {
        // 오브젝트 배열 생성
        int roomCount = Showrooms.transform.childCount;
        for (int i = 0; i < roomCount; ++i)
        {
            GameObject room = Showrooms.transform.GetChild(i).gameObject;
            
            int objectCount = room.transform.childCount;
            for (int j = ObjectTotalCount; j < ObjectTotalCount + objectCount; ++j)
            {
                GameObject obj = room.transform.GetChild(j - ObjectTotalCount).gameObject;
                Objects[j].Name = obj;
                Objects[j].IsActive = false;
                Objects[j].RoomNum = i;
                Objects[j].IsUndeadLive = false;
                Debug.Log($"{Objects[j].Name} {j}번째원소로 들어감");
            }

            ObjectTotalCount += objectCount;

        }

        // 언데드 생성
        for (int i = 0; i < ObjectTotalCount; ++i)
        {
            GameObject undead = Instantiate<GameObject>(UndeadPrefab);
            UndeadSpawners[i].GetComponent<UndeadSpawner>().Init(undead);
        }
    }

    private float _elapsedTime;
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        // 시간이 업데이트 될 때마다 오브젝트 변화사항도 하나씩 추가됨
        if (_elapsedTime >= _anomalyCooltime)
        {
            _elapsedTime = 0f;
            UpdateAnomaly();

            // 시간 업데이트
            CanUpdateAnomaly.Invoke();
        }

        /*
        if (!_startCountdown && _elapsedTime > _anomalyCooltime)
        {
            _startCountdown = true;
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
        int targetObj = SelectRandomObj();

        switch (Random.Range(0, 2))
        {
             case 0:
                ChangeObjectPosition.Invoke(Objects[targetObj].Name);
                break;
            case 1:
                ChangeObjectRotation.Invoke(Objects[targetObj].Name);
                break;
        }
    }

    private int SelectRandomObj()
    {
        int result = -1;
        while(result == -1)
        {
            int randomObject = Random.Range(0, ObjectTotalCount);   // 랜덤 오브젝트 설정
            Debug.Log($"{randomObject}");
            if (Objects[randomObject].IsActive == false)
            {
                Objects[randomObject].IsActive = true;
                result = randomObject;

                ++ActiveObjectCount;
            }
        }
        Debug.Log($"{Objects[result].Name}변경. {ActiveObjectCount}개의 오브젝트 활성화 됨");

        return result;
    }

    // CCTV 관련
    public UnityEvent<int> ShowCamInfo = new UnityEvent<int>();

    public void CameraIndexTest(int index)
    {
        ShowCamInfo.Invoke(index);
    }
   





    // UI 관련
    public UnityEvent CanUpdateAnomaly = new UnityEvent();
    public UnityEvent NotifyTextChange = new UnityEvent();

    // Undead 관련
    public UnityEvent<int> SpawnUndead = new UnityEvent<int>();


    public bool IsPlayerWatchingCCTV = false;

    //private int _undeadCooltime = 40;
    
    //private bool _startCountdown = false;
    private int _anomalyCooltime = 5;

    public GameObject UndeadPrefab;
    public GameObject[] UndeadSpawners;


    // 오브젝트 관련 - 움직임
    

    public void UpdateRayTarget(GameObject target)
    {
        AnomalyFix.Invoke(target);
    }

    public void UpdateNotifyText()
    {
        NotifyTextChange.Invoke();
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
