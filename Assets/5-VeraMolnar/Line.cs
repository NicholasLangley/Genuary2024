using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    Vector3 a = Vector3.zero;
    Vector3 b = Vector3.zero;
    Vector3 oldA;
    Vector3 oldB;

    bool lerping;
    float lerpFactor;
    float lerpSpeed = 1f;

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

        lerping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerping)
        {
            if (lerpFactor > 1.0f)
            {
                lerping = false;
                lerpFactor = 1.0f;
            }
            lr.SetPosition(0, Vector3.Lerp(oldA, a, lerpFactor));
            lr.SetPosition(1, Vector3.Lerp(oldB, b, lerpFactor));
            lerpFactor += lerpSpeed * Time.deltaTime;
        }
    }

    public void newDestPoint(Vector3 newPointA, Vector3 newPointB)
    {
        this.oldA = this.a;
        this.a = newPointA;
        this.oldB = this.b;
        this.b = newPointB;
        lerping = true;
        lerpFactor = 0.0f;
    }

    public Vector3 getPointA()
    {
        return this.a;
    }
}
