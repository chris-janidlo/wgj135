using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

// astronomical body factory
public class PillarOfCreation : MonoBehaviour
{
    // please don't modify the internal values of this from anywhere except this class :)))
    public ResourceBag Goals { get; private set; }

    public Vector2Int ExtraBodiesToSpawnRange;
    public Vector2 DistanceFromSpawnRange;

    public AstronomicalBodyBag GuaranteedBodyPrefabs, ExtraBodyPrefabs;

    public Transform AstronomicalBodyParent;

    void Start ()
    {
        List<AstronomicalBody> prefabs = getPrefabsToSpawn();
        List<Vector3> unitSphereDistribution = fibonacciSphere(prefabs.Count, true);
        
        for (int i = 0; i < prefabs.Count; i++)
        {
            Vector3 point;

            do
            {
                point = transform.position + unitSphereDistribution[i] * RandomExtra.Range(DistanceFromSpawnRange);
            } while (Physics.CheckSphere(point, prefabs[i].MinDistanceFromOtherColliders));

            Instantiate(prefabs[i], point, UnityEngine.Random.rotation);
        }

        Goals = getGoals(prefabs);
    }

    List<AstronomicalBody> getPrefabsToSpawn ()
    {
        List<AstronomicalBody> prefabs = new List<AstronomicalBody>(GuaranteedBodyPrefabs.Items);

        for (int i = 0; i < RandomExtra.Range(ExtraBodiesToSpawnRange); i++)
        {
            prefabs.Add(ExtraBodyPrefabs.GetNext());
        }

        prefabs.ShuffleInPlace();

        return prefabs;
    }

    // translated from python version at https://stackoverflow.com/a/26127012/5931898
    List<Vector3> fibonacciSphere (int samples, bool randomize)
    {
        float rand = randomize ? UnityEngine.Random.Range(0f, samples) : 1;

        List<Vector3> points = new List<Vector3>();
        float offset = 2f / samples;
        float increment = Mathf.PI * (3 - Mathf.Sqrt(5f));

        for (int i = 0; i < samples; i++)
        {
            float y = i * offset - 1 + offset / 2;
            float r = Mathf.Sqrt(1 - Mathf.Pow(y, 2));
            float phi = ((i + rand) % samples) * increment;

            points.Add(new Vector3
            (
                Mathf.Cos(phi) * r,
                y,
                Mathf.Sin(phi) * r
            ));
        }

        return points;
    }

    ResourceBag getGoals (List<AstronomicalBody> bodies)
    {
        ResourceBag potentialGoals = new ResourceBag(0, 0, 0, 0);

        foreach (ResourceBag bag in bodies.Select(b => b.ResourceProfile))
        {
            potentialGoals += bag;
        }

        return new ResourceBag
        (
            0,
            UnityEngine.Random.Range(potentialGoals.Hydrogen / 2, potentialGoals.Hydrogen) / 10 * 10,
            UnityEngine.Random.Range(potentialGoals.Methane / 2, potentialGoals.Methane) / 10 * 10,
            UnityEngine.Random.Range(potentialGoals.Silicon / 2, potentialGoals.Silicon) / 10 * 10
        );
    }
}

[Serializable]
public class AstronomicalBodyBag : BagRandomizer<AstronomicalBody> {}
