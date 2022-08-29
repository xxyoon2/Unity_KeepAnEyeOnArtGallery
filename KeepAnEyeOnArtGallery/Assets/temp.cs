using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using TMPro;

public class TestCsvRecord
{
    public string text { get; set; }
}


public class temp : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    private string[] _dialogs;
    void Start()
    {
        TextAsset tempTextAsset = Resources.Load<TextAsset>("CSV/test");
        _ui = GetComponent<TextMeshProUGUI>();
        _dialogs = new string[6];

        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|",
            NewLine = Environment.NewLine
        };

        using (StringReader csvString = new StringReader(tempTextAsset.text))
        {
            using (CsvReader csv = new CsvReader(csvString, config))
            {
                IEnumerable<TestCsvRecord> records = csv.GetRecords<TestCsvRecord>();
                int i = 0;
                foreach (TestCsvRecord record in records)
                {
                    Debug.Log($"{record.text}");
                    _dialogs[i] = record.text;
                    i++;
                }
            }
        }
        StartCoroutine(PrologueDialogue());
    }

    IEnumerator PrologueDialogue()
    {
        int i = 0;
        while(i < _dialogs.Length)
        {
            _ui.text = _dialogs[i];
            if (Input.GetKey(KeyCode.Space)) 
            { 
                yield return new WaitForSeconds(0.1f);
                i++; 
            }
            yield return new WaitForSeconds(0.1f);
        }
        _ui.text = "Press spacebar to start game...";
    }

}
