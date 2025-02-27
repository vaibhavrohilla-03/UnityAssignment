using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile; 
    private List<int> extractedData = new List<int>(); 

    void Start()
    {
        if (csvFile != null)
        {
            extractedData = ReadCSV(csvFile.text);
            Debug.Log("CSV Data Extracted: " + string.Join(", ", extractedData));
        }
        else
        {
            Debug.LogError("CSV file not assigned in the inspector");
        }
    }

    List<int> ReadCSV(string csvText)
    {
        List<int> csvData = new List<int>();
        string[] lines = csvText.Split('\n');

        foreach (string line in lines)
        {
            string[] values = line.Split(','); 
            foreach (string value in values)
            {
                if (int.TryParse(value.Trim(), out int number)) 
                {
                    csvData.Add(number);
                }
            }
        }
        return csvData;
    }

    public Dictionary<string, int> GetCSVData()
    {
        Dictionary<string, int> data = new Dictionary<string, int>();

         
        string[] lines = csvFile.text.Split('\n');

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[1], out int value))
            {
                data[parts[0].Trim()] = value;
            }
        }

        return data;
    }
}
