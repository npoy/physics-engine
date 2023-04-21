using UnityEngine;
using System.Collections;

public class RocketEngine : MonoBehaviour {
    public float fuelMass;              // [kg]
    public float maxThrust;             // kN [Kg * m/s^2 ] => F = ma
    [Range(0, 1f)]
    public float thrustPercent;         // [none]
    public Vector3 thrustUnitVector;    // [none]
    private PhysicsEngine physicsEngine;
    private float currentThrust;        // N

    // Use this for initialization
    void Start() {
        physicsEngine = GetComponent<PhysicsEngine>();
        physicsEngine.mass += fuelMass;
    }

    void FixedUpdate() {
        if (fuelMass > FuelThisUpdate()) {
            fuelMass -= FuelThisUpdate();
            physicsEngine.mass -= FuelThisUpdate();
            ExertForce();
        } else {
            Debug.LogWarning("Out of rocket fuel");
        }
    }

    float FuelThisUpdate() {
        float exhaustMassFlow;    
        float effectiveExhaustVelocity;

        effectiveExhaustVelocity = 4462f;           // [m/s] liquid H O

        // thrust = massFlow * exhaustVelocity
        // massFlow = thrust / exhaustVelocity
        exhaustMassFlow = currentThrust / effectiveExhaustVelocity;

        return exhaustMassFlow * Time.deltaTime;    // [kg]
    }   

    void ExertForce() {
        currentThrust = thrustPercent * maxThrust * 1000f;
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust; // N
        physicsEngine.AddForce(thrustVector);
    }
}