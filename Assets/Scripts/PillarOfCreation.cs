using System;
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
        ResourceBag potentialGoals = new ResourceBag(0, 0, 0, 0);

        for (int i = 0; i < GuaranteedBodyPrefabs.Items.Count; i++)
        {
            var spawned = spawnAstronomicalBody(GuaranteedBodyPrefabs);

            potentialGoals += spawned.ResourceProfile;
        }

        for (int i = 0; i < RandomExtra.Range(ExtraBodiesToSpawnRange); i++)
        {
            var spawned = spawnAstronomicalBody(ExtraBodyPrefabs);

            potentialGoals += spawned.ResourceProfile;
        }

        Goals = new ResourceBag
        (
            0,
            UnityEngine.Random.Range(potentialGoals.Hydrogen / 2, potentialGoals.Hydrogen),
            UnityEngine.Random.Range(potentialGoals.Methane / 2, potentialGoals.Methane),
            UnityEngine.Random.Range(potentialGoals.Silicon / 2, potentialGoals.Silicon)
        );
    }

    AstronomicalBody spawnAstronomicalBody (AstronomicalBodyBag bag)
    {
        AstronomicalBody next = bag.GetNext();
        return Instantiate(next, findUnoccupiedPosition(next.MinDistanceFromOtherColliders), UnityEngine.Random.rotation, AstronomicalBodyParent);
    }

    public Vector3 findUnoccupiedPosition (float minDistance)
    {
        Vector3 position;
        
        do
        {
            position = transform.position + UnityEngine.Random.onUnitSphere * RandomExtra.Range(DistanceFromSpawnRange);
        } while (Physics.CheckSphere(position, minDistance));

        return position;
    }
}

[Serializable]
public class AstronomicalBodyBag : BagRandomizer<AstronomicalBody> {}
