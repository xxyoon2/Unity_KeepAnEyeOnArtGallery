using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    private List<DialogueRecord> _dialogueRecords;
    private AudioSource _audio;

    private int _currentDialogueIndex = 1;
    private bool _isPrologueOver = false;

    void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
        _audio = GetComponent<AudioSource>();

        _isPrologueOver = false;
    }

    private void Start()
    {
        _dialogueRecords = CSVParser.GetDialogueRecords();
        _ui.text = _dialogueRecords[_currentDialogueIndex].Text;

        _audio.Play();
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
}
