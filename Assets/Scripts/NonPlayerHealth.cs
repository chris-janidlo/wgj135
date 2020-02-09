using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NonPlayerHealth : MonoBehaviour
{
    public event Action Died;

    [SerializeField]
    float _value;
    public float Value
    {
        get => _value;
        set => _value = value;
    }

    public void Damage (float amount)
    {
        Value -= amount;

        if (Value <= 0)
        {
            Destroy(gameObject);
            Died?.Invoke();
        }
    }
}
