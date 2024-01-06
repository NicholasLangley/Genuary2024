using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField]
    GameObject LinePrefab;

    public List<GameObject> lines;

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

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 p1 = new Vector3((float)x - gridWidth / 2f, (float)y - gridHeight/2f, 0);
                Vector3 dir = Random.insideUnitSphere.normalized;
                float length = Random.Range(minLength, maxLength);
                Vector3 p2 = new Vector3(p1.x + dir.x * length, p1.y + dir.y * length, p1.z + dir.z * length);
                GameObject line = Instantiate(LinePrefab);
                line.GetComponent<Line>().setPoints(p1, p2);
                line.transform.SetParent(transform);
                lines.Add(line);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
