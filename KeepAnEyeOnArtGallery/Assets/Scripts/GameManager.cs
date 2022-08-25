using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehavior<GameManager>
{
    public UnityEvent CanUpdateAnomaly = new UnityEvent();
    public UnityEvent<GameObject> AnomalyFix = new UnityEvent<GameObject>();
    private float _elapsedTime;
    private int _anomalyCooltime = 10;

    public void UpdateRayTarget(GameObject target)
    {
        AnomalyFix.Invoke(target);
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _anomalyCooltime)
        {
            _elapsedTime = 0f;
            CanUpdateAnomaly.Invoke();
        }
    }

}
