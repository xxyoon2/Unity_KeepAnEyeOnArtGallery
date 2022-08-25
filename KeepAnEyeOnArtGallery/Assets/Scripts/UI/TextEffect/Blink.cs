using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Blink : MonoBehaviour
{
    private TextMeshProUGUI _infoText;
    private float _fadeInTime = 0f;
    void Start()
    {
        _infoText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkText());
    }
    private IEnumerator BlinkText()
    {
        while(true)
        {
            if(_fadeInTime >= 1f)
            {
                yield break;
            }
            _fadeInTime += 0.02f;
            _infoText.color = new Color(1, 1, 1, _fadeInTime);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
