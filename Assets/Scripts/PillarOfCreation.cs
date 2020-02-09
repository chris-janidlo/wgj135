using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

// astronomical body factory
public class PillarOfCreation : MonoBehaviour
{
    public Vector2Int ExtraBodiesToSpawnRange;
    public Vector2 DistanceFromOriginRange;

    public AstronomicalBodyBag GuaranteedBodyPrefabs, ExtraBodyPrefabs;

    public Transform AstronomicalBodyParent;

    void Start ()
    {
        for (int i = 0; i < GuaranteedBodyPrefabs.Items.Count; i++)
        {
            spawnAstronomicalBody(GuaranteedBodyPrefabs);
        }

        for (int i = 0; i < RandomExtra.Range(ExtraBodiesToSpawnRange); i++)
        {
            spawnAstronomicalBody(ExtraBodyPrefabs);
        }
    }

    void spawnAstronomicalBody (AstronomicalBodyBag bag)
    {
        var next = bag.GetNext();
        var body = Instantiate(next, findUnoccupiedPosition(next.MinDistanceFromOtherColliders), UnityEngine.Random.rotation, AstronomicalBodyParent);
    }

    public Vector3 findUnoccupiedPosition (float minDistance)
    {
        Vector3 position;
        
        do
        {
            position = UnityEngine.Random.onUnitSphere * RandomExtra.Range(DistanceFromOriginRange);
        } while (Physics.CheckSphere(position, minDistance));

        return position;
    }
}

[Serializable]
public class AstronomicalBodyBag : BagRandomizer<AstronomicalBody> {}
