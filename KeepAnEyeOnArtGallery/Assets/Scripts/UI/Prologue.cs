using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    private List<DialogueRecord> _dialogueRecords;
    private int _currentDialogueIndex = 1;
    private bool _isPrologueOver = false;

    void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
        _isPrologueOver = false;
    }

    private void Start()
    {
        _dialogueRecords = CSVParser.GetDialogueRecords();
        _ui.text = _dialogueRecords[_currentDialogueIndex].Text;
    }

    void Update()
    {
        if(_isPrologueOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene("InGame");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ++_currentDialogueIndex;
            if( _currentDialogueIndex >= _dialogueRecords.Count )
            { 
                _ui.text = "tap Space to start / tap Esc to Quit";
                _isPrologueOver = true;
            }
            _ui.text = _dialogueRecords[_currentDialogueIndex].Text;
        }
    }
    /*
      IEnumerator PrologueDialogue()
     {
         int i = 0;
         while (i < _dialogs.Length)
         {
             _ui.text = _dialogs[i];
             if (Input.GetKey(KeyCode.Space))
             {
                 yield return new WaitForSeconds(0.1f);
                 i++;
             }
         }
         _ui.text = "Press spacebar to start game...";
     }
     */
}
