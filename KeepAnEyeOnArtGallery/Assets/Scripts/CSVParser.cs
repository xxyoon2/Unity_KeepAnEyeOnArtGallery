using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using TMPro;

public class DialogueRecord
{
    public int Index { get; set; }
    public string Text { get; set; }
}


public static class CSVParser
{
    public static List<DialogueRecord> GetDialogueRecords()
    {
        TextAsset dialogTextAsset = Resources.Load<TextAsset>("CSV/tutorial");

        List<DialogueRecord> dialogRecords = new List<DialogueRecord>();
        dialogRecords.Add(null);  // HACK : 인덱스를 정렬하기 위한 임시 데이터

        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|",
            NewLine = Environment.NewLine
        };

        using (StringReader csvString = new StringReader(dialogTextAsset.text))
        {
            using (CsvReader csv = new CsvReader(csvString, config))
            {
                IEnumerable<DialogueRecord> records = csv.GetRecords<DialogueRecord>();
                foreach (DialogueRecord record in records)
                {
                    dialogRecords.Add(record);
                }
            }
        }

        return dialogRecords;
    }
}
