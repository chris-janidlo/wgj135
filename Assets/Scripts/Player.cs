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

    public float PickupSoundRepeatTime;
    public int MaxPickupSoundRepeats;
    public AudioSource PickupSource;

    IEnumerator deathEnum;
    int pickupSoundsToPlay;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    void Start ()
    {
        StartCoroutine(pickupSoundRoutine());
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

    public void PlayPickupSound ()
    {
        pickupSoundsToPlay++;
        if (pickupSoundsToPlay > MaxPickupSoundRepeats) pickupSoundsToPlay = MaxPickupSoundRepeats;
    }

    IEnumerator pickupSoundRoutine ()
    {
        while (true)
        {
            while (pickupSoundsToPlay > 0)
            {
                PickupSource.Play();
                pickupSoundsToPlay--;
                yield return new WaitForSeconds(PickupSoundRepeatTime);
            }
            yield return null;
        }
    }

    IEnumerator deathRoutine ()
    {
        yield return new WaitForSeconds(DeathGracePeriodSeconds);
        SceneManager.LoadScene(GameoverScene);
    }
}
