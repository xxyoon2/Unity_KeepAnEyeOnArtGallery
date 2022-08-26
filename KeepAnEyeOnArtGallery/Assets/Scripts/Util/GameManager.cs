using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehavior<GameManager>
{
    public UnityEvent CanUpdateAnomaly = new UnityEvent();
    public UnityEvent<GameObject> AnomalyFix = new UnityEvent<GameObject>();
    public UnityEvent<int> ShowCamInfo = new UnityEvent<int>();

    public bool IsPlayerWatchingCCTV = false;

    private float _elapsedTime;
    private int _anomalyCooltime = 10;

    public void UpdateRayTarget(GameObject target)
    {
        AnomalyFix.Invoke(target);
    }
    public void CameraIndexTest(int index)
    {
        Debug.Log("Ä«¸Þ¶ó ¤¡¤¡");
        ShowCamInfo.Invoke(index);
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
