using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraphController : MonoBehaviour
{
     public LineGraph3D lineGraph;
     public BarGraphController barGraphcontroller;
    public void PlotData(TextAsset csvFile)
    {
        if (lineGraph == null)
        {
            Debug.LogError("LineGraph3D is missing!");
            return;
        }

        if (csvFile == null)
        {
            Debug.LogError("CSV file is null!");
            return;
        }

        StartCoroutine(PlotDataCoroutine(csvFile));
    }

    private IEnumerator PlotDataCoroutine(TextAsset csvFile)
    {
        lineGraph.ResetGraph();
        barGraphcontroller.barGraph.ResetGraph();

        yield return new WaitForSeconds(lineGraph.lineAnimationDuration);

        CSVReader csvReader = lineGraph.GetComponent<CSVReader>();
        if (csvReader != null)
        {
            csvReader.csvFile = csvFile;
            Dictionary<string, int> newData = csvReader.GetCSVData();
            lineGraph.ShowGraph(newData);
        }
    }
}
