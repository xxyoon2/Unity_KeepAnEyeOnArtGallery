using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float _elapsedTime;

    void Start()
    {
        
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if( _elapsedTime > 20f)
        {
            _elapsedTime = 0f;
            Debug.Log("20초 지남. 변경사항 생김");
        }
    }
}
