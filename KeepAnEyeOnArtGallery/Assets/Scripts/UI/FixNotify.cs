using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FixNotify : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    void Start()
    {
        _ui = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.NotifyTextChange.AddListener(UpdateText);
    }
    void UpdateText(string AimTextInfo)
    {
        switch(AimTextInfo)
        {
            case "InteractObject":
                _ui.text = "Tap E To Interaction";
                break;
            case "FixableObject":
                _ui.text = "Tap Space To Fix";
                break;
            case "Fixing":
                _ui.text = "Fixing...";
                break;
            case "FixCompleted":
                _ui.text = "Completed anomaly fix";
                break;
            case "FixFailed":
                _ui.text = "There is nothing to fix on this object...";
                break;


        }

    }


}
