using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class PlanetExplosionEffect : Singleton<PlanetExplosionEffect>
{
    [SerializeField]
    private AudioSource SourceToPlay;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    public void Play (Vector3 position)
    {
        transform.position = position;
        SourceToPlay.Play();
    }
}
