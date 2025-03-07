using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineGraph3D : MonoBehaviour
{
    
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private float pointSpacing = 2f;
    [SerializeField] private float pointScaleFactor = 0.5f;
    [SerializeField] private Transform graphOrigin;
    [SerializeField] private Color lineColor;
    [SerializeField] public float lineAnimationDuration = 1f;
    [SerializeField] public Color labelColor;

    

    private List<GameObject> points = new List<GameObject>();
    private List<LineRenderer> lines = new List<LineRenderer>();
    private List<GameObject> labels = new List<GameObject>();

    public void ShowGraph(Dictionary<string, int> data)
    {
        int index = 0;
        GameObject prevPoint = null;

        foreach (var entry in data)
        {
            string dataName = entry.Key;
            int quantity = entry.Value;

            float xPos = index * pointSpacing;
            Vector3 pointPosition = new Vector3(0, quantity/2.5f * pointScaleFactor, xPos) + graphOrigin.position;

            GameObject point = Instantiate(pointPrefab, pointPosition, Quaternion.identity, graphOrigin);
            points.Add(point);

            // Instantiate label below each point
            Vector3 labelPosition = new Vector3(point.transform.position.x, graphOrigin.position.y - 0.5f, point.transform.position.z);
            GameObject label = new GameObject("Label");
            label.transform.position = labelPosition;
            TextMeshPro textMesh = label.AddComponent<TextMeshPro>();
            textMesh.text = dataName;
            textMesh.fontSize = 3;
            textMesh.alignment = TextAlignmentOptions.Center;
            textMesh.color = labelColor;
            label.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            labels.Add(label);

            if (prevPoint != null)
            {
                StartCoroutine(AnimateLine(prevPoint.transform.position, point.transform.position));
            }

            prevPoint = point;
            index++;
        }
    }

    private IEnumerator AnimateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("Line");
        LineRenderer line = lineObj.AddComponent<LineRenderer>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = lineColor;
        line.endColor = lineColor;
        line.positionCount = 2;

        float elapsedTime = 0;
        while (elapsedTime < lineAnimationDuration)
        {
            float t = elapsedTime / lineAnimationDuration;
            Vector3 currentPosition = Vector3.Lerp(start, end, t);
            line.SetPosition(0, start);
            line.SetPosition(1, currentPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        line.SetPosition(1, end);
        lines.Add(line);
    }

    public void ResetGraph()
    {
        if (points.Count == 0 && lines.Count == 0 && labels.Count == 0)
        {
            return;
        }
       

        foreach (GameObject point in points)
        {
            Destroy(point);
        }
        points.Clear();

        foreach (LineRenderer line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();

        foreach (GameObject label in labels)
        {
            Destroy(label);
        }
        labels.Clear();
    }
}
