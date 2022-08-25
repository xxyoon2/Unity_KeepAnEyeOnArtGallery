using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehavior<GameManager>
{
    public UnityEvent CanUpdateAnomaly = new UnityEvent();
    public UnityEvent<string> AnomalyFix = new UnityEvent<string>();
    private float _elapsedTime;
    private int _anomalyCooltime = 20;

    private int _currentScore = 0;
    
    public void UpdateRayTarget(string target)
    {
        AnomalyFix.Invoke(target);
        Debug.Log($"{target}찾기 가보자고");
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _anomalyCooltime)
        {
            _elapsedTime = 0f;
            Debug.Log($"{_anomalyCooltime}초 지남. 변경사항 생김");
            CanUpdateAnomaly.Invoke();
        }
    }

}
