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
    private int _anomalyCooltime = 10;

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
            //_elapsedTime = 0f;
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

}
