using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatMeter : MonoBehaviour
{
    public AnimationCurve HeatToTopLabelAlpha, HeatToBottomLableAlpha;

    public CanvasGroup TopLabel, BottomLabel;
    public Image Bar;

    void Update ()
    {
        Bar.fillAmount = Player.Instance.NormalizedHeat;

        float heat = Player.Instance.Resources.Heat;

        TopLabel.alpha = HeatToTopLabelAlpha.Evaluate(heat);
        BottomLabel.alpha = HeatToBottomLableAlpha.Evaluate(heat);
    }
}
