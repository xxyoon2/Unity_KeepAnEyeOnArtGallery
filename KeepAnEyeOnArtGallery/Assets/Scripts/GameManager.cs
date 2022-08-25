using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehavior<GameManager>
{
    public UnityEvent CanUpdateAnomaly = new UnityEvent();
    private float _elapsedTime;
    private int _anomalyCooltime = 10;

    void Start()
    {
        
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
