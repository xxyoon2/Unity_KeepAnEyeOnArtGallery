using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour
{
    private float _glitchEffectTime;
    void Start()
    {
       // StartCoroutine(GlitchEffect());
    }

    IEnumerator GlitchEffect()
    {
        while(true)
        {
            _glitchEffectTime = Random.Range(0f, 3f);
            yield return new WaitForSeconds(_glitchEffectTime);
            Vector3 glitchPos = transform.position;
            glitchPos.x += 10;
            transform.position = glitchPos;
            yield return new WaitForSeconds(0.05f);
            glitchPos.x -= 10;
            transform.position = glitchPos;
        }
    }
}
