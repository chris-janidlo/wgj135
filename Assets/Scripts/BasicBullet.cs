using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float Damage, LifeTime;

    public Collider Collider;
    public Rigidbody Rigidbody;

    IEnumerator Start ()
    {
        yield return new WaitForSeconds(LifeTime);
        stop();
    }

    void OnCollisionEnter (Collision other)
    {
        var health = other.gameObject.GetComponent<NonPlayerHealth>();
        health?.Damage(Damage);

        stop();
    }

    void stop ()
    {
        Destroy(Rigidbody);
        Destroy(Collider);
    }
}
