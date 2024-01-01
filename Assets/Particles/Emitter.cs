using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    [SerializeField]
    [Range (1, 100)]
    float startVel;

    [SerializeField]
    [Range(1, 100)]
    float startLifetime;

    [SerializeField]
    [Range(-2, 2)]
    float acceleration;

    [SerializeField]
    GameObject particlePrefab;

    // Update is called once per frame
    void Update()
    {
        GameObject par = Instantiate(particlePrefab);
        par.GetComponent<Particle>().initialize(Random.insideUnitSphere.normalized, startVel, startLifetime, acceleration);
    }
}
