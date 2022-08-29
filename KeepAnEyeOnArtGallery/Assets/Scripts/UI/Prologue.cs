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
    private bool _isPrologueEnd = false;

    void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _dialogueRecords = CSVParser.GetDialogueRecords();
        _ui.text = _dialogueRecords[_currentDialogueIndex].Text;
    }

    void Update()
    {
        if(_isPrologueEnd && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("InGame");
        }
        if (_isPrologueEnd && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ++_currentDialogueIndex;
            if( _currentDialogueIndex >= _dialogueRecords.Count )
            { 
                _ui.text = "tap E to Start Game / tap Esc to Title Scene";
                _isPrologueEnd = true;
                _currentDialogueIndex = _dialogueRecords.Count;
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
