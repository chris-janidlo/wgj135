using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMeters : MonoBehaviour
{
    public Text OutlineText, FillText;
    public PillarOfCreation PillarOfCreation;

    void Update ()
    {
        var currentResources = Player.Instance.Resources;
        var targetResources = PillarOfCreation.Goals;

        string text =
            meter("HYDROGEN", currentResources.Hydrogen, targetResources.Hydrogen) + "\n" +
            meter("METHANE\t", currentResources.Methane, targetResources.Methane) + "\n" +
            meter("SILICON\t", currentResources.Silicon, targetResources.Silicon);

        OutlineText.text = text;
        FillText.text = text;
    }

    string meter (string label, int current, int goal)
    {
        bool success = current >= goal;
        return (success ? "[" : " ") + $"{label}\t{current}/{goal}" + (success ? "]" : "");
    }
}
