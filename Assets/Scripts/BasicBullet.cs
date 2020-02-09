using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float Damage;
    public Rigidbody Rigidbody;

    void OnCollisionEnter (Collision other)
    {
        var health = other.gameObject.GetComponent<NonPlayerHealth>();
        health?.Damage(Damage);

        Destroy(gameObject);
    }
}
