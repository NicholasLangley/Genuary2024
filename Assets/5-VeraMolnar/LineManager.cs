using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField]
    GameObject LinePrefab;

    public List<GameObject> lines;
    List<Vector3> startingPointGrid;

    [SerializeField]
    [Range (1, 100)]
    int gridWidth;

    [SerializeField]
    [Range(1, 100)]
    int gridHeight;

    [SerializeField]
    [Range(1, 20)]
    float minLength;

    [SerializeField]
    [Range(1, 20)]
    float maxLength;

    [SerializeField]
    [Range(0, 1)]
    float maxSpeed;

    [SerializeField]
    [Range(0, 1)]
    float minSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lines = new List<GameObject>();
        startingPointGrid = new List<Vector3>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 p1 = new Vector3((float)x - gridWidth / 2f, (float)y - gridHeight/2f, 0);
                Vector3 dir = UnityEngine.Random.insideUnitSphere.normalized;
                float length = UnityEngine.Random.Range(minLength, maxLength);
                Vector3 p2 = new Vector3(p1.x + dir.x * length, p1.y + dir.y * length, p1.z + dir.z * length);
                GameObject line = Instantiate(LinePrefab);
                line.GetComponent<Line>().setPoints(p1, p2);
                line.transform.SetParent(transform);
                lines.Add(line);

                startingPointGrid.Add(p1);
            }
        }

        InvokeRepeating("updateLines", 3.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateLines()
    {
        //randomize starting points
        shuffleStartinglist(startingPointGrid, DateTime.UtcNow.Millisecond);

        int i = 0;
        foreach (GameObject line in lines)
        {
            Vector3 p1 = startingPointGrid[i];
            Vector3 dir = UnityEngine.Random.insideUnitSphere.normalized;
            float length = UnityEngine.Random.Range(minLength, maxLength);
            Vector3 p2 = new Vector3(p1.x + dir.x * length, p1.y + dir.y * length, p1.z + dir.z * length);
            line.GetComponent<Line>().newDestPoint(p1, p2);
            i++;
        }
    }

    void shuffleStartinglist(List<Vector3> list, int seed)
    {
        System.Random rng = new System.Random(seed);
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Vector3 value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}
