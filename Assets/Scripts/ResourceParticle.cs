using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ResourceParticle : MonoBehaviour
{
    public float AttractionDelay, AttractionTime;
    public Rigidbody Rigidbody;

    float attractionTimer;

    Transform target;
    ResourceType resourceType;

    Vector3 smoothDampVelocity = Vector3.zero;

    public void Initialize (Transform target, ResourceType resourceType)
    {
        this.target = target;
        this.resourceType = resourceType;

        attractionTimer = AttractionDelay;
    }

    void Update ()
    {
        attractionTimer -= Time.deltaTime;
    }

    void FixedUpdate ()
    {
        if (attractionTimer <= 0)
        {
            Rigidbody.velocity = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref smoothDampVelocity, AttractionTime);
        }
    }

    void OnCollisionEnter (Collision other)
    {
        var player = other.gameObject.GetComponent<Player>();
        player?.Resources.AddOneOfType(resourceType);
        player?.PlayPickupSound();
        Destroy(gameObject);
    }
}
