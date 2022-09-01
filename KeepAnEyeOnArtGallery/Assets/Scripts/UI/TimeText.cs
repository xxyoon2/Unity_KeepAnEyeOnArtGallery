using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    void Start()
    {
        _ui = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.CanUpdateTime.RemoveListener(UpdateText);
        GameManager.Instance.CanUpdateTime.AddListener(UpdateText);
    }

    void UpdateText(int hour, int minute)
    {
        if(hour >= 6)
        {
            GameManager.Instance.GameClear();
        }
        _ui.text = $"0{hour}:{minute}0 AM";
    }
}
