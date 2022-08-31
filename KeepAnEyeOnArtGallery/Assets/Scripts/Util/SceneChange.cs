using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Prologue");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
