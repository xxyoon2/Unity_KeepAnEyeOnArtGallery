using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraInfoText : MonoBehaviour
{
    TextMeshProUGUI _ui;
    void Start()
    {
        _ui = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.ShowCamInfo.AddListener(UpdateText);
    }

    void UpdateText(int index)
    {
        _ui.text = $"Camera {index}";
    }
}
