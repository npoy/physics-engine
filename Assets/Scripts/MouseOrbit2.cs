using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This is the same as the script from Unity's Standard Assets
With one addition to allow for zooming with mouse wheel
*/
public class MouseOrbit2 : MonoBehaviour
{
    public Transform target;
    float distance = 10.0f;

    float xSpeed = 250.0f;
    float ySpeed = 120.0f;

    float yMinLimit = -20;
    float yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;

    void Start () {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void LateUpdate () {
        if (target) {
            x += (float)(Input.GetAxis("Mouse X") * xSpeed * 0.02);
            y -= (float)(Input.GetAxis("Mouse Y") * ySpeed * 0.02);
            distance += Input.mouseScrollDelta.y * 0.2f; // Line added
            
            y = ClampAngle(y, yMinLimit, yMaxLimit);
                
            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
            
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    static float ClampAngle (float angle, float min, float max) {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}	
