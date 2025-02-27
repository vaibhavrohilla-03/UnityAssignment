using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarGraphController : MonoBehaviour
{
    [SerializeField] private BarGraph3D barGraph;

    public void PlotData(TextAsset csvFile)
    {
        if (barGraph == null)
        {
            Debug.LogError("Barnone");
            return;
        }

        if (csvFile == null)
        {
            Debug.LogError("CSVnull");
            return;
        }

        StartCoroutine(PlotDataCoroutine(csvFile));
    }

    private IEnumerator PlotDataCoroutine(TextAsset csvFile)
    {
        
        barGraph.ResetGraph();

     
        yield return new WaitForSeconds(barGraph.animationDuration);

        CSVReader csvReader = barGraph.GetComponent<CSVReader>();
        if (csvReader != null)
        {
            csvReader.csvFile = csvFile;
            Dictionary<string, int> newData = csvReader.GetCSVData();
            barGraph.ShowGraph(newData);
        }
        
    }
}
