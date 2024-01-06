using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    Vector3 a = Vector3.zero;
    Vector3 b = Vector3.zero;

    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    public void setPoints(Vector3 a, Vector3 b)
    {
        this.a = a;
        this.b = b;
        
        lr.SetPosition(0, a);
        lr.SetPosition(1, b);
        lr.startWidth  = lr.endWidth = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
