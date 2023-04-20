using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public float mass = 1f;
    public Vector3 velocityVector; // average velocity this FixedUpdate()
    public Vector3 netForceVector;
    public List<Vector3> forceVectorList = new List<Vector3>();

    void FixedUpdate() {
        AddForces();
        UpdateVelocity();

        // Update position
        transform.position += velocityVector * Time.deltaTime;
    }

    void AddForces() {
        netForceVector = Vector3.zero;
        foreach (Vector3 forceVector in forceVectorList) {
            netForceVector = netForceVector + forceVector;
        }
    }

    void UpdateVelocity() {
        Vector3 accelerationVector = netForceVector / mass; // F=m*a => a=F/m
        velocityVector += accelerationVector * Time.deltaTime; // a=(Vi-Vf)/t => v=a*t
    }
}
