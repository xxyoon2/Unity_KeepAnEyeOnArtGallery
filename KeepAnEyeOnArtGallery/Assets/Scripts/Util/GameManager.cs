using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehavior<GameManager>
{
    // 오브젝트 관련
    public UnityEvent CanUpdateAnomaly = new UnityEvent();
    public UnityEvent<GameObject> AnomalyFix = new UnityEvent<GameObject>();

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

    void Start()
    {
        int spawnerCount = UndeadSpawners.Length;
        Debug.Log($"{UndeadSpawners.Length}");
        for (int i = 0; i < spawnerCount; ++i)
        {
            GameObject undead = Instantiate<GameObject>(UndeadPrefab);
            UndeadSpawners[i].GetComponent<UndeadSpawner>().Init(undead);
            Debug.Log("우가!!!!");
        }
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
    

    void Update()
    {
        _elapsedTime += Time.deltaTime;

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
