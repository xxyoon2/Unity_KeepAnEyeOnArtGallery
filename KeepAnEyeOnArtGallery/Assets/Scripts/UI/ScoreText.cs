using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _ui;

    private void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        int _hour = GameManager.Instance.Hour;
        int _minute = GameManager.Instance.Minute;
        int _anomalyCount = GameManager.Instance.ActiveObjectCount;
        int _fixedAnomalyCount = GameManager.Instance.FixObjCount;
        _ui.text = $"Your Survived Time: 0{_hour}:{_minute}0 \nFixed Anomaly Count: {_fixedAnomalyCount} \nUnresolved Anomaly: {_anomalyCount}";
    }

}
