using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    public Vector3 dir = Vector3.zero;
    public float velocity = 0.0f;
    public float lifetime = 1.0f;
    public float acceleration = 0.0f;
    float age = 0.0f;

    public void initialize(Vector3 newDir, float newVel, float newLife, float accel)
    {
        dir = newDir;
        velocity = newVel;
        acceleration = accel;
        lifetime = newLife;
        age = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        this.transform.localPosition += dir * velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;
        this.GetComponent<MeshRenderer>().material.SetVector("_dir", dir);
        this.GetComponent<MeshRenderer>().material.SetFloat("_age", Mathf.InverseLerp(0, lifetime, age));
        if (age >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
