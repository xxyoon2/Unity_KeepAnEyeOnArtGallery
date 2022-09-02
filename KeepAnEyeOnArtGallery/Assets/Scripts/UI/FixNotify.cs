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
                StartCoroutine(UpdateFixingState());
                //_isFixing = true;
                break;
            case "FixCompleted":
                _ui.text = "Completed anomaly fix";
                break;
            case "FixFailed":
                _ui.text = "There is nothing to fix on this object...";
                break;


        }

        IEnumerator UpdateFixingState()
        {
            _ui.text = "Fixing";
            yield return new WaitForSeconds(1f);
            _ui.text = "Fixing.";
            yield return new WaitForSeconds(1f);
            _ui.text = "Fixing..";
            yield return new WaitForSeconds(0.5f);
            _ui.text = "Fixing...";

            yield break;

        }
    }


}
