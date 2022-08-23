using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChangeScene : MonoBehaviour
{
    public TextMeshProUGUI pressKeyText;
    public void Update()
    {
        StartCoroutine(BlinkText());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("으아아아ㅏㅇ");
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator BlinkText()
    {
        while (true) 
        {
            pressKeyText.text = "";
            yield return new WaitForSeconds(.5f);
            pressKeyText.text = "press SpaceKey to start";
            yield return new WaitForSeconds(.5f);

        }
    }
}
