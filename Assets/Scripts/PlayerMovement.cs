using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Force;
    public float MinimumVelocity;
    public float MovementHeatCostPerSecond;
    
    public string InputAxisX, InputAxisY, InputAxisZ;

    public Rigidbody Rigidbody;

    Vector3 inputDir;
    bool inputting;

    void Update ()
    {
        inputDir = new Vector3
        (
            Input.GetAxis(InputAxisX),
            Input.GetAxis(InputAxisY),
            Input.GetAxis(InputAxisZ)
        ).normalized;

        inputting = inputDir != Vector3.zero;

        if (inputting)
        {
            Player.Instance.Resources.Heat -= MovementHeatCostPerSecond * Time.deltaTime;
        }
    }

    void FixedUpdate ()
    {
        Rigidbody.AddRelativeForce(inputDir * Force, ForceMode.Acceleration);

        if (!inputting && Rigidbody.velocity.magnitude < MinimumVelocity)
        {
            Rigidbody.velocity = Vector3.zero;
        }
    }
}
