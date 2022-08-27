using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    private int _hour = 0;
    private int _minute = 0;
    void Start()
    {
        _ui = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.TimeChange.RemoveListener(UpdateText);
        GameManager.Instance.TimeChange.AddListener(UpdateText);
    }

    void UpdateText()
    {
        _minute++;
        if(_minute >= 6)
        {
            _minute = 0;
            _hour++;
        }
        _ui.text = $"0{_hour}:{_minute}0 AM";
    }
}
