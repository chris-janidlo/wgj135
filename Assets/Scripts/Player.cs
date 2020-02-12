using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using crass;

[RequireComponent(typeof(Collider))]
public class Player : Singleton<Player>
{
    public ResourceBag Resources;

    public float NormalizedHeat => (Resources.Heat - HeatMin) / (HeatMax - HeatMin);

    public float HeatMin, HeatMax;
    public float IdleLifeSupportDrainPerSecond;
    public float DeathGracePeriodSeconds;
    public string GameoverScene;

    public PlayerMovement Movement;

    IEnumerator deathEnum;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    void Update ()
    {
        Resources.Heat = Mathf.Clamp(Resources.Heat, HeatMin, HeatMax);

        if (Resources.Heat == HeatMin)
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

        Resources.Heat -= IdleLifeSupportDrainPerSecond * Time.deltaTime;
    }

    IEnumerator deathRoutine ()
    {
        yield return new WaitForSeconds(DeathGracePeriodSeconds);
        SceneManager.LoadScene(GameoverScene);
    }
}
