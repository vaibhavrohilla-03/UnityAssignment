using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarGraphController : MonoBehaviour
{
    public CSVReader csvReader;
     public BarGraph3D barGraph;
     public LineGraphController linegraphcontroller;

    public void PlotData(TextAsset csvFile)
    {
        //Debug.Log("plotdata called");
        //Debug.Log(csvFile.name);

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
        Debug.Log("StartedPlotroutine");
        barGraph.ResetGraph();
        linegraphcontroller.lineGraph.ResetGraph();
     
        yield return new WaitForSeconds(barGraph.animationDuration);

        
        if (csvReader != null)
        {
            csvReader.csvFile = csvFile;
            Dictionary<string, int> newData = csvReader.GetCSVData();
            barGraph.ShowGraph(newData);
        }
        else
        {
            Debug.Log("csvreader is null");
        }
        yield return null;
    }
}
