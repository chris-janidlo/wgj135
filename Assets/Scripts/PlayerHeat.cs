using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeat : MonoBehaviour
{
    [SerializeField]
    float _value;
    public float CurrentValue
    {
        get => _value;
        set => _value = Mathf.Clamp(value, ColdDeathValue, HeatDeathValue);
    }

    public float NormalizedValue => (CurrentValue - ColdDeathValue) / (HeatDeathValue - ColdDeathValue);

    public float ColdDeathValue, HeatDeathValue;
    public float IdleLifeSupportDrainPerSecond;
    public float DeathGracePeriodSeconds;

    IEnumerator deathEnum;

    void Update ()
    {
        if (CurrentValue == ColdDeathValue || CurrentValue == HeatDeathValue)
        {
            if (deathEnum == null)
            {
                deathEnum = deathRoutine();
                StartCoroutine(deathEnum);
            }
        }
        else if (deathEnum != null)
        {
            StopCoroutine(deathEnum);
            deathEnum = null;
        }

        CurrentValue -= IdleLifeSupportDrainPerSecond * Time.deltaTime;
    }

    IEnumerator deathRoutine ()
    {
        yield return new WaitForSeconds(DeathGracePeriodSeconds);
        // TODO: actual game over
        while (true)
        {
            Debug.Log("GAME OVER");
            yield return null;
        }
    }
}
