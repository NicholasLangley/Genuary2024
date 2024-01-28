using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePendulum : MonoBehaviour
{
    [SerializeField]
    GameObject pendulumBallPrefab;

    [SerializeField]
    GameObject lineSegmentPrefab;

    GameObject lineSeg1, lineSeg2;

    GameObject ball1, ball2;


    //angle of pendulum from y axis
    [SerializeField]
    float theta1, theta2;

    //angle change
    [SerializeField]
    float theta1Dot = 0f, theta2Dot = 0f;
    float theta1DotDot = 0f, theta2DotDot = 0f;

    //mass of pendulum
    [SerializeField]
    [Range(1, 100)]
    float m1 = 10, m2 = 10;

    //length of pendulum
    [SerializeField]
    [Range(0.5f, 100)]
    float l1 = 1f, l2 = 1f;

    [SerializeField]
    float g = -9.81f;

    //positions
    Vector3 pos1, pos2;

    LineRenderer lr;

    private void Awake()
    {
        lr = this.GetComponent<LineRenderer>();
        pos1 = new Vector3();
        pos2 = new Vector3();

        ball1 = Instantiate(pendulumBallPrefab);
        ball2 = Instantiate(pendulumBallPrefab);

        ball1.GetComponent<ParticleSystem>().Pause();

        lineSeg1 = Instantiate(lineSegmentPrefab);
        lineSeg2 = Instantiate(lineSegmentPrefab);

        lineSeg1.GetComponent<LineRenderer>().startWidth = 0.05f;
        lineSeg1.GetComponent<LineRenderer>().endWidth = 0.05f;
        lineSeg2.GetComponent<LineRenderer>().startWidth = 0.05f;
        lineSeg2.GetComponent<LineRenderer>().endWidth = 0.05f;

        theta1 = (float)Random.Range(0f, 2f * Mathf.PI);
        theta2 = (float)Random.Range(0f, 2f * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        colorByVelocity();

        theta1Dot += theta1DotDot * Time.deltaTime;
        theta2Dot += theta2DotDot * Time.deltaTime;

        theta1 += theta1Dot * Time.deltaTime; 
        theta2 += theta2Dot * Time.deltaTime;

        updateAngularAcceleration();
    }


    void UpdatePosition()
    {
        pos1.x = l1 * Mathf.Sin(theta1);
        pos1.y = l1 * Mathf.Cos(theta1);

        pos2.x = pos1.x + l2 * Mathf.Sin(theta2);
        pos2.y = pos1.y + l2 * Mathf.Cos(theta2);

        lineSeg1.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        lineSeg1.GetComponent<LineRenderer>().SetPosition(1, pos1);

        lineSeg2.GetComponent<LineRenderer>().SetPosition(0, pos1);
        lineSeg2.GetComponent<LineRenderer>().SetPosition(1, pos2);

        ball1.transform.position = pos1;
        ball2.transform.position = pos2;
    }

    void colorByVelocity()
    {
        Vector2 ball1Vel = new Vector2();
        Vector2 ball2Vel = new Vector2();

        ball1Vel.x = theta1Dot * l1 * Mathf.Cos(theta1);
        ball1Vel.y = theta1Dot * l1 * Mathf.Sin(theta1);

        ball2Vel.x = ball1Vel.x + theta2Dot * l2 * Mathf.Cos(theta2);
        ball2Vel.y = ball1Vel.y + theta2Dot * l2 * Mathf.Sin(theta2);

        ball1.GetComponent<MeshRenderer>().material.SetVector("_vel", ball1Vel);
        ball2.GetComponent<MeshRenderer>().material.SetVector("_vel", ball2Vel);

        lineSeg1.GetComponent<LineRenderer>().material.SetFloat("_angularVel", theta1Dot);
        lineSeg2.GetComponent<LineRenderer>().material.SetFloat("_angularVel", theta2Dot);

        ParticleSystem.MainModule particleMain = ball2.GetComponent<ParticleSystem>().main;
        particleMain.startColor = new Color(0, Mathf.Abs(ball2Vel.y) / 5, Mathf.Abs(ball2Vel.x) / 5, 1.0f);

    }

    void updateAngularAcceleration()
    {

        float eq1 = 0 -  g * ((2f * m1) + m2) * Mathf.Sin(theta1);
        float eq2 = m2 * g * Mathf.Sin(theta1 - (2f * theta2));
        float eq3 = 2f * Mathf.Sin(theta1 - theta2) * m2;
        float eq4 = (Mathf.Pow(theta2Dot, 2f) * l2) + (Mathf.Pow(theta1Dot, 2f) * l1 * Mathf.Cos(theta1 - theta2));
        float denominator = (2f * m1) + m2 - (m2 * Mathf.Cos((2f * theta1) - (2f * theta2)));

        theta1DotDot = (eq1 - eq2 - (eq3 * eq4)) / (l1 * denominator);

        float eq5 = 2f * Mathf.Sin(theta1 - theta2);
        float eq6 = Mathf.Pow(theta1Dot, 2f) * l1 * (m1 + m2);
        float eq7 = g * (m1 + m2) * Mathf.Cos(theta1);
        float eq8 = Mathf.Pow(theta2Dot, 2f) * l2 * m2 * Mathf.Cos(theta1 - theta2);

        theta2DotDot = (eq5 * (eq6 + eq7 + eq8)) / (l2 * denominator);
    }

}
