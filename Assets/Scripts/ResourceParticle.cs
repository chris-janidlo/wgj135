using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ResourceParticle : MonoBehaviour
{
    public float PlayerMoveTime;
    public Rigidbody Rigidbody;

    Transform target;
    ResourceType resourceType;

    Vector3 smoothDampVelocity = Vector3.zero;

    public void Initialize (Transform target, ResourceType resourceType)
    {
        this.target = target;
        this.resourceType = resourceType;
    }

    void FixedUpdate ()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref smoothDampVelocity, PlayerMoveTime);
    }

    void OnCollisionEnter (Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        player?.Resources.AddOneOfType(resourceType);
        Destroy(gameObject);
    }
}
