using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public float mass = 1f;        // [kg]
    public Vector3 velocityVector; // [m/s] average velocity this FixedUpdate()
    public Vector3 netForceVector; // N [kg m/s^2]

    private List<Vector3> forceVectorList = new List<Vector3>();
    private PhysicsEngine[] physicsEngineArray;

    private const float bigG = 6.673e-11f; // N * (m/kg)2

    // Use this for initialization
    void Start() {
        SetupThrustTrails();
        physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
    }

    void FixedUpdate() {
        RenderTrails();
        CalculateGravity();
        UpdatePosition();
    }

    public void AddForce(Vector3 forceVector) {
        forceVectorList.Add(forceVector);
    }

    void CalculateGravity() {
        foreach (PhysicsEngine physicsEngineA in physicsEngineArray)
        {
            foreach (PhysicsEngine physicsEngineB in physicsEngineArray)
            {
                if (physicsEngineA != physicsEngineB && physicsEngineA != this) {
                    Debug.Log("Calculating force exerted on " + physicsEngineA.name +
                              " due to the gravity of " + physicsEngineB.name);

                    Vector3 offSet = physicsEngineA.transform.position - physicsEngineB.transform.position;
                    float rSquared = Mathf.Pow(offSet.magnitude, 2f);
                    float gravityMagnitude = bigG * physicsEngineA.mass * physicsEngineB.mass / rSquared;
                    Vector3 gravityFeltVector = gravityMagnitude * offSet.normalized;

                    physicsEngineA.AddForce(-gravityFeltVector);
                }
            }
        }
    }

    void UpdatePosition() {
        // Sum the forces and clear the list
        netForceVector = Vector3.zero;
        foreach (Vector3 forceVector in forceVectorList) {
            netForceVector = netForceVector + forceVector;
        }
        forceVectorList.Clear();

        // Calculate position change due to net force
        Vector3 accelerationVector = netForceVector / mass; // F=m*a => a=F/m
        velocityVector += accelerationVector * Time.deltaTime; // a=(Vi-Vf)/t => v=a*t
        transform.position += velocityVector * Time.deltaTime;
    }

    // Draw Forces
    public bool showTrails = true;
	private LineRenderer lineRenderer;
	private int numberOfForces;
	
	void SetupThrustTrails () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.SetColors(Color.yellow, Color.yellow);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.useWorldSpace = false;
	}
	
	// Update is called once per frame
	void RenderTrails () {
		if (showTrails) {
			lineRenderer.enabled = true;
			numberOfForces = forceVectorList.Count;
			lineRenderer.SetVertexCount(numberOfForces * 2);
			int i = 0;
			foreach (Vector3 forceVector in forceVectorList) {
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i+1, -forceVector);
				i = i + 2;
			}
		} else {
			lineRenderer.enabled = false;
		}
	}
}
