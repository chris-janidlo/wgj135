using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class PlayerMovement : MonoBehaviour
{
    public float MaxSpeed, TurnSpeed;
    public Vector3 Thrust;
    public float MaxThrustHeatCostPerSecond;

    public EasingFunction.Ease DecelerationEase;
    public float KnockbackNonDecelTime;

    public string MovementAxisX, MovementAxisY, MovementAxisZ, PitchAxis, YawAxis;

    public AnimationCurve TranslationalInputMagnitudeToThrusterVolume, RotationalInputMagnitudeToThrusterVolume;
    public AudioSource ThrusterSource;

    [SerializeField]
    Rigidbody Rigidbody;
    
    TransitionableVector3 decelerationTransition = new TransitionableVector3();

    Vector3 translationalInput;
    Vector2 rotationalInput;

    float knockbackTimer;
    bool cruiseControl;

    void Start ()
    {
        decelerationTransition.AttachMonoBehaviour(this);
    }

    void Update ()
    {
        translationalInput = new Vector3
        (
            Input.GetAxis(MovementAxisX),
            Input.GetAxis(MovementAxisY),
            Input.GetAxis(MovementAxisZ)
        );

        rotationalInput = new Vector2
        (
            Input.GetAxis(PitchAxis),
            Input.GetAxis(YawAxis)
        );

        knockbackTimer -= Time.deltaTime;
    }

    void FixedUpdate ()
    {
        rotate();
        translate();
    }

    void OnGUI ()
    {
        cruiseControl = Event.current.capsLock;
    }

    public void Knockback (Vector3 force)
    {
        Rigidbody.AddForce(force, ForceMode.VelocityChange);
        knockbackTimer = KnockbackNonDecelTime;
    }

    void rotate ()
    {
		transform.localRotation *= Quaternion.AngleAxis(rotationalInput.y * TurnSpeed, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(rotationalInput.x * TurnSpeed, Vector3.right);
    }

    void translate ()
    {
        float currentSpeed = Rigidbody.velocity.magnitude;

        if (translationalInput != Vector3.zero)
        {
            Rigidbody.AddRelativeForce(Vector3.Scale(translationalInput, Thrust), ForceMode.Acceleration);
            decelerationTransition.Value = Rigidbody.velocity;
        }
        else if (knockbackTimer <= 0 && !cruiseControl)
        {
            if (Rigidbody.velocity != Vector3.zero && !decelerationTransition.IsTransitioningTo(Vector3.zero))
            {
                float maxThrust = Mathf.Max(Thrust.x, Mathf.Max(Thrust.y, Thrust.z));
                // assuming the player took the fastest route to get to the current speed, this means that it takes exactly as many seconds to slow down as it did to speed up
                float decelTime = (currentSpeed / maxThrust);
                decelerationTransition.StartTransitionToIfNotAlreadyStarted(Vector3.zero, decelTime, DecelerationEase);
            }

            Rigidbody.velocity = decelerationTransition.Value;
        }
        else
        {
            decelerationTransition.StopTransitioning();
        }

        if (currentSpeed > MaxSpeed)
            Rigidbody.velocity = Rigidbody.velocity.normalized * MaxSpeed;

        float scaledThrustHeatCost = MaxThrustHeatCostPerSecond * currentSpeed / MaxSpeed;
        Player.Instance.Resources.Heat -= scaledThrustHeatCost * Time.deltaTime;

        ThrusterSource.volume =
            TranslationalInputMagnitudeToThrusterVolume.Evaluate(translationalInput.magnitude) +
            RotationalInputMagnitudeToThrusterVolume.Evaluate(rotationalInput.magnitude);
    }
}
