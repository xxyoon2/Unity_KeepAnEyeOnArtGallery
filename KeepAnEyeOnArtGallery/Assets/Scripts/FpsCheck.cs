using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsCheck : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    private void Start()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }
    void UpdateText()
    {
        float fps = 1.0f / Time.deltaTime;
        _ui.text = $"Fps: {fps}";
    }

}