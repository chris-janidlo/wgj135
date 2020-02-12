using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using crass;

[RequireComponent(typeof(Collider))]
public class Mothership : MonoBehaviour
{
    public PillarOfCreation PillarOfCreation;
    public string SceneToLoadOnVictory;

    void OnTriggerEnter (Collider other)
    {
        var currentResources = Player.Instance.Resources;
        var targetResources = PillarOfCreation.Goals;

        bool won = true;

        foreach (ResourceType type in EnumUtil.AllValues<ResourceType>())
        {
            if (currentResources[type] < targetResources[type])
            {
                won = false;
                break;
            }
        }

        if (won) SceneManager.LoadScene(SceneToLoadOnVictory);
    }
}
