using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class AstronomicalBody : MonoBehaviour
{
    public float MinDistanceFromOtherColliders;

    public ResourceBag ResourceProfile;
    public Vector2 ParticleExplosiveForceRange;

    public NonPlayerHealth Health;

    public ResourceParticle ParticlePrefab;

    void Start ()
    {
        Health.Died += die;
    }

    void die ()
    {
        Destroy(gameObject);

        foreach (ResourceType type in EnumUtil.AllValues<ResourceType>())
        {
            for (int i = 0; i < ResourceProfile[type]; i++)
            {
                Vector3 explosion = Random.onUnitSphere * RandomExtra.Range(ParticleExplosiveForceRange);
                var particle = Instantiate(ParticlePrefab, transform.position, Quaternion.LookRotation(explosion));
                particle.Initialize(Player.Instance.transform, type);
                particle.Rigidbody.AddForce(explosion, ForceMode.VelocityChange);
            }
        }

        PlanetExplosionEffect.Instance.Play(transform.position);
    }
}
