using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarGraph3D : MonoBehaviour
{
    
    [SerializeField] private GameObject barPrefab;
    [SerializeField] private float barSpacing = 2f;
    [SerializeField] private float barScaleFactor = 0.2f;
    [SerializeField] private Transform graphOrigin;
    [SerializeField] public float animationDuration = 1f;
    [SerializeField] public Color labelcolor;  

    private List<GameObject> bars = new List<GameObject>();
    private List<GameObject> labels = new List<GameObject>();

    void Start()
    {
        
    }

    public void ShowGraph(Dictionary<string, int> data)
    {
        Debug.Log("showgraph called");
        int index = 0;
        foreach (var entry in data)
        {
            string dataname = entry.Key;
            int quantity = entry.Value;

            float xPos = index * barSpacing;

            // Instantiate the bar at graph origin with local positioning
            GameObject bar = Instantiate(barPrefab, graphOrigin);
            bar.transform.localPosition = new Vector3(0, 0, xPos);
            bar.transform.localScale = new Vector3(1, 0, 1); // Start with zero height

            StartCoroutine(AnimateBar(bar, quantity * barScaleFactor, animationDuration, dataname));

            bars.Add(bar);
            index++;
        }
    }

    private IEnumerator AnimateBar(GameObject bar, float targetHeight, float duration, string labelName)
    {
        float elapsedTime = 0;
        Vector3 startScale = new Vector3(1, 0, 1);
        Vector3 targetScale = new Vector3(1, targetHeight, 1);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            bar.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bar.transform.localScale = targetScale;

        // Instantiate label separately below the bar
        Vector3 labelPosition = new Vector3(bar.transform.position.x, graphOrigin.position.y - 0.5f, bar.transform.position.z);
        GameObject label = new GameObject("Label");
        label.transform.position = labelPosition;

        TextMeshPro textMesh = label.AddComponent<TextMeshPro>();
        textMesh.text = labelName;
        textMesh.fontSize = 3;
        textMesh.alignment = TextAlignmentOptions.Center;

        
        if (labelcolor != null)
        {
            textMesh.color = labelcolor;
        }

        label.transform.rotation = Quaternion.Euler(0f, -90f, 0f); // Face correct direction
        labels.Add(label);
    }

    public void ResetGraph()
    {
        StartCoroutine(ResetBarsCoroutine());
    }

    private IEnumerator ResetBarsCoroutine()
    {
        if(bars.Count == 0) yield break;

        float elapsedTime = 0;
        List<Vector3> startScales = new List<Vector3>();

        foreach (GameObject bar in bars)
        {
            startScales.Add(bar.transform.localScale);
        }

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;

            for (int i = 0; i < bars.Count; i++)
            {
                if (bars[i] != null)
                {
                    bars[i].transform.localScale = Vector3.Lerp(startScales[i], new Vector3(1, 0, 1), t);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject bar in bars)
        {
            Destroy(bar);
        }

        foreach (GameObject label in labels)
        {
            Destroy(label);
        }

        bars.Clear();
        labels.Clear();
    }
}
