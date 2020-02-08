using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Force;
    
    public string InputAxisX, InputAxisY, InputAxisZ;

    public Rigidbody Rigidbody;

    Vector3 inputDir;

    void Update ()
    {
        inputDir = new Vector3
        (
            Input.GetAxis(InputAxisX),
            Input.GetAxis(InputAxisY),
            Input.GetAxis(InputAxisZ)
        );
    }

    void FixedUpdate ()
    {
        Rigidbody.AddRelativeForce(inputDir * Force, ForceMode.Acceleration);
    }
}
