using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Force;
    public float MovementHeatCostPerSecond;
    
    public string InputAxisX, InputAxisY, InputAxisZ;

    public Rigidbody Rigidbody;
    public PlayerHeat Heat;

    Vector3 inputDir;

    void Update ()
    {
        inputDir = new Vector3
        (
            Input.GetAxis(InputAxisX),
            Input.GetAxis(InputAxisY),
            Input.GetAxis(InputAxisZ)
        ).normalized;

        if (inputDir != Vector3.zero)
        {
            Heat.CurrentValue -= MovementHeatCostPerSecond * Time.deltaTime;
        }
    }

    void FixedUpdate ()
    {
        Rigidbody.AddRelativeForce(inputDir * Force, ForceMode.Acceleration);
    }
}
