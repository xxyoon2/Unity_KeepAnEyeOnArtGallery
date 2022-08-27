using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FixNotify : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    private bool _isFixing = false;
    void Start()
    {
        _ui = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.NotifyTextChange.AddListener(UpdateText);
    }
    void UpdateText()
    {
        _isFixing = !_isFixing;
        if (_isFixing)
        {
            _ui.text = "Fixing...";
        }
        else
        {
            _ui.text = "Tap Space to Fix";
        }

    }


}
