using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float Damage;
    public Rigidbody Rigidbody;

    void OnCollisionEnter (Collision other)
    {
        // do damage
    }
}
