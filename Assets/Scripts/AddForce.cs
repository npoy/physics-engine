using UnityEngine;
using System.Collections;

public class AddForce : MonoBehaviour {
    public Vector3 forceVector;
    private PhysicsEngine physicsEngine;

    // Use this for initialization
    void Start() {
        physicsEngine = GetComponent<PhysicsEngine>();
    }

    void FixedUpdate() {
        physicsEngine.AddForce(forceVector);
    }
}