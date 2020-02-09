using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerHealth : MonoBehaviour
{
    public float Value { get; private set; }
    
    public float InitialValue;

    void Start ()
    {
        Value = InitialValue;
    }

    public void Damage (float amount)
    {
        Value -= amount;

        if (amount <= 0) Destroy(gameObject);
    }
}
