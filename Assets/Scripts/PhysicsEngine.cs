using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public Vector3 velocityVector; // average velocity this FixedUpdate()
    public Vector3 netForceVector;
    public List<Vector3> forceVectorList = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() {
        AddForces();
        
        if (netForceVector == Vector3.zero) {
            transform.position += velocityVector * Time.deltaTime;
        } else {
            Debug.LogError("Unbalanced force detected, help!");
        }
    }

    void AddForces() {
        netForceVector = Vector3.zero;
        foreach (Vector3 forceVector in forceVectorList) {
            netForceVector = netForceVector + forceVector;
        }
    }
}
